using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Interfaces
{
    public interface ICalc
    {
        int Sum(List<int> numbers);
        int Minus(List<int> numbers);
        int Multiply(List<int> numbers);
        int Divide(List<int> numbers);
    }
}
