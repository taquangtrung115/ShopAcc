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
using ShopAccBE.Data.Models;

namespace ShopAcc.Controllers
{
    [ApiController]
    [Route("api/overtimetype")]
    public class OvertTimeTypeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly OverTimeTypeServices _services;
        public OvertTimeTypeController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new OverTimeTypeServices(_dataContext);
        }
        #region Action Over Time Type
        [HttpGet]
        public ActionResult<APIModel<OverTimeType>> Get()
        {
            var APIModel = _services.GetListOverTimeTypeAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OverTimeType>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<OverTimeType>> Add(OverTimeType shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<OverTimeType>> Update(OverTimeType shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<OverTimeType>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}