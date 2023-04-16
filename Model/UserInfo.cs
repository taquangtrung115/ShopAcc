namespace ShopAccBE.Model
{
    public class UserInfo : BaseModel
    {
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
