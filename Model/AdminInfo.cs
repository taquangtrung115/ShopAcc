namespace ShopAccBE.Model
{
    public class AdminInfo : BaseModel
    {
        public string BankName { get; set; } = string.Empty;
        public string BankCode { get; set; } = string.Empty;
        public string BankNumber { get; set; } = string.Empty;
        public string AdminName { get; set; } = string.Empty;
    }
}
