﻿using AutoMapper;
using JSSATSProject.Repository;
using JSSATSProject.Repository.Entities;
using JSSATSProject.Service.Models;
using JSSATSProject.Service.Models.ProductModel;
using JSSATSProject.Service.Service.IService;
<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JSSATSProject.Repository.CacheManagers;
using JSSATSProject.Repository.ConstantsContainer;
=======

>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704

namespace JSSATSProject.Service.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDiamondPriceListService _diamondPriceListService;
        private readonly CacheManager<Product> _productCacheManager;
        private readonly CacheManager<MaterialPriceList> _materialPriceListCacheManager;

        public ProductService(UnitOfWork unitOfWork, IMapper mapper, IDiamondPriceListService diamondPriceListService,
            CacheManager<Product> productCacheManager, CacheManager<MaterialPriceList> materialPriceListCacheManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _diamondPriceListService = diamondPriceListService;
            _productCacheManager = productCacheManager;
            _materialPriceListCacheManager = materialPriceListCacheManager;
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
            }

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        private async Task<decimal> CalculateProductPrice(ResponseProduct responseProduct)
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

            if (responseProduct.CategoryId == 7)
            {
                diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOrigin(diamond.CutId, diamond.ClarityId,
                    diamond.ColorId, diamond.CaratId, diamond.OriginId);
                totalPrice = priceRate * diamondPrice;
            }
            else if (responseProduct.CategoryId is 1 or 2 or 3 or 4 or 5 or 6)
            {
                //not all product has diamond (retail gold, wholesale gold,...)
                if (diamond is not null)
                    diamondPrice = await _diamondPriceListService.FindPriceBy4CAndOrigin(diamond.CutId,
                        diamond.ClarityId, diamond.ColorId, diamond.CaratId, diamond.OriginId);

                //except diamond category, all the rest product categories has material 
                var productMaterial = responseProduct.ProductMaterials!.First();
                var today = DateTime.Today;
                var materialPriceKey = productMaterial.Material.Id + today.ToString("yyyyMMdd");
                if (!_materialPriceListCacheManager.TryGetValue(materialPriceKey, out var closestPriceList))
                {
                    closestPriceList = productMaterial.Material.MaterialPriceLists
                        .OrderBy(m => Math.Abs((m.EffectiveDate - today).TotalDays))
                        .First();
                    _materialPriceListCacheManager.SetValue(materialPriceKey, closestPriceList);
                }

                var materialPrice = productMaterial.Weight.GetValueOrDefault() * closestPriceList.SellPrice;
                totalPrice = priceRate * (diamondPrice + materialPrice + gemCost + materialCost + productionCost);
            }

            return totalPrice;
        }

        public async Task<ResponseModel> GetByCodeAsync(string code)
        {
<<<<<<< HEAD
            var response = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Code.Equals(code),
                null,
                includeProperties: "",
                pageIndex: null,
                pageSize: null
            );
=======
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Code.Equals(code),
                includeProperties: "Category,Stalls,");
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                //Weight = _unitOfWork.ProductMaterialRepository.GetByIDAsync(1).,
                Status = product.Status
            }).ToList();

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Id.Equals(id),
                includeProperties: "Category,Stalls");


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                //PriceRate = product.PriceRate,
                Status = product.Status
            }).ToList();

            return new ResponseModel
            {
                Data = response,
                MessageError = "",
            };
        }

        public async Task<ResponseModel> GetByNameAsync(string name)
        {
<<<<<<< HEAD
            var response = await _unitOfWork.ProductRepository.GetAsync(
                c => c.Name.Equals(name),
                null,
                includeProperties: "",
                pageIndex: null,
                pageSize: null
            );
=======
            var entities = await _unitOfWork.ProductRepository.GetAsync(
               c => c.Name.Equals(name),
               includeProperties: "Category,Stalls");
>>>>>>> ef1d898c610203bb40990ce34f1644abc601b704


            var response = entities.Select(product => new ResponseProductDetails
            {
                Id = product.Id,
                CategoryName = product.Category.Name,
                StallName = product.Stalls.Name,
                Code = product.Code,
                Name = product.Name,
                MaterialCost = product.MaterialCost,
                ProductionCost = product.ProductionCost,
                GemCost = product.GemCost,
                Img = product.Img,
                //PriceRate = product.PriceRate,
                Status = product.Status
            }).ToList();

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

        public async Task<ResponseModel> UpdateStatusProductAsync(int productId, RequestUpdateStatusProduct requestProduct)
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
    }
}