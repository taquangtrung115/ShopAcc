using ShopAccBE.Data;
using ShopAccBE.Model;
using ShopAccBE.ModelParse;
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

        #region Get Hour and Minute
        public List<int> GetHourAndMinute(string time)
        {
            var lstInt = new List<int>();
            int hour = 0;
            int minute = 0;

            if (!string.IsNullOrEmpty(time))
            {
                lstInt.Add(hour);
                lstInt.Add(minute);
                return lstInt;
            }

            var lstTime = time.Split(":");
            int.TryParse(lstTime[0], out hour);
            int.TryParse(lstTime[1], out minute);
            lstInt.Add(hour);
            lstInt.Add(minute);
            return lstInt;
        }
        #endregion

        #region Action Shift
        public APIModel<Shift> Add(ShiftModel shift)
        {
            var APIModel = new APIModel<Shift>();
            APIModel.Status = StatusApi.E_FAILED.ToString();

            #region Validate
            var messValidate = BaseServices.Validate<ShiftModel>(shift);
            if (!string.IsNullOrEmpty(messValidate))
            {
                APIModel.Message = messValidate;
                return APIModel;
            }
            #endregion
            try
            {
                var dateNow = DateTime.Now.Date;
                var shiftSave = new Shift();

                shiftSave.WorkHours = shift.WorkHours;
                shiftSave.IsNightShift = shift.IsNightShift;

                #region In and Out and Break
                #region In Time
                var hourAndMinutesIn = GetHourAndMinute(shift.InTime);
                shiftSave.InTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hourAndMinutesIn[0], hourAndMinutesIn[1], 0);
                #endregion

                #region Out Time
                var hourAndMinutesOut = GetHourAndMinute(shift.CoOut);
                shiftSave.CoOut = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hourAndMinutesOut[0], hourAndMinutesOut[1], 0);
                #endregion

                #region Break In Time
                var hourAndMinutesBreakIn = GetHourAndMinute(shift.BreakInTime);
                shiftSave.BreakInTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hourAndMinutesBreakIn[0], hourAndMinutesBreakIn[1], 0);
                #endregion

                #region Break Out Time
                var hourAndMinutesBreakOut = GetHourAndMinute(shift.BreakOutTime);
                shiftSave.BreakOutTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, hourAndMinutesBreakOut[0], hourAndMinutesBreakOut[1], 0);
                #endregion
                #endregion

                BaseServices.UpdateFieldBase<Shift>(shift);

                _dataContext.Add(shiftSave);
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
                APIModel.Data = GetListShiftRawData();
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
