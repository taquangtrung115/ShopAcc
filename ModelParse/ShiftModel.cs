using ShopAccBE.Model;

namespace ShopAccBE.ModelParse
{
    public class ShiftModel : BaseModel
    {
        public string Code { get; set; }
        public string ShiftName { get; set; }
        public string InTime { get; set; }
        public string CoOut { get; set; }
        public double WorkHours { get; set; }
        public string BreakInTime { get; set; }
        public string BreakOutTime { get; set; }
        public bool? IsNightShift { get; set; }
    }
}
