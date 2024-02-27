namespace ShopAccBE.Model
{
    public class Bill : BaseModel
    {
        public Guid? ProductID { get; set; }
        public Guid? UserInfoID { get; set; }
        public string Status { get; set; }
        public string MethodPayMent { get; set; }
        public string Total { get; set; }
    }
}
