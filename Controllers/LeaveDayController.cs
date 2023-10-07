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
    [Route("api/leaveday")]
    public class LeaveDayController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly LeaveDayServices _services;
        public LeaveDayController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new LeaveDayServices(_dataContext);
        }
        #region Action Leave Day
        [HttpGet]
        public ActionResult<APIModel<LeaveDay>> Get()
        {
            var APIModel = _services.GetListLeaveDayAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveDay>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<LeaveDay>> Add(LeaveDay shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<LeaveDay>> Update(LeaveDay shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<LeaveDay>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}