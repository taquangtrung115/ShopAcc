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

namespace ShopAccBE.Business
{
    public class ProductServices
    {
        public List<Product> Import(List<object> lstObjProduct)
        {
            var lstProduct = new List<Product>();
           
            if (lstObjProduct != null)
            {
                foreach (var item in lstObjProduct)
                {

                    var productConvert = (Product)Newtonsoft.Json.JsonConvert.DeserializeObject(item.ToString(), typeof(Product));
                    if (productConvert != null)
                        lstProduct.Add(productConvert);
                }
                
            }
            return lstProduct;
        }
    }
}
