using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Guards
{
    public static class Guard
    {
        public static T NotNull<T>(T? value, string paramName) where T : class
=> value ?? throw new ArgumentNullException(paramName);


        public static string NotNullOrWhiteSpace(string? value, string paramName)
        => string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("Value cannot be null or whitespace.", paramName) : value!;


        public static decimal NonNegative(decimal value, string paramName)
        => value < 0 ? throw new ArgumentOutOfRangeException(paramName, "Value cannot be negative.") : value;
    }
}
