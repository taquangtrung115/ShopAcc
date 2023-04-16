namespace ShopAccBE.Model
{
    public class BaseModel
    {
        public Guid ID { get; set; }
        public DateTime? DateUpdate { get; set; } = null;
        public string UserUpdate { get; set; } = string.Empty;
        public bool? IsDelete { get; set; } = null;
    }
}
