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
        private readonly ProductServices _services;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new ProductServices(_dataContext);
        }
        #region Action Product
        [HttpGet]
        public ActionResult<APIModel<Product>> Get()
        {
            var APIModel = _services.GetListProductToApi();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<Product>> Add(Product product)
        {
            var APIModel = _services.Add(product);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<Product>> Update(Product product)
        {
            var APIModel = _services.Update(product);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<Product>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion

        #region Import Product
        [HttpPost("import")]
        public ActionResult<APIModel<Product>> Import(List<object> lstObjProduct)
        {
            var APIModel = _services.Import(lstObjProduct);
            return Ok(APIModel);
        }
        #endregion

    }
}