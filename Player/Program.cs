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
        private static char[] _alpha;
        public static char[] Alpha { get; set; }
        //public static int A {get;set;}- объявление
        //int a   - объявление
        //a = 0; - присвоение
        //int a = 0; - присвоение с объявлением
        //var a = 0; - аналогично выше
        //public static int A {get;set;} = 0; - присвоение с объявлением значения по-умолчанию
        // new char[] - инициализация (ищем по слову new)

        static void Main(string[] args)
        {
            var t = new int[2,2] {{00,01}, {10,11} };
            var dict = new Dictionary<int, string>
            {
                {
                    1,
                    "Никита"
                },
                {
                    2,
                    "Степа"
                },
                {
                    3,
                    "Настя"
                }
            };
            try
            {
                throw new KeyNotFoundException("VSEM KIRDIK");
                dict.Add(3, "sdsdd");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("dfgsdg");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("PRIVED");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("dfgsdg");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("dfgsdg");
            }
            catch (Exception ex)
            {
                Console.WriteLine("dfgsdg");
            }
            dict.Values.ToList().ForEach(Console.WriteLine);
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
