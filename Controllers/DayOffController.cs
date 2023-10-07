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
    [Route("api/dayoff")]
    public class DayOffController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly DayOffServices _services;
        public DayOffController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new DayOffServices(_dataContext);
        }
        #region Action Day Off
        [HttpGet]
        public ActionResult<APIModel<DayOff>> Get()
        {
            var APIModel = _services.GetListDayOffAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DayOff>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<DayOff>> Add(DayOff shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<DayOff>> Update(DayOff shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<DayOff>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}