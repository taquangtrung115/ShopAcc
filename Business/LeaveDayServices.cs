using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class LeaveDayServices
    {
        private readonly DataContext _dataContext;
        public LeaveDayServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Raw Data
        public APIModel<LeaveDay> GetListLeaveDayAPI()
        {
            var apiModel = new APIModel<LeaveDay>();
            apiModel.Data = GetListLeaveDayRawData();
            apiModel.Status = StatusApi.E_SUCCESSED.ToString();
            return apiModel;
        }
        public List<LeaveDay> GetListLeaveDayRawData()
        {
            return _dataContext.LeaveDay.ToList();
        }
        #endregion

        #region Action Shift
        public APIModel<LeaveDay> Add(LeaveDay leaveDay)
        {
            var APIModel = new APIModel<LeaveDay>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                leaveDay.ID = Guid.NewGuid();
                leaveDay.IsDelete = null;
                _dataContext.Add(leaveDay);
                _dataContext.SaveChanges();
                APIModel.Data = GetListLeaveDayRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public LeaveDay GetByID(Guid id)
        {
            var product = new LeaveDay();
            if (id != Guid.Empty)
            {
                product = _dataContext.LeaveDay.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<LeaveDay> Update(LeaveDay leaveDay)
        {
            var APIModel = new APIModel<LeaveDay>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbLeaveDay = _dataContext.LeaveDay.Where(s => s.ID == leaveDay.ID).FirstOrDefault();
                if (dbLeaveDay == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<LeaveDay> { dbLeaveDay };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<LeaveDay> Delete(Guid id)
        {
            var APIModel = new APIModel<LeaveDay>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbLeaveDay = GetByID(id);
                if (dbLeaveDay == null)
                    return APIModel;
                _dataContext.LeaveDay.Remove(dbLeaveDay);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListLeaveDayRawData();
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
