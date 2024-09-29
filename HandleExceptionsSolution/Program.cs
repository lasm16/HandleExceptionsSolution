using System.Collections;
using System.Text;

namespace HandleExceptionsSolution
{
    internal class Program
    {
        private const string EndOfLine = "\r\n";
        private const string CoefficientA = "a";
        private const string CoefficientB = "b";
        private const string CoefficientC = "c";
        private const string AllowedRanged = "Допустимы числа от -2147483648 до 2147483647";

        private static int _a = 0;
        private static int _b = 0;
        private static int _c = 0;
        private static string[] lines = [
            "a: ",
            "b: ",
            "c: ",
        ];
        private static bool _parsed = false;
        private static Dictionary<string, string> _coefficientWithValue = [];

        static void Main(string[] args)
        {
            Console.WriteLine("Решаем квадратное уроавнение");
            Console.WriteLine("a * x^2 + b * x + c = 0");
            do
            {
                _coefficientWithValue.Clear();
                ClearConsole();
                Console.WriteLine("Введите значение a:");
                _coefficientWithValue.Add(CoefficientA, Console.ReadLine());
                Console.WriteLine("Введите значение b:");
                _coefficientWithValue.Add(CoefficientB, Console.ReadLine());
                Console.WriteLine("Введите значение c:");
                _coefficientWithValue.Add(CoefficientC, Console.ReadLine());
                _parsed = CheckInputCoefficients(_coefficientWithValue);
            } while (!_parsed);

            SolveProgram(_a, _b, _c);
        }

        private static void ParseToInt(string key, string value)
        {
            switch (key)
            {
                case CoefficientA: _a = int.Parse(value); break;
                case CoefficientB: _b = int.Parse(value); break;
                case CoefficientC: _c = int.Parse(value); break;
            }
        }

        private static void ClearConsole()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static bool CheckInputCoefficients(IDictionary dictionary)
        {
            var dict = (Dictionary<string, string>)dictionary;
            foreach (var pair in dict)
            {
                try
                {
                    ParseToInt(pair.Key, pair.Value);
                }
                catch (OverflowException)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(AllowedRanged);
                    return false;
                }
                catch (Exception)
                {
                    var message = $"Неверный формат параметра {pair.Key}";
                    var severity = Severity.Error;
                    FormatData(message, severity, _coefficientWithValue);
                    return false;
                }
            }
            return true;
        }

        private static void SolveProgram(int a, int b, int c)
        {
            var d = (int)Math.Pow(b, 2) - 4 * a * c;
            try
            {
                FindRoots(d, a, b);
            }
            catch (EquationException e)
            {
                var message = e.Message;
                var severity = Severity.Warning;
                FormatData(message, severity, _coefficientWithValue);
            }

        }

        private static void FormatData(string message, Severity severity, IDictionary data)
        {
            if (severity == Severity.Warning)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            var line = new string('-', 50);
            var text = new StringBuilder(line + EndOfLine + message + EndOfLine + line + EndOfLine);
            Console.WriteLine(text);

            var coefficientWithValue = (Dictionary<string, string>)data;
            foreach (var pair in coefficientWithValue)
            {
                Console.WriteLine(pair.Key + " = " + pair.Value);
            }
        }

        private static double GetX1(int d, int a, int b)
        {
            return (-b + (int)Math.Sqrt(d)) / 2 * a;
        }

        private static double GetX2(int d, int a, int b)
        {
            return (-b - (int)Math.Sqrt(d)) / 2 * a;
        }

        private static void FindRoots(int d, int a, int b)
        {
            if (d > 0)
            {
                var x1 = GetX1(d, a, b);
                Console.WriteLine($"x1 = {x1}");
                var x2 = GetX2(d, a, b);
                Console.WriteLine($"x1 = {x2}");
            }
            else if (d == 0)
            {
                var x1 = GetX1(d, a, b);
                Console.WriteLine($"x = {x1}");
            }
            else
            {
                throw new EquationException("Вещественных значений не найдено");
            }
        }

        /// <summary>
        /// Вывести курсор
        /// </summary>
        /// <param name="position"></param>
        private static void WriteCursor(int position)
        {
            Console.SetCursorPosition(0, position);
            Console.WriteLine(">");
            Console.SetCursorPosition(0, position);
        }

        /// <summary>
        /// Напечатать коэффициенты
        /// </summary>
        private static void PrintCoefficients()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }

    }
}
