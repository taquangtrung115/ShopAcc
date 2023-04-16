using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAccBE.Data;
using ShopAccBE.Model;

namespace ShopAcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            if (lstProduct != null)
            {
                APIModel.Message = "Success";
                APIModel.Data = lstProduct;
            }
            else
            {
                APIModel.Message = "Failed";
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
            APIModel.Message = "Failed";
            try
            {
                _dataContext.Add(product);
                await _dataContext.SaveChangesAsync();
                APIModel.Data = await _dataContext.Product.ToListAsync();
                APIModel.Message = "Success";
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Status = ex.ToString();
                return Ok(APIModel);
            }
        }
        [HttpPut]
        public async Task<ActionResult<APIModel<Product>>> Update(Product product)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Message = "Failed";
            try
            {
                var dbProduct = await _dataContext.Product.Where(s => s.ID == product.ID).FirstOrDefaultAsync();
                if (dbProduct == null)
                    return Ok(APIModel);
                dbProduct.Name = product.Name;
                dbProduct.YearCreate = product.YearCreate;
                dbProduct.Amount = product.Amount;
                dbProduct.Price = product.Price;
                await _dataContext.SaveChangesAsync();
                APIModel.Data = new List<Product> { dbProduct };
                APIModel.Message = "Success";
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Status = ex.ToString();
                return Ok(APIModel);
            }

        }
        [HttpDelete]
        public async Task<ActionResult<APIModel<Product>>> Delete(Guid id)
        {
            var APIModel = new APIModel<Product>();
            APIModel.Message = "Failed";
            try
            {
                var dbProduct = await _dataContext.Product.Where(s => s.ID == id).FirstOrDefaultAsync();
                if (dbProduct == null)
                    return Ok(APIModel);
                _dataContext.Product.Remove(dbProduct);
                await _dataContext.SaveChangesAsync();
                APIModel.Message = "Success";
                APIModel.Data = await _dataContext.Product.ToListAsync();
                return Ok(APIModel);
            }
            catch (Exception ex)
            {
                APIModel.Status = ex.ToString();
                return Ok(APIModel);
            }
            
        }
    }
}