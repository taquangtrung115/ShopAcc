using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class DayOffServices
    {
        private readonly DataContext _dataContext;
        public DayOffServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Day Off Raw Data
        public List<DayOff> GetListDayOffRawData()
        {
            return _dataContext.DayOff.ToList();
        }
        public APIModel<DayOff> GetListDayOffAPI()
        {
            APIModel<DayOff> api = new APIModel<DayOff>();
            api.Status = StatusApi.E_SUCCESSED.ToString();
            api.Data = GetListDayOffRawData();
            return api;
        }
        #endregion

        #region Action Day Off
        public APIModel<DayOff> Add(DayOff dayOff)
        {
            var APIModel = new APIModel<DayOff>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                dayOff.ID = Guid.NewGuid();
                dayOff.IsDelete = null;
                _dataContext.Add(dayOff);
                _dataContext.SaveChanges();
                APIModel.Data = GetListDayOffRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public DayOff GetByID(Guid id)
        {
            var product = new DayOff();
            if (id != Guid.Empty)
            {
                product = _dataContext.DayOff.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<DayOff> Update(DayOff roster)
        {
            var APIModel = new APIModel<DayOff>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbDayOff = _dataContext.DayOff.Where(s => s.ID == roster.ID).FirstOrDefault();
                if (dbDayOff == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<DayOff> { dbDayOff };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<DayOff> Delete(Guid id)
        {
            var APIModel = new APIModel<DayOff>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbDayOff = GetByID(id);
                if (dbDayOff == null)
                    return APIModel;
                _dataContext.DayOff.Remove(dbDayOff);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListDayOffRawData();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        #endregion
    }
}
