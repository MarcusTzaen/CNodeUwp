using System;

namespace CNodeUwp.Common
{
    public static class DateTimeExtensions
    {
        public static string GetDiffFromNow(this DateTime dt)
        {
            var diff = (DateTime.Now - dt.ToLocalTime());
            if (diff.Days > 0)
            {
                return $"{diff.Days}天前";
            }
            else if (diff.Hours > 0)
            {
                return $"{diff.Hours}小时前";
            }
            else if (diff.Minutes > 0)
            {
                return $"{diff.Minutes}分钟前";
            }
            return "刚刚";
        }
    }
}
