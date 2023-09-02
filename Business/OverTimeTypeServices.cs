using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class OverTimeTypeServices
    {
        private readonly DataContext _dataContext;
        public OverTimeTypeServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Raw Data
        public APIModel<OverTimeType> GetListOverTimeTypeAPI()
        {
            var apiModel = new APIModel<OverTimeType>();
            apiModel.Data = GetListOverTimeTypeRawData();
            apiModel.Status = StatusApi.E_SUCCESSED.ToString();
            return apiModel;
        }
        public List<OverTimeType> GetListOverTimeTypeRawData()
        {
            return _dataContext.OverTimeType.ToList();
        }
        #endregion

        #region Action Shift
        public APIModel<OverTimeType> Add(OverTimeType otType)
        {
            var APIModel = new APIModel<OverTimeType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                otType.ID = Guid.NewGuid();
                otType.IsDelete = null;
                _dataContext.Add(otType);
                _dataContext.SaveChanges();
                APIModel.Data = GetListOverTimeTypeRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public OverTimeType GetByID(Guid id)
        {
            var product = new OverTimeType();
            if (id != Guid.Empty)
            {
                product = _dataContext.OverTimeType.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<OverTimeType> Update(OverTimeType otType)
        {
            var APIModel = new APIModel<OverTimeType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbOTType = _dataContext.OverTimeType.Where(s => s.ID == otType.ID).FirstOrDefault();
                if (dbOTType == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<OverTimeType> { dbOTType };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<OverTimeType> Delete(Guid id)
        {
            var APIModel = new APIModel<OverTimeType>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbOTType = GetByID(id);
                if (dbOTType == null)
                    return APIModel;
                _dataContext.OverTimeType.Remove(dbOTType);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListOverTimeTypeRawData();
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
