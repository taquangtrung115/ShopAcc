namespace ShopAccBE.Model
{
    public class Product : BaseModel
    {
        public double? Price { get; set; } = null;
        public double? Amount { get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public int? YearCreate { get; set; } = null;
        public string Title { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
    }
}
