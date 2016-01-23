using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimension;
            int[] mass;
            CreateMass(out dimension, out mass);
            Console.WriteLine("Исходный массив: \n");
            foreach (int k in mass)
            {
                Console.Write(" " + k);
            }
            Console.WriteLine("\n");

            QuickSort(mass, 0, dimension-1);
            Console.WriteLine("Отсортированный массив: \n");
            foreach (int k in mass)
            {
                Console.Write(" " + k);
            }
            Console.WriteLine("\n");
        }

    static void QuickSort(int[] array, int start, int end)
    {
        int midElement = array[(start + end) / 2];
        int i = start;
        int j = end;
        while (i <= j)
        {
            while (array[i] < midElement)
            {
                i++;
            }              
            while (array[j] > midElement)
            {
                j--;
            }
            if (i <= j)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                if(j > 0) // т.к. j может выходить за пределы массива. Например при сортировке массива {0, 0, -1, 6, 8, -7};
                {
                    j--;
                }
                //Console.WriteLine(j);
            }
        }

        // рекурсивные вызовы, если есть, что сортировать
        if (i < end) QuickSort(array, i, end); // правая (Например, в тако массиве {1, 2, 10, 4, 3} правая рекурсия не сработает, т.к. i = end)
        if (j > start) QuickSort(array, start, j); // левая
        }

    static void CreateMass(out int N ,out int[] arr)
        {
            Console.WriteLine("Введите размерность массива:");
            N = Int32.Parse(Console.ReadLine());
            arr = new int[N];
            int count = N;
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("Введите число (осталось ввести {0} чисел):", count);
                arr[i] = Int32.Parse(Console.ReadLine());
                count --;
            }
            Console.Clear();
            
            //Random rand = new Random();
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = rand.Next(-100, 100);
            //}
        }
    }
}
