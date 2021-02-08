using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    class Program
    {
        public static string ToString(int[] args)
        {
            var result = "";
            for (var i = 0; i < args.Length; i++)
            {
                if (i == 0)
                { 
                    result = args[i].ToString();
                    continue;
                }

                result =  $"{result} {args[i]}";
            }
            return result;
        }

        static void Main(string[] args)
        {
            var something = new int[5]{10,20,30,40,50};
            var s = ToString(something);//+7 8 7
            var text = "Привет, чертов мир!";
            //string.StartsWith  - ПРОВЕРКА!!! Что строка начинается с указанных символом
            var index = text.IndexOf("чертов");
            var length = "чертов".Length;
            Console.WriteLine($"{text.Substring(0, index-1)}{text.Substring(index+length)}");
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
