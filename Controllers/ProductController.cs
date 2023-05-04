using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;
using static System.Net.Mime.MediaTypeNames;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using ShopAccBE.Business;

namespace ShopAcc.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<ActionResult<APIModel<Product>>> Get()
        {
            var lstProduct = await _dataContext.Product.ToListAsync();
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            if (lstProduct != null)
            {
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = lstProduct;
            }
            else
            {
                APIModel.Status = StatusApi.E_FAILED.ToString();
            }
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                var product = await _dataContext.Product.Where(s => s.ID == id).FirstOrDefaultAsync();
                return Ok(product);
            }
            else
            {
                return BadRequest("Failed");
            }
        }
        [HttpPost]
        public async Task<ActionResult<APIModel<Product>>> Add(Product product)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                product.ID = Guid.NewGuid();
                product.IsDelete = null;
                _dataContext.Add(product);
                await _dataContext.SaveChangesAsync();
                APIModel.Data = await _dataContext.Product.ToListAsync();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return Ok(APIModel);
            }
        }
        [HttpPut]
        public async Task<ActionResult<APIModel<Product>>> Update(Product product)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbProduct = await _dataContext.Product.Where(s => s.ID == product.ID).FirstOrDefaultAsync();
                if (dbProduct == null)
                    return Ok(APIModel);
                await _dataContext.SaveChangesAsync();
                APIModel.Data = new List<Product> { dbProduct };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return Ok(APIModel);
            }

        }
        [HttpDelete]
        public async Task<ActionResult<APIModel<Product>>> Delete(Guid id)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbProduct = await _dataContext.Product.Where(s => s.ID == id).FirstOrDefaultAsync();
                if (dbProduct == null)
                    return Ok(APIModel);
                _dataContext.Product.Remove(dbProduct);
                await _dataContext.SaveChangesAsync();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = await _dataContext.Product.ToListAsync();
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return Ok(APIModel);
            }
            
        }
        [HttpPost("import")]
        public async Task<ActionResult<APIModel<Product>>> Import(List<object> lstObjProduct)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                if (lstObjProduct != null)
                {
                    var services = new ProductServices();
                    var lstProduct = services.Import(lstObjProduct);
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
                            await _dataContext.SaveChangesAsync();
                            lstProductReturn.Add(productSave);
                        }
                        APIModel.Data = lstProductReturn;

                        APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return Ok(APIModel);
            }
            
            return Ok(APIModel);
        }
    }
}