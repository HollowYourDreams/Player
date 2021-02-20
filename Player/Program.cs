using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Player.Models;

namespace Player
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new Calc();
            Console.WriteLine("Введите числа для суммирования или \"end\" для выхода из программы");
            var isStopped = false;
            var numbers = new List<int>();
            while (!isStopped)
            {
                var text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text) || text.ToLower() == "END".ToLower())
                {
                    break;
                }
                if (int.TryParse(text, out var res))
                {
                    numbers.Add(res);
                }
                else
                {
                    Console.WriteLine("Вы ввели недопустимое значение!");
                    Console.WriteLine("Введите число или \"end\" ");
                }
            }

            var sum = calc.Sum(new List<int>{1,2});
            Console.WriteLine(sum);

            Console.ReadKey();
        }
    }
}
