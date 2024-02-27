using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using ShopAccBE.Data;
using ShopAccBE.Model;
using System.Linq;
using System.Reflection;
using static ShopAccBE.Data.EnumConstant;

namespace ShopAccBE.Business
{
    public static class BaseServices
    {
        private static TValue GetFieldValue<TValue>(this object instance, string name)
        {
            var type = instance.GetType();
            var field = type.GetFields(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance).FirstOrDefault(e => typeof(TValue).IsAssignableFrom(e.FieldType) && e.Name == name);
            return (TValue)field?.GetValue(instance);
        }
        public static string Validate<TEntity>(TEntity source)
        {
            var message = string.Empty;
            if (source == null)
                return ValidateCode.E_NULL.ToString();
            else
            {
                if (typeof(TEntity).Name == ConstantText.E_SHIFT)
                {
                    var code = GetFieldValue<string>(source, "Code");
                    var workHours = GetFieldValue<double>(source, "WorkHours");
                    var inTime = GetFieldValue<string>(source, "InTime");
                    var outTime = GetFieldValue<string>(source, "OutTime");
                    var breakInTime = GetFieldValue<string>(source, "BreakInTime");
                    var breakOutTime = GetFieldValue<string>(source, "BreakOutTime");
                    if (string.IsNullOrEmpty((string?)code))
                        return "Code cannot be null!";
                    if (workHours == null || workHours == 0 || workHours < 0)
                        return "Work Hours need to long than zero!";
                    if (string.IsNullOrEmpty(outTime))
                        return "Out time can be not null!";
                    if (string.IsNullOrEmpty(breakOutTime))
                        return "Break out time can be not null!";
                }
                else if (typeof(TEntity).Name == ConstantText.E_ROSTER)
                {

                }
                else if (typeof(TEntity).Name == ConstantText.E_LEAVEDAY_TYPE)
                {

                }
                else if (typeof(TEntity).Name == ConstantText.E_LEAVEDAY)
                {

                }
                else if (typeof(TEntity).Name == ConstantText.E_OVERTIME_TYPE)
                {

                }
                else if (typeof(TEntity).Name == ConstantText.E_OVERTIME)
                {

                }
                else if (typeof(TEntity).Name == ConstantText.E_DAYOFF)
                {

                }
            }
            return message;
        }
        public static void UpdateFieldBase<TEntity>(TEntity source)
        {
            TrySetProperty(source, "DateUpdate", DateTime.Now);
            TrySetProperty(source, "IsDelete", null);
            TrySetProperty(source, "ID", Guid.NewGuid());
            TrySetProperty(source, "UserUpdate", "temp");
        }
        private static void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }
    }
}
