using System.Collections.Generic;

namespace OperatorOverloading
{
    class ComplexComparer : IComparer<Complex>
    {
        public int Compare(Complex x, Complex y)
        {
            return x.Real.CompareTo(y.Real);
        }
    }
}
