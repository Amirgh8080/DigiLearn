﻿namespace Common.Application
{
    public static class NumberHelper
    {
        public static string TooMan(this int price, bool isFree= false)
        {
            if (price == 0 && isFree)
                return "رایگان";
            return $"{price:#,0} تومان";
        }
        public static string TooMan(this int? price)
        {
            return $"{price:#,0} تومان";
        }
        public static string SplitNumber(this int price)
        {
            return $"{price:#,0}";
        }
        public static string SplitNumber(this int? price)
        {
            return $"{price:#,0}";
        }
    }
}