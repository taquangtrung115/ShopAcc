namespace ShopAccBE.Model
{
    public class Shift : BaseModel
    {
        public string Code { get; set; }
        public string ShiftName { get; set; }
        public DateTime InTime { get; set; }
        public DateTime CoOut { get; set; }
        public double WorkHours { get; set; }
        public DateTime BreakInTime { get; set; }
        public DateTime BreakOutTime { get; set; }
        public bool? IsNightShift { get; set; }
    }
}
