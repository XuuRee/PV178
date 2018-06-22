using System;

namespace OperatorOverloading
{
    public class Complex : IComparable<Complex>
    {
        public double Real { get; }

        public double Imag { get; }

        public Complex(double real, double imag)
        {
            Real = real;
            Imag = imag;
        }

        public static Complex operator +(Complex first, Complex second)
        {
            return new Complex(first.Real + second.Real, first.Imag + second.Imag);
        }

        public int CompareTo(Complex other)
        {
            // ignore the imag parts
            return this.Real.CompareTo(other.Real);
        }
    }
}
