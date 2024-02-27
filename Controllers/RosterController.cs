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
    [Route("api/roster")]
    public class RosterController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly RosterServices _services;
        public RosterController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new RosterServices(_dataContext);
        }
        #region Action Roster
        [HttpGet]
        public ActionResult<APIModel<Roster>> Get()
        {
            var APIModel = _services.GetListRosterAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Roster>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<Roster>> Add(Roster shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<Roster>> Update(Roster shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<Roster>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}