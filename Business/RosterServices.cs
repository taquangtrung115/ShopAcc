using ShopAccBE.Data;
using ShopAccBE.Model;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public class RosterServices
    {
        private readonly DataContext _dataContext;
        public RosterServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region Get List
        public List<Roster> GetListRosterRawData()
        {
            return _dataContext.Roster.ToList();
        }
        public APIModel<Roster> GetListRosterAPI()
        {
            var apiModel = new APIModel<Roster>();
            apiModel.Status = StatusApi.E_SUCCESSED.ToString();
            apiModel.Data = GetListRosterRawData();
            return apiModel;
        }
        #endregion

        #region Action Roster
        public APIModel<Roster> Add(Roster roster)
        {
            var APIModel = new APIModel<Roster>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                roster.ID = Guid.NewGuid();
                roster.IsDelete = null;
                _dataContext.Add(roster);
                _dataContext.SaveChanges();
                APIModel.Data = GetListRosterRawData();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public Roster GetByID(Guid id)
        {
            var product = new Roster();
            if (id != Guid.Empty)
            {
                product = _dataContext.Roster.Where(s => s.ID == id).FirstOrDefault();
                return product;
            }
            else
            {
                return null;
            }
        }
        public APIModel<Roster> Update(Roster roster)
        {
            var APIModel = new APIModel<Roster>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbRoster = _dataContext.Roster.Where(s => s.ID == roster.ID).FirstOrDefault();
                if (dbRoster == null)
                    return APIModel;
                _dataContext.SaveChanges();
                APIModel.Data = new List<Roster> { dbRoster };
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                return APIModel;
            }
            catch (Exception ex)
            {
                APIModel.Message = ex.Message.ToString();
                return APIModel;
            }
        }
        public APIModel<Roster> Delete(Guid id)
        {
            var APIModel = new APIModel<Roster>();
            APIModel.Status = StatusApi.E_FAILED.ToString();
            try
            {
                var dbRoster = GetByID(id);
                if (dbRoster == null)
                    return APIModel;
                _dataContext.Roster.Remove(dbRoster);
                _dataContext.SaveChanges();
                APIModel.Status = StatusApi.E_SUCCESSED.ToString();
                APIModel.Data = GetListRosterRawData();
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
