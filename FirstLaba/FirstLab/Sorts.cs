﻿using System;
using System.Linq;

namespace FirstLab
{
    ///<summary>
    ///Класс Sorts - 
    ///класс программы,
    ///содержащий сортировки
    ///</summary>
    public class Sorts
    {
        ///<summary>
        ///Вспомогательный метод, "меняет местами" два элемента
        ///</summary>
        ///<param name = "aFirstArg">Первый меняемый элемент</param>
        ///<param name = "aSecondArg">Второй меняемый элемент</param>
        ///<returns>
        ///Ничего не возвращает
        ///</returns>
        public static void Swap(ref int aFirstArg, ref int aSecondArg)
        {
            //Временная (вспомогательная) переменная, хранит значение первого элемента
            int tmpParam = aFirstArg;

            //Первый аргумент получил значение второго
            aFirstArg = aSecondArg;

            //Второй аргумент, получил сохраненное ранее значение первого
            aSecondArg = tmpParam;
        }

        ///<summary>
        ///Метод сортировки пузырьком
        ///</summary>
        ///<param name = "data">Сортируемый массив</param>
        ///<returns>
        ///Ничего не возвращает
        ///</returns>
        public static void Bubble(int[] data)
        {
            //Основной цикл (количество повторений равно количеству элементов массива)
            for (int i = 0; i < data.Length; i++)
            {
                //Вложенный цикл (количество повторений, равно количеству элементов массива минус 1 и минус количество выполненных повторений основного цикла)
                for (int j = 0; j < data.Length - 1 - i; j++)
                {
                    //Если элемент массива с индексом j больше следующего за ним элемента
                    if (data[j] > data[j + 1])
                    {
                        //Меняем местами элемент массива с индексом j и следующий за ним
                        Swap(ref data[j], ref data[j + 1]);
                    }
                }
            }
        }

        ///<summary>
        ///Сортировка методом Шелла
        ///</summary>
        ///<param name = "data">Сортируемый массив</param>
        ///<returns>
        ///Ничего не возвращает
        ///</returns>
        public static void Shell(int[] data)
        {
            int curr; //доп. элемент для обмена элементов
            int step; //шаг разбиения массива

            /*
            Шаг разбиения step - равен половине размера массива и больше нуля,
            так как иначе (если не > 0) массив уже явл. упорядоченным.
            После каждой итерации данного цикла шаг делим пополам до тех пор,
            пока он не будет равен 1 (ведь это будет означать, что мы рассмотрели весь
            массив как единую группу):
            */
            for (step = data.Length / 2; step > 0; step = step / 2)
            {
                /*
                ограничиваем i равную шагу разбиения размером массива, 
                чтобы не выйти за пределы размера массива:
                (т.к. массив не знает своего размера)
                */
                for (int i = step; i < data.Length; i++)
                {
                    /*
                    j - изначально будет равен индексу первого элемента массива,
                    и после каждой итерации он будет увеличиваться на единицу,
                    а с помощью предыдущего цикла ему не удасться выйти за пределы массива:
                    (тем самым мы учтем все элементы массива)
                    */
                    for (int j = i - step; j >= 0; j = j - step)
                    {
                        /*
                        С помощью предыдущих циклов, мы сравним все элементы по группам,
                        в которых будет по 2 элемента из массива
                        (1-ый j - очередной элемент, а 2-ой j + step - расстояние между j и step).
                        => Если значение в индексе j больше значения в индексе j + step данного массива, то:
                        */
                        if (data[j] > data[j + step])
                        {
                            //производим обмен местами этих элементов:
                            curr = data[j];
                            data[j] = data[j + step];
                            data[j + step] = curr;
                        }
                    }
                }
            }
        }

        ///<summary>
        ///Метод быстрой сортировки
        ///</summary>
        ///<param name = "data">Сортируемый массив</param>
        ///<returns>
        ///Ничего не возвращает
        ///</returns>
        public static void Quick(int[] data)
        {
            QuickSort(data, 0, data.Length - 1);
        }

        public static void DefaultSort(int[] data)
        {
            Array.Sort(data); //встроенный метод
        }

        private static void QuickSort(int[] arr, int first, int last)
        {
            int p = arr[(last - first) / 2 + first];
            int temp;
            int i = first, j = last;
            while (i <= j)
            {
                while (arr[i] < p && i <= last) ++i;
                while (arr[j] > p && j >= first) --j;
                if (i <= j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    ++i; --j;
                }
            }
            if (j > first) QuickSort(arr, first, j);
            if (i < last) QuickSort(arr, i, last);
        }
    }
}