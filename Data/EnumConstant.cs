using ShopAccBE.Model;

namespace ShopAccBE.Data
{
    public class EnumConstant
    {
        public enum StatusApi
        {
            E_SUCCESSED,
            E_FAILED
        }
        public enum ValidateCode
        {
            //model null
            E_NULL,
        }
        public enum Major
        {
            E_SHIFT,
            E_ROSTER,
            E_LEAVEDAY_TYPE,
            E_LEAVEDAY,
            E_OVERTIME_TYPE,
            E_OVERTIME,
            E_DAYOFF,
        }
        #region Enum Core
        #region Roster
        public enum RosterType
        {
            //TKB hàng ngày
            E_ROSTER_SCHEDULE,
            //Đổi TKB
            E_ROSTER_CHANGE_SCHEDULE,
        }
        #endregion

        #region Leave Day
        public enum DurationType
        {
            //Toàn ca
            E_FULL_SHIFT,
            //Nữa sau ca
            E_LAST_SHIFT,
            //Nữa ca trước
            E_FIRST_SHIFT,
        }
        public enum DayOffType
        {
            //Lễ nhà nước
            E_HOLIDAYS,
            //Lễ cty
            E_HOLIDAYS_COMPANY
        }
        #endregion

        #region Overt Time
        public enum OverTimeDurationType
        {
            //OT 100%
            E_OT_100,
            //OT 150%
            E_OT_150,
            //OT 200%
            E_OT_200,
            //OT 250%
            E_OT_250,
            //OT 300%
            E_OT_300,
        }
        #endregion
        #endregion

    }
    public class ConstantText
    {
        public static string E_ROSTER = "Roster";
        public static string E_SHIFT = "Shift";
        public static string E_LEAVEDAY_TYPE = "LeaveDayType";
        public static string E_LEAVEDAY = "LeaveDay";
        public static string E_OVERTIME_TYPE = "OverTimeType";
        public static string E_OVERTIME = "OverTime";
        public static string E_DAYOFF = "DayOff";
    }
}
