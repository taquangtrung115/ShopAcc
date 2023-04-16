﻿namespace ShopAccBE.Model
{
    public class UserInfo : BaseModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserLogin { get; set; }
        public string Phone { get; set; } = string.Empty;
        public byte[] PasssWordHash { get; set; }
        public byte[] PasssWordSalth { get; set; }
    }
}