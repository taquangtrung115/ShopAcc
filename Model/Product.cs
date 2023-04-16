namespace ShopAccBE.Model
{
    public class Product : BaseModel
    {
        public string Price { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? YearCreate { get; set; }
        public int? TotalFriend { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
    }
}
