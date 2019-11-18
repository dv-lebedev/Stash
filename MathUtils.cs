using System;
using System.Linq;
using System.Windows;


public static class MathUtils
{
    /// <summary>
    /// Is points sorted by freq
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static bool IsSortedByFreq(Point[] points)
    {
        if (points == null) throw new ArgumentNullException(nameof(points));
        if (points.Length == 0) throw new ArgumentException("points.Length == 0");

        if (points.Length == 1) return true;


        for(int i = 0; i < points.Length - 1; i++)
        {
            if (points[i].X > points[i + 1].X) return false;
        }

        return true;
    }


    /// <summary>
    /// Get approximated point by freq using points from array
    /// Array should be sorted by freq
    /// </summary>
    /// <param name="points"></param>
    /// <param name="freq"></param>
    /// <returns></returns>
    public static Point GetApproxi(Point[] points, double freq)
    {
        if (points == null) throw new ArgumentNullException(nameof(points));
        if (points.Length == 0) throw new ArgumentException("points.Length == 0");

        if (freq < 0 || double.IsNaN(freq) || double.IsInfinity(freq))
            throw new ArgumentOutOfRangeException(nameof(freq));

        int first = 0;
        int last = points.Length - 1;

        //если искомая частота за пределами диапазона
        if (freq < points[first].X)
            return new Point(freq, points[first].Y);

        if (freq > points[last].X)
            return new Point(freq, points[last].Y);

        int mid = 0;
        int prevMid = 0;

        while (true)
        {
            mid = (first + last) / 2;
            if (prevMid == mid) break;
            prevMid = mid;

            Point midValue = points[mid];

            if (midValue.X == freq) return midValue;
            else if (midValue.X > freq) last = mid;
            else if (midValue.X < freq) first = mid;
        }

        Point left = points[first];
        Point right = points[last];

        double value = left.Y +
           (Math.Log10(freq) - Math.Log10(left.X)) / (Math.Log10(right.X) - Math.Log10(left.X)) *
           ((right.Y - left.Y) / 1.0);

        if (double.IsNaN(value) || double.IsInfinity(value)) value = 0;

        return new Point(freq, value);
    }
}
