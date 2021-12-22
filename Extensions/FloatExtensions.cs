using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class FloatExtensions
    {
        private static readonly Random rnd = new Random();

        public static float Round(this float number, int decimalPlaces)
        {
            return (float)Math.Round(number, decimalPlaces);
        }
        public static bool IsBetween(this float value, float min, float max, bool inclusive = true)
        {
            return inclusive && value >= min && value <= max || !inclusive && value > min && value < max;
        }
        public static float GetNearest(this float value, float test1, float test2)
        {
            float difference1 = Math.Abs(Math.Abs(test1) - Math.Abs(value));
            float difference2 = Math.Abs(Math.Abs(test2) - Math.Abs(value));
            if (difference1 <= difference2)
                return test1;
            return test2;
        }
        public static float Randomize(this float value, float randomizationRate)
        {
            float baseNumber = value * randomizationRate;
            float result = rnd.NextFloat(value - baseNumber, value + baseNumber);
            return result;
        }

        public static float Map(this float value, float x1, float x2, float y1, float y2)
        {
            return y1 + (y2 - y1) * (value - x1) / (x2 - x1);
        }
    }
}