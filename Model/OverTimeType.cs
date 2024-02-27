namespace ShopAccBE.Model
{
    public class OverTimeType : BaseModel
    {
        public string Code { get; set; }
        public string OverTimeTypeName { get; set; }
        public double Coefficient { get; set; }
    }
}
