using Microsoft.AspNetCore.Mvc;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using System.Net.Http;
using System;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Azure;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;
using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.SignalR.Hosting;
using ShopAccBE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShopAccBE.Business
{
    public class ProductServices
    {
        private readonly DataContext _dataContext;
        public ProductServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Import Product
        public APIModel<Product> Import(List<object> lstObjProduct)
        {
            var APIModel = new APIModel<Product>();
            var lstProduct = new List<Product>();
            if (lstObjProduct != null)
            {
                foreach (var item in lstObjProduct)
                {
                    var productConvert = (Product)Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString(), typeof(Product));
                    if (productConvert != null)
                        lstProduct.Add(productConvert);
                }
                if (lstProduct != null && lstProduct.Count > 0)
                {
                    var lstProductReturn = new List<Product>();
                    foreach (var product in lstProduct)
                    {
                        var productSave = new Product();
                        productSave.ID = Guid.NewGuid();
                        productSave.IsDelete = null;
                        productSave.DateUpdate = DateTime.Now;
                        productSave.Amount = product.Amount;
                        productSave.Price = product.Price;
                        productSave.Description = product.Description;
                        productSave.YearCreate = product.YearCreate;
                        productSave.Title = product.Title;
                        productSave.ProductName = product.ProductName;

                        _dataContext.Add(productSave);
                        _dataContext.SaveChanges();
                        lstProductReturn.Add(productSave);
                    }
                    APIModel.Data = lstProductReturn;
                    APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                }
            }
            else
                APIModel.Status = StatusApi.E_FAILED.ToString();
            return APIModel;
        }

        #endregion

        #region Action Product
        public APIModel<Product> GetListProductToApi()
        {
            var lstProduct = GetListProduct();
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            if (lstProduct != null)
            {
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = lstProduct;
            }
            else
                APIModel.Status = StatusApi.E_FAILED.ToString();
            return APIModel;
        }
        private List<Product> GetListProduct()
        {
            return _dataContext.Product.ToList();
        }
        public APIModel<Product> Add(Product product)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                product.ID = Guid.NewGuid();
                product.IsDelete = null;
                _dataContext.Add(product);
                _dataContext.SaveChanges();
                APIModel.Data = GetListProduct();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public Product GetByID(Guid id)
        {
            var product = new Product();
            if (id != Guid.Empty)
            {
                product = _dataContext.Product.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<Product> Update(Product product)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbProduct = _dataContext.Product.Where(s => s.ID == product.ID).FirstOrDefault();
                if (dbProduct == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<Product> { dbProduct };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<Product> Delete(Guid id)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbProduct = GetByID(id);
                if (dbProduct == null)
                    return APIModel;
                _dataContext.Product.Remove(dbProduct);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = _dataContext.Product.ToList();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        #endregion

    }
}
