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
    [Route("api/shift")]
    public class ShiftController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ShiftServices _services;
        public ShiftController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _services = new ShiftServices(_dataContext);
        }
        #region Action Shift
        [HttpGet]
        public ActionResult<APIModel<Shift>> Get()
        {
            var APIModel = _services.GetListShiftAPI();
            return Ok(APIModel);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Shift>> Get(Guid id)
        {
            return Ok(_services.GetByID(id));
        }
        [HttpPost]
        public ActionResult<APIModel<Shift>> Add(ShiftModel shift)
        {
            var APIModel = _services.Add(shift);
            return Ok(APIModel);
        }
        [HttpPut]
        public ActionResult<APIModel<Shift>> Update(Shift shift)
        {
            var APIModel = _services.Update(shift);
            return Ok(APIModel);
        }
        [HttpDelete]
        public ActionResult<APIModel<Shift>> Delete(Guid id)
        {
            var APIModel = _services.Delete(id);
            return Ok(APIModel);
        }
        #endregion
    }
}