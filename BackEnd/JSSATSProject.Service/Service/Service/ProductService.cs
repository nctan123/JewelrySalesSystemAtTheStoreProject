using System.Linq.Expressions;
using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.AzureBlob;
using JSSATSProject.Repository.ConstantsContainer;
using JSSATSProject.Repository.CustomLib;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
using Microsoft.EntityFrameworkCore;
using JSSATSProject.Service.Extensions;
using JSSATSProject.Repository.Repos;

namespace JSSATSProject.Service.Service.Service;

public class ProductService : IProductService
{
    private readonly IDiamondPriceListService _diamondPriceListService;
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly AzureBlobStorage _blobService;

    public ProductService(UnitOfWork unitOfWork, IMapper mapper, AzureBlobStorage blobService,
        IDiamondPriceListService diamondPriceListService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _blobService = blobService;
        _diamondPriceListService = diamondPriceListService;
    }

    public async Task<ResponseModel> CreateProductAsync(RequestCreateProduct requestProduct)
    {
        string imageUrl = null;
        // Upload image and get URL
        if (requestProduct.ImgFile != null)
        {
            string fileName = $"{Guid.NewGuid()}_{requestProduct.ImgFile.FileName}";
            imageUrl = await _blobService.UploadImageAsync(requestProduct.ImgFile.OpenReadStream(), fileName);
        }

        var entity = _mapper.Map<Product>(requestProduct);
        entity.Img = imageUrl;

        var productCategoryTypeId =
            await _unitOfWork.ProductCategoryRepository.GetTypeIdByCategoryIdAsync(requestProduct.CategoryId);
        var newCode = await GenerateUniqueCodeAsync(productCategoryTypeId);

        entity.Code = newCode;

        await _unitOfWork.ProductRepository.InsertAsync(entity);
        await _unitOfWork.SaveAsync();

        return new ResponseModel
        {
            Data = entity,
            MessageError = ""
        };
    }


    public async Task<decimal> CalculateProductPrice(Product correspondingProduct)
    {
        decimal totalPrice = 0;
        decimal diamondPrice = 0;
        var priceRate = correspondingProduct.PriceRate;
        var gemCost = correspondingProduct.GemCost.GetValueOrDefault();
        var materialCost = correspondingProduct.MaterialCost.GetValueOrDefault();
        var productionCost = correspondingProduct.ProductionCost.GetValueOrDefault();
        var diamondsOfProduct = correspondingProduct.ProductDiamonds;
        var diamond = diamondsOfProduct?.FirstOrDefault()?.Diamond;
        var totalFactors = diamond is null
            ? 1
            : (diamond.Fluorescence.PriceRate * diamond.Shape.PriceRate *
               diamond.Symmetry.PriceRate * diamond.Polish.PriceRate).GetValueOrDefault();

        if (correspondingProduct.CategoryId == ProductConstants.DiamondsCategory)
        {
            diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                diamond.ClarityId,
                diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors, DateTime.Now);
            totalPrice = (productionCost + diamondPrice) * priceRate;
        }
        else if (correspondingProduct.CategoryId is ProductConstants.BraceletCategory
                 or ProductConstants.EarringsCategory or ProductConstants.NecklaceCategory
                 or ProductConstants.RingCategory or ProductConstants.RetailGoldCategory
                 or ProductConstants.WholesaleGoldCategory
                )
        {
            //except diamond category, all the rest product categories has material 
            var productMaterial = correspondingProduct.ProductMaterials!.First();
            var closestPriceList = productMaterial.Material.MaterialPriceLists
                .OrderBy(m => Math.Abs((m.EffectiveDate - DateTime.Today).TotalDays))
                .First();

            if (correspondingProduct.CategoryId is ProductConstants.WholesaleGoldCategory)
                return closestPriceList.SellPrice * priceRate;

            //not all product has diamond (retail gold, wholesale gold,...)
            if (diamond is not null)
                diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                    diamond.ClarityId, diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors,
                    DateTime.Now);

            var materialPrice = productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
            totalPrice = diamondPrice + materialPrice + gemCost + materialCost + productionCost;
            totalPrice *= priceRate;
        }

        return Math.Ceiling(totalPrice);
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
        var totalFactors = diamond is null
            ? 1
            : (diamond.Fluorescence.PriceRate * diamond.Shape.PriceRate *
               diamond.Symmetry.PriceRate * diamond.Polish.PriceRate).GetValueOrDefault();

        if (correspondingProduct.CategoryId == ProductConstants.DiamondsCategory)
        {
            diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOriginAndFactors(diamond.CutId,
                diamond.ClarityId,
                diamond.ColorId, diamond.CaratId, diamond.OriginId, totalFactors, DateTime.Now);
            totalPrice = (productionCost + diamondPrice) * priceRate;
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
                    DateTime.Now);

            //except diamond category, all the rest product categories has material 
            var productMaterial = correspondingProduct.ProductMaterials.First();
            var closestPriceList = productMaterial.Material.MaterialPriceLists
                .OrderByDescending(m => m.EffectiveDate)
                .First();

            var materialPrice = productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
            totalPrice = diamondPrice + materialPrice + gemCost + materialCost + productionCost;
            totalPrice *= priceRate;
        }
        else if (correspondingProduct.CategoryId is ProductConstants.WholesaleGoldCategory)
        {
            var productMaterial = correspondingProduct.ProductMaterials.First();
            var closestPriceList = productMaterial.Material.MaterialPriceLists
                .OrderBy(m => Math.Abs((m.EffectiveDate - DateTime.Today).TotalDays))
                .First();
            totalPrice = quantity * closestPriceList.SellPrice * priceRate;
        }

        return Math.Ceiling(totalPrice);
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
                               "ProductMaterials,ProductMaterials.Material," + "Stalls"
        );
        var response = _mapper.Map<List<ResponseProduct>>(entities);
        foreach (var responseProduct in response)
        {
            responseProduct.ProductValue =
                await CalculateProductPrice(entities.Where(e => e.Id == responseProduct.Id).First()!);
            var promotion =
                await _unitOfWork.PromotionRepository.GetPromotionByCategoryAsync(responseProduct.CategoryId);
            if (promotion != null)
            {
                responseProduct.PromotionId = promotion.Id;
                responseProduct.DiscountRate = promotion.DiscountRate;
            }
        }

        return new ResponseModel
        {
            Data = response,
            MessageError = ""
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
                               "ProductMaterials,ProductMaterials.Material," + "Category.Type"
        );

        var enumerable = response.ToList();
        if (!enumerable.Any()) return null;

        return enumerable.First();
    }

    //Update Stall
    public async Task<ResponseModel> UpdateStallProductAsync(int productId, RequestUpdateProduct requestProduct)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.GetAsync(
                p => p.Id == productId,
                includeProperties: "Stalls");
            var product = products.FirstOrDefault();

            if (product != null)
            {
                if (requestProduct.StallsId.HasValue)
                {
                    var newStall = await _unitOfWork.StallRepository.GetByIDAsync(requestProduct.StallsId.Value);
                    if (newStall != null)
                    {
                        product.StallsId = requestProduct.StallsId.Value;
                        product.Stalls = newStall;
                    }
                    else
                    {
                        return new ResponseModel
                        {
                            Data = null,
                            MessageError = "Stall not found"
                        };
                    }
                }

                _mapper.Map(requestProduct, product);
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new ResponseModel
                {
                    Data = product,
                    MessageError = ""
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Product not found"
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            // Logger.LogError(ex, "An error occurred while updating the product.");
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the product: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> GetAllAsync(int categoryId, int? stallId = null, int pageIndex = 1,
        int pageSize = 10,
        bool ascending = true, bool includeNullStalls = true)
    {
        Expression<Func<Product, bool>> filter;

        if (includeNullStalls)
        {
            filter = product => product.CategoryId == categoryId && product.Stalls == null;
        }
        else
        {
            filter = product => product.CategoryId == categoryId && product.Stalls != null;
        }

        if (stallId.HasValue)
        {
            filter = filter.AndAlso(product => product.StallsId == stallId.Value);
        }

        IOrderedQueryable<Product> OrderBy(IQueryable<Product> p) => p.OrderBy(pr => pr.Status);

        // Retrieve all products matching the filter
        var entities = await _unitOfWork.ProductRepository.GetAsync(
            filter,
            orderBy: OrderBy,
            includeProperties: "ProductDiamonds.Diamond.Carat,ProductDiamonds.Diamond.Clarity," +
                               "ProductDiamonds.Diamond.Color,ProductDiamonds.Diamond.Cut," +
                               "ProductDiamonds.Diamond.Fluorescence,ProductDiamonds.Diamond.Origin," +
                               "ProductDiamonds.Diamond.Polish,ProductDiamonds.Diamond.Shape," +
                               "ProductDiamonds.Diamond.Symmetry,ProductMaterials.Material.MaterialPriceLists,Category," +
                               "ProductMaterials,ProductMaterials.Material,Stalls,Stalls.Type",
            pageIndex: pageIndex,
            pageSize: pageSize
        );

        var responseList = new List<ResponseProduct>();
        foreach (var entity in entities)
        {
            var responseProduct = _mapper.Map<ResponseProduct>(entity);
            responseProduct.ProductValue = await CalculateProductPrice(entity);
            var promotion =
                await _unitOfWork.PromotionRepository.GetPromotionByCategoryAsync(responseProduct.CategoryId);
            if (promotion != null)
            {
                responseProduct.PromotionId = promotion.Id;
                responseProduct.DiscountRate = promotion.DiscountRate;
            }

            responseList.Add(responseProduct);
        }

        // Ensure Stall property or a similar property exists in ResponseProduct
        // If not, adjust the sorting to match available properties in ResponseProduct
        responseList = responseList
            .OrderBy(rp => rp.Status)
            .ThenBy(rp => ascending ? rp.ProductValue : -rp.ProductValue)
            .ThenBy(rp => ascending ? rp.Name : null)
            .ToList();


        var totalCount = await _unitOfWork.ProductRepository.CountAsync(filter);
        var products = await _unitOfWork.ProductRepository.GetAsync(filter);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            Data = responseList,
            TotalElements = totalCount,
            TotalPages = totalPages,
            MessageError = ""
        };
    }


    public async Task<ResponseModel> SearchProductsAsync(int categoryId, string searchTerm, int? stallId = null,
        int pageIndex = 1,
        int pageSize = 10, bool ascending = true, bool includeNullStalls = true)
    {
        Expression<Func<Product, bool>> filter;

        if (includeNullStalls)
        {
            filter = p => p.CategoryId == categoryId && p.Stalls == null;
        }
        else
        {
            filter = p => p.CategoryId == categoryId && p.Stalls != null;
        }

        if (stallId.HasValue)
        {
            filter = filter.AndAlso(p => p.StallsId == stallId.Value);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter = filter.AndAlso(p => p.Code.Contains(searchTerm) || p.Name.Contains(searchTerm));
        }

        IOrderedQueryable<Product> OrderBy(IQueryable<Product> p)
            => p.OrderBy(pr => pr.Status);

        var entities = await _unitOfWork.ProductRepository.GetAsync(
            filter,
            includeProperties: "ProductDiamonds.Diamond.Carat,ProductDiamonds.Diamond.Clarity," +
                               "ProductDiamonds.Diamond.Color,ProductDiamonds.Diamond.Cut," +
                               "ProductDiamonds.Diamond.Fluorescence,ProductDiamonds.Diamond.Origin," +
                               "ProductDiamonds.Diamond.Polish,ProductDiamonds.Diamond.Shape," +
                               "ProductDiamonds.Diamond.Symmetry,ProductMaterials.Material.MaterialPriceLists,Category," +
                               "ProductMaterials,ProductMaterials.Material,Stalls"
        );

        var responseList = new List<ResponseProduct>();
        foreach (var entity in entities)
        {
            var responseProduct = _mapper.Map<ResponseProduct>(entity);
            responseProduct.ProductValue = await CalculateProductPrice(entity);
            var promotion =
                await _unitOfWork.PromotionRepository.GetPromotionByCategoryAsync(responseProduct.CategoryId);
            if (promotion != null)
            {
                responseProduct.PromotionId = promotion.Id;
                responseProduct.DiscountRate = promotion.DiscountRate;
            }

            responseList.Add(responseProduct);
        }

        // Sort the response list based on the ascending parameter
        responseList = responseList
            .OrderBy(rp => rp.Status)
            .ThenBy(ascending
                ? (Func<ResponseProduct, object>)(rp => rp.ProductValue)
                : (Func<ResponseProduct, object>)(rp => -rp.ProductValue)) // ProductValue sorting
            .ThenBy(ascending
                ? (Func<ResponseProduct, object>)(rp => rp.Name)
                : (Func<ResponseProduct, object>)(rp => rp.Name)) // Name sorting
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalCount = await _unitOfWork.ProductRepository.CountAsync(filter);
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new ResponseModel
        {
            TotalPages = totalPages,
            TotalElements = totalCount,
            Data = responseList,
            MessageError = ""
        };
    }


    public async Task<int> CountAsync(Expression<Func<Product, bool>> filter = null)
    {
        return await _unitOfWork.ProductRepository.CountAsync(filter);
    }

    public async Task UpdateAllProductStatusAsync(SellOrder sellOrder, string newStatus)
    {
        var productIds = sellOrder.SellOrderDetails.Select(s => s.ProductId);
        foreach (var productId in productIds)
        {
            await UpdateProductStatusAsync(productId, newStatus);
        }
    }

    public async Task<decimal> CalculateMaterialBuyPrice(int? materialId, decimal? materialWeight)
    {
        var price = await _unitOfWork.MaterialPriceListRepository.GetMaterialBuyPriceAsync(materialId, materialWeight);
        return price;
    }

    public async Task<bool> AreValidProducts(Dictionary<string, int> productCodes)
    {
        if (!productCodes.Any()) return false;

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
                    //check if product quantity in inventory is greater than quantity in the order
                    return p.ProductMaterials.First().Weight >= productCodes[p.Code];

                return productCodes[p.Code] == 1;
            })
            .ToDictionary(p => p.Code, p => productCodes[p.Code]);
        if (validProductsDictionary.Count != productCodes.Count) return false;
        //check if the count of valid products matches the count of provided product codes
        return validProductsDictionary.Count == productCodes.Count;
    }

    public async Task<string> GenerateUniqueCodeAsync(int productcategorytypeId)
    {
        string prefix;
        switch (productcategorytypeId)
        {
            case ProductConstants.JWECategoryTypeId:
                prefix = ProductConstants.JWEPrefix;
                break;
            case ProductConstants.RGOCategoryTypeId:
                prefix = ProductConstants.RGOPrefix;
                break;
            case ProductConstants.WSGCategoryTypeId:
                prefix = ProductConstants.WSGPrefix;
                break;
            default:
                prefix = ProductConstants.DIAPrefix;
                break;
        }

        string newCode;
        do
        {
            newCode = prefix + CustomLibrary.RandomNumber(3);
        } while (await _unitOfWork.Context.Products.AnyAsync(so => so.Code == newCode));

        return newCode;
    }

    public async Task<ResponseModel> UpdateProductStatusAsync(int productId, string newStatus)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.GetAsync(
                p => p.Id == productId,
                includeProperties: "Stalls");
            var product = products.FirstOrDefault();

            if (product != null)
            {
                product.Status = newStatus;
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveAsync();

                return new ResponseModel
                {
                    Data = product,
                    MessageError = ""
                };
            }

            return new ResponseModel
            {
                Data = null,
                MessageError = "Product not found"
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            // Logger.LogError(ex, "An error occurred while updating the product.");
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the product: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> UpdateProductStatusAsync(string productCode, string newStatus)
    {
        try
        {
            var products = await _unitOfWork.ProductRepository.GetAsync(
                p => p.Code == productCode,
                includeProperties: "Stalls");
            var product = products.FirstOrDefault();

            if (product != null)
            {
                product.Status = newStatus;
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveAsync();
            }

            return new ResponseModel
            {
                Data = product,
                MessageError = ""
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            // Logger.LogError(ex, "An error occurred while updating the product.");
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the product: " + ex.Message
            };
        }
    }

    public async Task<ResponseModel> UpdateProductStatusAsync(IEnumerable<string> productCodes, string newStatus)
    {
        try
        {
            var result = new List<Product>();
            foreach (var code in productCodes)
            {
                var products = await _unitOfWork.ProductRepository.GetAsync(
                    p => p.Code == code,
                    includeProperties: "Stalls");
                var product = products.FirstOrDefault();

                if (product != null)
                {
                    product.Status = newStatus;
                    result.Add(product);
                    await _unitOfWork.ProductRepository.UpdateAsync(product);
                    await _unitOfWork.SaveAsync();
                }
            }

            return new ResponseModel
            {
                Data = result,
                MessageError = ""
            };

            return new ResponseModel
            {
                Data = null,
                MessageError = "Product not found"
            };
        }
        catch (Exception ex)
        {
            // Log the exception and return an appropriate error response
            // Logger.LogError(ex, "An error occurred while updating the product.");
            return new ResponseModel
            {
                Data = null,
                MessageError = "An error occurred while updating the product: " + ex.Message
            };
        }
    }

    public async Task<Product> UpdateWholesaleGoldQuantity(Product product, int quantity)
    {
        product.ProductMaterials.First().Weight -= quantity;
        var inventoryWeight = product.ProductMaterials.First().Weight;
        if (inventoryWeight == 0) product.Status = ProductConstants.InactiveStatus;
        await _unitOfWork.ProductRepository.UpdateAsync(product);
        return product;
    }

    public async Task<bool> UpdateEntityProductAsync(int id, RequestUpdateEntityProduct request)
    {
        // Retrieve the product with related entities
        var products = await _unitOfWork.ProductRepository.GetAsync(
            p => p.Id == id,
            includeProperties: "ProductDiamonds,ProductMaterials");

        if (products == null)
        {
            return false;
        }

        var product = products.FirstOrDefault();
        // Update product properties
        _mapper.Map(request, product);

        // Handle ProductMaterial
        if (request.MaterialId.HasValue)
        {
            var existingProductMaterial = product.ProductMaterials
                .FirstOrDefault(pm => pm.MaterialId == request.MaterialId.Value);

            if (existingProductMaterial != null)
            {
                existingProductMaterial.Weight = request.Weight;
                _unitOfWork.ProductMaterialRepository.UpdateAsync(existingProductMaterial);
            }
            else
            {
                var newProductMaterial = new ProductMaterial
                {
                    ProductId = product.Id,
                    MaterialId = request.MaterialId.Value,
                    Weight = request.Weight
                };
                await _unitOfWork.ProductMaterialRepository.InsertAsync(newProductMaterial);
            }
        }
        else
        {
            // Remove material if MaterialId is not provided
            var materialToRemove = product.ProductMaterials
                .FirstOrDefault(pm => pm.MaterialId == request.MaterialId);
            if (materialToRemove != null)
            {
                await _unitOfWork.ProductMaterialRepository.DeleteAsync(materialToRemove);
            }
        }

        // Handle ProductDiamond
        if (request.DiamondId.HasValue)
        {
            var existingProductDiamond = product.ProductDiamonds
                .FirstOrDefault(pd => pd.DiamondId == request.DiamondId.Value);

            if (existingProductDiamond != null)
            {
                var diamond = await _unitOfWork.DiamondRepository.GetByIDAsync(request.DiamondId.Value);
                if (diamond != null)
                {
                    _mapper.Map(request, diamond); // Update diamond properties
                    _unitOfWork.DiamondRepository.UpdateAsync(diamond);
                }
            }
            else
            {
                var newDiamond = await _unitOfWork.DiamondRepository.GetByIDAsync(request.DiamondId.Value);
                if (newDiamond != null)
                {
                    var newProductDiamond = new ProductDiamond
                    {
                        ProductId = product.Id,
                        DiamondId = newDiamond.Id,
                        Diamond = newDiamond
                    };
                    await _unitOfWork.ProductDiamondRespository.InsertAsync(newProductDiamond);
                }
            }
        }
        else
        {
            // Remove diamond if DiamondId is not provided
            var diamondToRemove = product.ProductDiamonds
                .FirstOrDefault(pd => pd.DiamondId == request.DiamondId);
            if (diamondToRemove != null)
            {
                await _unitOfWork.ProductDiamondRespository.DeleteAsync(diamondToRemove);
            }
        }

        // Update product
        _unitOfWork.ProductRepository.UpdateAsync(product);
        await _unitOfWork.SaveAsync();

        return true;
    }
}