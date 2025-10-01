using System.Globalization;

namespace System;

public static class TimeSpanExtensions
{
    public static string Format(this TimeSpan time)
    {
        string[] orders = ["ns", "µs", "ms", "s", "ks"];

        var ns = time.TotalNanoseconds;

        var o = 0;

        while (ns > 10000)
        {
            ns /= 1000;
            o++;
        }

        return $"{ns.ToString("#,#00.0", CultureInfo.InvariantCulture)} {orders[o]}";
    }
}
