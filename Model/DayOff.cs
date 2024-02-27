namespace ShopAccBE.Model
{
    public class DayOff : BaseModel
    {
        public DateTime DateOff { get; set; }
        public string Code { get; set; }
        public string DayOffName { get; set; }
    }
}
