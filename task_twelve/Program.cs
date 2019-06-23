using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_twelve
{
    class Program
    {
        static int countRear1 = 0, countCompare2 = 0, countCompare1 = 0, countRear2 = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Сортировка слиянием");
            Console.Write("Введите элементы массива: ");
            string[] arr1 = new string[1];
            try
            { arr1 = Console.ReadLine().Split(new[] { " ", ",", ";" }, StringSplitOptions.RemoveEmptyEntries); }
            catch (FormatException) { Console.WriteLine("Ошибка при вводе!"); }
            int[] array1 = new int[arr1.Length];
            int[] array2 = new int[arr1.Length];
            for (int i = 0; i < arr1.Length; i++)
            {
                array1[i] = Convert.ToInt32(arr1[i]);
                array2[i] = Convert.ToInt32(arr1[i]);
            }
            Console.WriteLine("Неотсортированный массив.");
            Console.WriteLine("Упорядоченный массив 1: {0}", string.Join(", ", MergeSort(array1)));
            Console.WriteLine("Упорядоченный массив 2: {0}", string.Join(", ", BucketSort(array2)));
            Console.WriteLine($"Количество перестановок:\nдля метода слияния: сравнения - {countCompare1}, перестановки - {countRear1}\nдля блочного метода: сравнения - {countCompare2}, перестановки - {countRear2}.");

            countRear1 = 0; countCompare2 = 0; countCompare1 = 0; countRear2 = 0;
            Array.Sort(array1); Array.Sort(array2);
            Console.WriteLine("\nОтсортированный массив по возрастанию.");
            Console.WriteLine("Упорядоченный массив 1: {0}", string.Join(", ", MergeSort(array1)));
            Console.WriteLine("Упорядоченный массив 2: {0}", string.Join(", ", BucketSort(array2)));
            Console.WriteLine($"Количество перестановок:\nдля метода слияния: сравнения - {countCompare1}, перестановки - {countRear1}\nдля блочного метода: сравнения - {countCompare2}, перестановки - {countRear2}.");

            countRear1 = 0; countCompare2 = 0; countCompare1 = 0; countRear2 = 0;
            Console.WriteLine("\nОтсортированный массив по убыванию.");
            Array.Reverse(array1); Array.Reverse(array2);
            Console.WriteLine("Упорядоченный массив 1: {0}", string.Join(", ", MergeSort(array1)));
            Console.WriteLine("Упорядоченный массив 2: {0}", string.Join(", ", BucketSort(array2)));
            Console.WriteLine($"Количество перестановок:\nдля метода слияния: сравнения - {countCompare1}, перестановки - {countRear1}\nдля блочного метода: сравнения - {countCompare2}, перестановки - {countRear2}.");

            Console.ReadLine();
        }


        //метод для слияния массивов
        static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            int[] tempArray = new int[highIndex - lowIndex + 1];
            int index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                    countCompare1++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                    countCompare1++;
                }

                index++;
            }

            for (int i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
                countRear1++;
            }

            for (int i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
                countRear1++;
            }

            for (int i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
                countRear1++;
            }
        }

        //сортировка слиянием
        static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
            return array;
        }

        static int[] MergeSort(int[] array)
        {
            return MergeSort(array, 0, array.Length - 1);
        }

        static int[] BucketSort(int[] items)
        {
            // Предварительная проверка элементов исходного массива
            if (items == null || items.Length < 2)
                return items;

            // Поиск элементов с максимальным и минимальным значениями
            int maxValue = items[0];
            int minValue = items[0];

            for (int i = 1; i < items.Length; i++)
            {
                if (items[i] > maxValue)
                {
                    countRear2++;
                    maxValue = items[i];
                }

                if (items[i] < minValue)
                {
                    countRear2++;
                    minValue = items[i];
                }
            }

            // Создание временного массива "карманов" в количестве,
            // достаточном для хранения всех возможных элементов,
            // чьи значения лежат в диапазоне между minValue и maxValue.
            // Т.е. для каждого элемента массива выделяется "карман" List<int>.
            // При заполнении данных "карманов" элементы исходного не отсортированного массива
            // будут размещаться в порядке возрастания собственных значений "слева направо".
            //

            List<int>[] bucket = new List<int>[maxValue - minValue + 1];

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            // Занесение значений в пакеты
            for (int i = 0; i < items.Length; i++)
            {
                bucket[items[i] - minValue].Add(items[i]);
                countCompare2++;
                countRear2++;
            }

            // Восстановление элементов в исходный массив из карманов в порядке возрастания значений
            int position = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        items[position] = bucket[i][j];
                        position++;
                        countCompare2++;
                    }
                }
            }
            return items;
        }
    }
}
