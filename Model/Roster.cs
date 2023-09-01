namespace ShopAccBE.Model
{
    public class Roster : BaseModel
    {
        public Guid UserID { get; set; }
        public string Note { get; set; }
        public string MonShiftName { get; set; }
        public string TueShiftName { get; set; }
        public string WedShiftName { get; set; }
        public string ThuShiftName { get; set; }
        public string FriShiftName { get; set; }
        public string SatShiftName { get; set; }
        public string SunShiftName { get; set; }
        public Guid? MonShiftID { get; set; }
        public Guid? TueShiftID { get; set; }
        public Guid? WedShiftID { get; set; }
        public Guid? ThuShiftID { get; set; }
        public Guid? FriShiftID { get; set; }
        public Guid? SatShiftID { get; set; }
        public Guid? SunShiftID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
