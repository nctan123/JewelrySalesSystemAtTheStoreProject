using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using JSSATSProject.Repository.CacheManagers;
using JSSATSProject.Repository.ConstantsContainer;

namespace JSSATSProject.Service.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDiamondPriceListService _diamondPriceListService;
        private readonly CacheManager<Product> _productCacheManager;
        private readonly CacheManager<MaterialPriceList> _materialPriceListCacheManager;
        private readonly IPromotionService _promotionService;

        public ProductService(UnitOfWork unitOfWork, IMapper mapper, IDiamondPriceListService diamondPriceListService
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _diamondPriceListService = diamondPriceListService;
        }


        public async Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct)
        {
            var entity = _mapper.Map<Product>(requestProduct);
            await _unitOfWork.ProductRepository.InsertAsync(entity);
            await _unitOfWork.SaveAsync();
            return new ResponseModel
            {
                Data = entity,
                MessageError = "",
            };
        }


        public async Task<ResponseModel> GetAllAsync()
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                includeProperties: "ProductDiamonds.Diamond.Carat,ProductDiamonds.Diamond.Clarity," +
                                   "ProductDiamonds.Diamond.Color,ProductDiamonds.Diamond.Cut," +
                                   "ProductDiamonds.Diamond.Fluorescence,ProductDiamonds.Diamond.Origin," +
                                   "ProductDiamonds.Diamond.Polish,ProductDiamonds.Diamond.Shape," +
                                   "ProductDiamonds.Diamond.Symmetry,ProductMaterials.Material.MaterialPriceLists,Category," +
                                   "ProductMaterials,ProductMaterials.Material"
            );
            var response = _mapper.Map<List<ResponseProduct>>(entities);
            foreach (var responseProduct in response)
            {
                responseProduct.ProductValue = await CalculateProductPrice(responseProduct);
                var promotion = await _unitOfWork.PromotionRepository.GetPromotionByCategoryAsync(responseProduct.CategoryId);
                responseProduct.PromotionId = promotion.Id;
                responseProduct.DiscountRate = promotion.DiscountRate;
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<decimal> CalculateProductPrice(ResponseProduct responseProduct)
        {
            decimal totalPrice = 0;
            decimal diamondPrice = 0;
            var correspondingProduct = _mapper.Map<Product>(responseProduct);
            var priceRate = correspondingProduct.PriceRate;
            var gemCost = correspondingProduct.GemCost.GetValueOrDefault();
            var materialCost = correspondingProduct.MaterialCost.GetValueOrDefault();
            var productionCost = correspondingProduct.ProductionCost.GetValueOrDefault();
            var diamondsOfProduct = responseProduct.ProductDiamonds;
            var diamond = diamondsOfProduct?.FirstOrDefault()?.Diamond;
            var timeStamp = DateTime.Now;
            var closestDate = await _diamondPriceListService.GetClosestPriceEffectiveDate(timeStamp);
            var totalFactors = diamond is null
                ? 1
                : (diamond.Fluorescence.PriceRate + diamond.Shape.PriceRate +
                   diamond.Symmetry.PriceRate + diamond.Polish.PriceRate).GetValueOrDefault();

            if (responseProduct.CategoryId == ProductConstants.DiamondsCategory)
            {
                diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                    diamond.ClarityId,
                    diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors, closestDate);
                totalPrice = priceRate * diamondPrice;
            }
            else if (correspondingProduct.CategoryId is ProductConstants.BraceletCategory
                     or ProductConstants.EarringsCategory or ProductConstants.NecklaceCategory
                     or ProductConstants.RingCategory or ProductConstants.RetailGoldCategory
                     or ProductConstants.WholesaleGoldCategory
                    )
            {
                //not all product has diamond (retail gold, wholesale gold,...)
                if (diamond is not null)
                    diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                        diamond.ClarityId, diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors,
                        closestDate);

                //except diamond category, all the rest product categories has material 
                var productMaterial = responseProduct.ProductMaterials!.First();
                var closestPriceList = productMaterial.Material.MaterialPriceLists
                    .OrderBy(m => Math.Abs((m.EffectiveDate - DateTime.Today).TotalDays))
                    .First();

                var materialPrice = productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
                totalPrice = diamondPrice + materialPrice + gemCost + materialCost + productionCost;
                if (correspondingProduct.CategoryId != ProductConstants.RetailGoldCategory &&
                    correspondingProduct.CategoryId != ProductConstants.WholesaleGoldCategory)
                {
                    totalPrice *= priceRate;
                }
            }
            return totalPrice;
        }

        //additional quantity var for Wholesale gold
        public async Task<decimal> CalculateProductPrice(Product correspondingProduct, int quantity)
        {
            decimal totalPrice = 0;
            decimal diamondPrice = 0;
            var priceRate = correspondingProduct.PriceRate;
            var gemCost = correspondingProduct.GemCost.GetValueOrDefault();
            var materialCost = correspondingProduct.MaterialCost.GetValueOrDefault();
            var productionCost = correspondingProduct.ProductionCost.GetValueOrDefault();
            var diamondsOfProduct = correspondingProduct.ProductDiamonds;
            var diamond = diamondsOfProduct?.FirstOrDefault()?.Diamond;
            var timeStamp = DateTime.Now;
            var closestDate = await _diamondPriceListService.GetClosestPriceEffectiveDate(timeStamp);
            var totalFactors = diamond is null
                ? 1
                : (diamond.Fluorescence.PriceRate + diamond.Shape.PriceRate +
                   diamond.Symmetry.PriceRate + diamond.Polish.PriceRate).GetValueOrDefault();

            if (correspondingProduct.CategoryId == ProductConstants.DiamondsCategory)
            {
                diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                    diamond.ClarityId,
                    diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors, closestDate);
                totalPrice = priceRate * diamondPrice;
            }
            else if (correspondingProduct.CategoryId is ProductConstants.BraceletCategory
                     or ProductConstants.EarringsCategory or ProductConstants.NecklaceCategory
                     or ProductConstants.RingCategory or ProductConstants.RetailGoldCategory
                    )
            {
                //not all product has diamond (retail gold, wholesale gold,...)
                if (diamond is not null)
                    diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                        diamond.ClarityId, diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors,
                        closestDate);

                //except diamond category, all the rest product categories has material 
                var productMaterial = correspondingProduct.ProductMaterials!.First();
                var closestPriceList = productMaterial.Material.MaterialPriceLists
                    .OrderBy(m => Math.Abs((m.EffectiveDate - DateTime.Today).TotalDays))
                    .First();

                var materialPrice = productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
                totalPrice = diamondPrice + materialPrice + gemCost + materialCost + productionCost;

                //just apply price rate for diamonds and jewelry
                if (correspondingProduct.CategoryId != ProductConstants.RetailGoldCategory &&
                    correspondingProduct.CategoryId != ProductConstants.WholesaleGoldCategory)
                    totalPrice *= priceRate;
            }
            else if (correspondingProduct.CategoryId is ProductConstants.WholesaleGoldCategory)
            {
                var productMaterial = correspondingProduct.ProductMaterials!.First();
                var closestPriceList = productMaterial.Material.MaterialPriceLists
                    .OrderBy(m => Math.Abs((m.EffectiveDate - DateTime.Today).TotalDays))
                    .First();

                var materialPrice = quantity * productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
                totalPrice = diamondPrice + materialPrice + gemCost + materialCost + productionCost;

                //just apply price rate for diamonds and jewelry
                if (correspondingProduct.CategoryId != ProductConstants.RetailGoldCategory &&
                    correspondingProduct.CategoryId != ProductConstants.WholesaleGoldCategory)
                    totalPrice *= priceRate;
            }
            return totalPrice;
        }


        public async Task<ResponseModel> GetByCodeAsync(string code)
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Code.Equals(code),
                includeProperties: "ProductDiamonds.Diamond.Carat,ProductDiamonds.Diamond.Clarity," +
                                   "ProductDiamonds.Diamond.Color,ProductDiamonds.Diamond.Cut," +
                                   "ProductDiamonds.Diamond.Fluorescence,ProductDiamonds.Diamond.Origin," +
                                   "ProductDiamonds.Diamond.Polish,ProductDiamonds.Diamond.Shape," +
                                   "ProductDiamonds.Diamond.Symmetry,ProductMaterials.Material.MaterialPriceLists,Category," +
                                   "ProductMaterials,ProductMaterials.Material"
            );
            var response = _mapper.Map<List<ResponseProduct>>(entities);
            foreach (var responseProduct in response)
            {
                responseProduct.ProductValue = await CalculateProductPrice(responseProduct);
                var promotion = await _unitOfWork.PromotionRepository.GetPromotionByCategoryAsync(responseProduct.CategoryId);
                responseProduct.PromotionId = promotion.Id;
                responseProduct.DiscountRate = promotion.DiscountRate;
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<Product?> GetEntityByCodeAsync(string code)
        {
            var response = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Code.Equals(code),
                includeProperties: "ProductDiamonds.Diamond.Carat,ProductDiamonds.Diamond.Clarity," +
                                   "ProductDiamonds.Diamond.Color,ProductDiamonds.Diamond.Cut," +
                                   "ProductDiamonds.Diamond.Fluorescence,ProductDiamonds.Diamond.Origin," +
                                   "ProductDiamonds.Diamond.Polish,ProductDiamonds.Diamond.Shape," +
                                   "ProductDiamonds.Diamond.Symmetry,ProductMaterials.Material.MaterialPriceLists,Category," +
                                   "ProductMaterials,ProductMaterials.Material"
            );

            var enumerable = response.ToList();
            if (!enumerable.Any())
            {
                return null;
            }

            return enumerable.First();
        }


        public async Task<ResponseModel> GetByNameAsync(string name)
        {
            var response = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Name.Equals(name),
                null,
                includeProperties: "",
                pageIndex: null,
                pageSize: null
            );

            if (!response.Any())
            {
                return new ResponseModel
                {
                    Data = null,
                    MessageError = $"Customer with name '{name}' not found.",
                };
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> UpdateProductAsync(int productId, RequestUpdateProduct requestProduct)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIDAsync(productId);
                if (product != null)
                {
                    product = _mapper.Map<Product>(requestProduct);
                    await _unitOfWork.ProductRepository.UpdateAsync(product);
                    await _unitOfWork.SaveAsync();

                    return new ResponseModel
                    {
                        Data = product,
                        MessageError = "",
                    };
                }

                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Not Found",
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "An error occurred while updating the customer: " + ex.Message
                };
            }
        }

        public async Task<bool> AreValidProducts(Dictionary<string, int> productCodes)
        {
            if (!productCodes.Any())
            {
                return false;
            }

            //fetch all valid products from the repository
            var products = await _unitOfWork.ProductRepository
                .GetAsync(p => productCodes.Keys.Contains(p.Code));

            //create a dictionary with product codes as keys and quantities as values
            var validProductsDictionary = products
                .Where(p => productCodes.ContainsKey(p.Code))
                .Where(p =>
                {
                    //wholesale gold
                    if (p.CategoryId == 4)
                    {
                        //check if product quantity in inventory is greater than quantity in the order
                        return p.ProductMaterials.First().Weight >= productCodes[p.Code];
                    }

                    return productCodes[p.Code] == 1;
                })
                .ToDictionary(p => p.Code, p => productCodes[p.Code]);
            if (validProductsDictionary.Count != productCodes.Count) return false;
            //check if the count of valid products matches the count of provided product codes
            return validProductsDictionary.Count == productCodes.Count;
        }

        public async Task<ResponseModel> UpdateStatusProductAsync(int productId,
            RequestUpdateStatusProduct requestProduct)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIDAsync(productId);
                if (product != null)
                {
                    _mapper.Map(requestProduct, product);

                    await _unitOfWork.ProductRepository.UpdateAsync(product);

                    return new ResponseModel
                    {
                        Data = product,
                        MessageError = "",
                    };
                }

                return new ResponseModel
                {
                    Data = null,
                    MessageError = "Not Found",
                };
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate error response
                return new ResponseModel
                {
                    Data = null,
                    MessageError = "An error occurred while updating the customer: " + ex.Message
                };
            }
        }
    }
}