namespace Scio.Web.Helpers
{
    using System;
    using System.Collections.Generic;

    public static class TimeSpanParser
    {
        public static string Parse(DateTime date)
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime().Subtract(date);

            string prefix = ts.Ticks < 0 ? "in " : string.Empty;
            string postfix = ts.Ticks > 0 ? " ago" : string.Empty;

            var dict = new Dictionary<string, int>
            {
                ["year"] = ts.Days / 365,
                ["month"] = ts.Days / 30,
                ["week"] = ts.Days / 7,
                ["day"] = ts.Days,
                ["hour"] = ts.Hours,
                ["minute"] = ts.Minutes,
                ["second"] = ts.Seconds,
            };

            foreach (var item in dict)
            {
                var value = Math.Abs(item.Value);
                var type = $" {item.Key}{(value > 1 ? "s" : string.Empty)}";

                if (value != 0)
                {
                    return string.Format("{0}{1}{2}{3}", prefix, value, type, postfix);
                }
            }

            return "just now";
        }
    }
}
