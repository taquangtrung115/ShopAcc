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
    [Route("api/overtime")]
    public class OverTimeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly OverTimeServices _services;
        public OverTimeController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new OverTimeServices(_dataContext);
        }
        #region Action Over Time
        [HttpGet]
        public ActionResult<APIModel<OverTime>> Get()
        {
            var APIModel = _services.GetListOverTimeAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OverTime>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<OverTime>> Add(OverTime shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<OverTime>> Update(OverTime shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<OverTime>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}