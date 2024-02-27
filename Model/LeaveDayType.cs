namespace ShopAccBE.Model
{
    public class LeaveDayType : BaseModel
    {
        public string LeaveDayTypeName { get; set; }
        public string Code { get; set; }
        public double MaxPerMonth { get; set; }
        public double IsAnlLeave { get; set; }
        public double IsSickLeave { get; set; }
        public double IsPreLeave { get; set; }
        public double IsNormalLeave { get; set; }
    }
}
