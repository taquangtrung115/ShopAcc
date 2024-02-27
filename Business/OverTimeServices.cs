using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class OverTimeServices
    {
        private readonly DataContext _dataContext;
        public OverTimeServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Day Off Raw Data
        public List<OverTime> GetListOverTimeRawData()
        {
            return _dataContext.OverTime.ToList();
        }
        public APIModel<OverTime> GetListOverTimeAPI()
        {
            APIModel<OverTime> api = new APIModel<OverTime>();
            api.Status = StatusApi.E_SUCCESSED.ToString();
            api.Data = GetListOverTimeRawData();
            return api;
        }
        #endregion

        #region Action Day Off
        public APIModel<OverTime> Add(OverTime ot)
        {
            var APIModel = new APIModel<OverTime>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                ot.ID = Guid.NewGuid();
                ot.IsDelete = null;
                _dataContext.Add(ot);
                _dataContext.SaveChanges();
                APIModel.Data = GetListOverTimeRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public OverTime GetByID(Guid id)
        {
            var product = new OverTime();
            if (id != Guid.Empty)
            {
                product = _dataContext.OverTime.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<OverTime> Update(OverTime ot)
        {
            var APIModel = new APIModel<OverTime>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbDayOff = _dataContext.OverTime.Where(s => s.ID == ot.ID).FirstOrDefault();
                if (dbDayOff == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<OverTime> { dbDayOff };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<OverTime> Delete(Guid id)
        {
            var APIModel = new APIModel<OverTime>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbDayOff = GetByID(id);
                if (dbDayOff == null)
                    return APIModel;
                _dataContext.OverTime.Remove(dbDayOff);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListOverTimeRawData();
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
