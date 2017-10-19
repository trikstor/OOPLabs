using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstLab
{
    public class CommandsHandler
    {
        /// <summary>
        /// Кол-во итераций цикла для определения среднего времени работы алгоритмов
        /// </summary>
        private int QuantIterations { get; set; }

        /// <summary>
        /// Последовательность для обработки алгоритмами
        /// </summary>
        private List<int> DataSequence { get; set; }
        
        /// <summary>
        /// Выводит описание программы и команд
        /// </summary>
        /// <returns>Возвращает описание</returns>
        public string Help()
        {
            var info =
                "Данная программа позволяет определить скорость различных видов сортировок целых чисел.";
            return info;
        }

        /// <summary>
        /// Задает кол-во итераций цикла определения среднего времени
        /// </summary>
        /// <param name="param">Первый элемент списка - кол-во итераций</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public string Iterations(List<int> param)
        {
            QuantIterations = param[0];

            var info =
                "Количество итераций:" + QuantIterations;
            return info;
        }

        /// <summary>
        /// Задает последовательность для обработки алгоритмами
        /// </summary>
        /// <param name="param">Список чисел для обработки алгоритмами</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public string Sequence(List<int> param)
        {
            DataSequence = param;

            var info =
                "Последовательность установлена";
            return info;
        }

        /// <summary>
        ///  Добавление последовательности случайных чисел для проверки алгоритмов
        /// </summary>
        /// <param name="param">
        /// Параметры, первый из которых может указывать кол-во случайных чисел,
        /// если список пуст то генерируется 1000 случ. чисел.
        /// </param>
        /// <returns>Возвращает уведомление о выполнении операции</returns>
        public string RandSec(List<int> param)
        {
            // Кол-во случайных чисел
            // Если кол-во случайных чисел в списке не указано то будет 1000
            var currParam = 1000;

            //Создание объекта для генерации случайных чисел
            Random rnd = new Random();

            DataSequence = new List<int>();

            // Если существует параметр, который обоначает кол-во чисел в массиве то
            //  используем его
            if (param.Count > 0)
            {
                currParam = param[0];
            }

            for (var i = 0; i < currParam; i++)
            {
                // Получить случайное число (в диапазоне от 0 до 10)
                DataSequence.Add(rnd.Next(0, 10));
            }

            var info =
                "Последовательность установлена";
            return info;
        }

        /// <summary>
        /// Делегат для методов сортировки
        /// </summary>
        /// <param name="data">Набор чисел для проверки алгоритма</param>
        private delegate void CurrSort(int[] data);

        public string Test()
        {
            // Список для хранения среднего времени работы каждого алгоритма
            var times = new List<double>();

            CurrSort currSort = Sorts.Bubble;

            // три сортировки - три вызова функции сортировки
            times.Add(Compare(currSort));
            currSort = Sorts.Shell;
            times.Add(Compare(currSort));
            currSort = Sorts.Quick;
            times.Add(Compare(currSort));
            currSort = Sorts.Radix;
            times.Add(Compare(currSort));
            /*(currSort = Sorts.HeapSort;
            times.Add(Compare(currSort));
            */

            // Получение среднего арифметического времени работы алгоритма
            // путем деления времени на кол-во итераций
            for (var i = 0; i < 3; i++)
            {
                times[i] = times[i] / QuantIterations;
            }

            var info =
                $"Итераций: {QuantIterations}, Размер массива: {DataSequence.Count}\n " +
                $"Пузырек: {times[0]}мс\n Шелл: {times[1]}мс\n Быстрая сортировка: {times[2]}мс\n" +
                $" Поразрядная: {times[3]}мс\n";
            return info;
        }

        /// <summary>
        /// Получение общего времени работы алгоритма (для определения среднего времени работы)
        /// </summary>
        /// <param name="sort">Делегат необходимого метода</param>
        /// <returns>Общее время работы</returns>
        private double Compare(CurrSort sort)
        {
            double times = 0.0; // Общее время работы алгоритма
            var stopWatch = new Stopwatch(); // Таймер

            // Метод алгоритма вызывается необходимое кол-во раз
            // и суммируются скорости работы алгоритма
            for (var i = 0; i < QuantIterations; i++)
            {
                stopWatch.Restart();
                sort(DataSequence.ToArray());
                stopWatch.Stop();

                var ts = stopWatch.Elapsed;
                // Прибавляем время работы алгоритма в миллисекундах
                times += ts.TotalMilliseconds;
            }
            return times;
        }
    }
}
