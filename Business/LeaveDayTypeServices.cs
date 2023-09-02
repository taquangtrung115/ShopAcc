using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class LeaveDayTypeServices
    {
        private readonly DataContext _dataContext;
        public LeaveDayTypeServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Raw Data
        public APIModel<LeaveDayType> GetListLeaveDayTypeAPI()
        {
            var apiModel = new APIModel<LeaveDayType>();
            apiModel.Data = GetListLeaveDayTypeRawData();
            apiModel.Status = StatusApi.E_SUCCESSED.ToString();
            return apiModel;
        }
        public List<LeaveDayType> GetListLeaveDayTypeRawData()
        {
            return _dataContext.LeaveDayType.ToList();
        }
        #endregion

        #region Action Shift
        public APIModel<LeaveDayType> Add(LeaveDayType ldType)
        {
            var APIModel = new APIModel<LeaveDayType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                ldType.ID = Guid.NewGuid();
                ldType.IsDelete = null;
                _dataContext.Add(ldType);
                _dataContext.SaveChanges();
                APIModel.Data = GetListLeaveDayTypeRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public LeaveDayType GetByID(Guid id)
        {
            var product = new LeaveDayType();
            if (id != Guid.Empty)
            {
                product = _dataContext.LeaveDayType.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<LeaveDayType> Update(LeaveDayType ldType)
        {
            var APIModel = new APIModel<LeaveDayType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbLDType = _dataContext.LeaveDayType.Where(s => s.ID == ldType.ID).FirstOrDefault();
                if (dbLDType == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<LeaveDayType> { dbLDType };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<LeaveDayType> Delete(Guid id)
        {
            var APIModel = new APIModel<LeaveDayType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbLDType = GetByID(id);
                if (dbLDType == null)
                    return APIModel;
                _dataContext.LeaveDayType.Remove(dbLDType);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListLeaveDayTypeRawData();
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
