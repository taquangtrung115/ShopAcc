namespace ShopAccBE.Model
{
    public class BaseModel
    {
        public Guid ID { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string UserUpdate { get; set; }
        public string ActionStatus { get; set; }
        public string Name { get; set; }
        public bool? IsDelete { get; set; }
    }
}
