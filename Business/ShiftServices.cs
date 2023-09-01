using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class ShiftServices
    {
        private readonly DataContext _dataContext;
        public ShiftServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get Raw Data
        public APIModel<Shift> GetListShiftAPI()
        {
            var apiModel = new APIModel<Shift>();
            apiModel.Data = GetListShiftRawData();
            apiModel.Status = StatusApi.E_SUCCESSED.ToString();
            return apiModel;
        }
        public List<Shift> GetListShiftRawData()
        {
            return _dataContext.Shift.ToList();
        }
        #endregion

        #region Action Shift
        public APIModel<Shift> Add(Shift shift)
        {
            var APIModel = new APIModel<Shift>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                shift.ID = Guid.NewGuid();
                shift.IsDelete = null;
                _dataContext.Add(shift);
                _dataContext.SaveChanges();
                APIModel.Data = GetListShiftRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public Shift GetByID(Guid id)
        {
            var product = new Shift();
            if (id != Guid.Empty)
            {
                product = _dataContext.Shift.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<Shift> Update(Shift shift)
        {
            var APIModel = new APIModel<Shift>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbShift = _dataContext.Shift.Where(s => s.ID == shift.ID).FirstOrDefault();
                if (dbShift == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<Shift> { dbShift };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<Shift> Delete(Guid id)
        {
            var APIModel = new APIModel<Shift>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbShift = GetByID(id);
                if (dbShift == null)
                    return APIModel;
                _dataContext.Shift.Remove(dbShift);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = _dataContext.Shift.ToList();
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
