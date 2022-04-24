using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Calculator.Models
{
    static class Calculate
    {
        public static double Multiply(double a, double b) => a*b;
        public static decimal SelfDivide(decimal a) => 1/a;
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Divide(double a, double b) => a/b;

    }
}
