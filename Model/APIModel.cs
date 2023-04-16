namespace ShopAccBE.Model
{
    public class APIModel<Model>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Model> Data { get; set; }
    }
}
