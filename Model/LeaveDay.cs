namespace ShopAccBE.Model
{
    public class LeaveDay : BaseModel
    {
        public double LeaveDays { get; set; }
        public double LeaveHours { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public Guid LeaveDayTypeID { get; set; }
        public string LeaveDayTypeName { get; set; }
        public string DurationType { get; set; }
        public string Note { get; set; }
    }
}
