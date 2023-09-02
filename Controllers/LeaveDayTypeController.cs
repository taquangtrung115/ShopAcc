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
    [Route("api/leavedaytype")]
    public class LeaveDayTypeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly LeaveDayTypeServices _services;
        public LeaveDayTypeController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new LeaveDayTypeServices(_dataContext);
        }
        #region Action Leave Day Type
        [HttpGet]
        public ActionResult<APIModel<LeaveDayType>> Get()
        {
            var APIModel = _services.GetListLeaveDayTypeAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveDayType>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<LeaveDayType>> Add(LeaveDayType shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<LeaveDayType>> Update(LeaveDayType shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<LeaveDayType>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}