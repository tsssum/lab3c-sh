using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_16
{
    internal class Program
    {
        static int enter_size()
        {
            Console.Write("Введите количество точек: ");
            int size = 2 * Convert.ToInt32(Console.ReadLine());
            return size;
        }
        static double[] create_array(int size)
        {
            double[] arr = new double[size];
            Console.WriteLine("Введите координаты каждой точки поочерёдно черед пробел:");
            string[] points = Console.ReadLine().Split(' ');
            for (int i = 0; i < size; ++i)
                arr[i] = double.Parse(points[i]);
            return arr;
        }

        static bool triangle_exist(double[] arr, int size)
        {
            bool flag = false;
            
            for (int i = 0; i < size-5; i+=2) //-5 потому что иду тройками: последняя тройка включает в себя 6 элементов массива
            {
                const double epsilon = 1e-10;
                if (Math.Abs((arr[i] - arr[i + 2]) * (arr[i + 5] - arr[i + 1]) - (arr[i + 1] - arr[i + 3]) * (arr[i + 4] - arr[i])) > epsilon)
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }
        static double distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        static double task(double[] arr, int n, double[] min_points)
        {
            double minP = double.MaxValue;

            for (int i = 0; i < n-4; i+=2) //потому что если пойдёт дальше, не получит треугольника
            {
                for (int j = i + 2; j < n-2; j+=2)
                {
                    for (int k = j + 2; k < n; k+=2)
                    {
                        double perimeter = distance(arr[i], arr[i+1], arr[j], arr[j + 1]) +
                                           distance(arr[j], arr[j + 1], arr[k], arr[k + 1]) +
                                           distance(arr[k], arr[k + 1], arr[i], arr[i + 1]);

                        if (perimeter < minP)
                        {
                            minP = perimeter;
                            min_points[0] = arr[i];
                            min_points[1] = arr[i+1];
                            min_points[2] = arr[j];
                            min_points[3] = arr[j+1];
                            min_points[4] = arr[k];
                            min_points[5] = arr[k+1];
                        }
                    }
                }
            }

            return minP;
        }
        static void print_array(double[] arr)
        {
            for (int i = 0; i < arr.Length; i+=2)
                Console.Write(arr[i] + " " + arr[i+1] +'\n');
            Console.WriteLine();
        }
        static void Main()
        {
            int option;
            do
            {
                Console.WriteLine("Меню\n1. Решение задачи\n2. Завершение работы\n");

                bool isErrorOp = true;
                do
                {
                    if (int.TryParse(Console.ReadLine(), out option) && (option >= 1 && option <= 2))
                        isErrorOp = false;
                    else
                        Console.WriteLine("Ошибка! Вводимое значение должно быть 1 или 2. Попробуйте заново :)\n");
                } while (isErrorOp);

                if (option != 2)
                {
                    int size = enter_size();
                    double[] arr = create_array(size);
                    double[] min_points = new double[6];

                    if (size < 3 || !triangle_exist(arr, size))
                        Console.Write("Невозможно построить треугольник");

                    else
                    {
                        double minR = task(arr, size, min_points);

                        Console.Write("Периметр: " + minR);
                        Console.Write("\nТочки:\n");
                        print_array(min_points);
                    }

                    Console.WriteLine("\nНажмите у или Y, если хотите завершить программу:");
                    string isExit = Console.ReadLine();
                    if (isExit == "y" || isExit == "Y")
                        option = 2;
                }
            } while (option != 2);
        }
    }
}
