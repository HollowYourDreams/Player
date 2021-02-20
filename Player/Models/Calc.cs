using System;
using System.Collections.Generic;
using System.Linq;
using Player.Interfaces;

namespace Player.Models
{
    public class Calc : ICalc
    {
        // | & - вычисляет ОБЕ части 
        // ||  && - вычисляет, пока не false
        public int Sum(List<int> numbers)
        {
            if (numbers == null || !numbers.Any())
            {
                return 0;
            }
            numbers.ForEach(x => Console.WriteLine(x));
            return numbers.Sum();
        }

        public int Minus(List<int> numbers)
        {
            throw new System.NotImplementedException();
        }

        public int Multiply(List<int> numbers)
        {
            throw new System.NotImplementedException();
        }

        public int Divide(List<int> numbers)
        {
            throw new System.NotImplementedException();
        }
    }
}
