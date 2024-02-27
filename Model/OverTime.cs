namespace ShopAccBE.Model
{
    public class OverTime : BaseModel
    {
        public Guid UserID { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime InTime { get; set; }
        public double RegisterHours { get; set; }
        public Guid? ShiftID { get; set; }
        public Guid OverTimeTypeID { get; set; }
        public string OverTimeTypeName { get; set; }
        public string Note { get; set; }
    }
}
