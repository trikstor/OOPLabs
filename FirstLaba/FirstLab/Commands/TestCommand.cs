using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstLab.Commands
{
    public class TestCommand : ICommand
    {
        public string Name => "test";
        public string Help => "Запуск сортировок и проверка времени их выполнения";
        public string[] Synonyms => new[] { "run", "start" };

        public void Execute(List<int> param)
        {
            try
            {
                if (Processing.QuantIterations > 0 && Processing.DataSequence.Count > 0)
                {
                    // Список для хранения среднего времени работы каждого алгоритма
                    var times = new List<double>();

                    // три сортировки - три вызова метода сортировки
                    CurrSort currSort = Sorts.Bubble;
                    times.Add(Compare(currSort));
                    currSort = Sorts.Shell;
                    times.Add(Compare(currSort));
                    currSort = Sorts.Quick;
                    times.Add(Compare(currSort));
                    ;

                    // Получение среднего арифметического времени работы алгоритма
                    // путем деления времени на кол-во итераций
                    for (var i = 0; i < 3; i++)
                    {
                        times[i] = times[i] / Processing.QuantIterations;
                    }

                    var info =
                        $"Итераций: {Processing.QuantIterations}, Размер массива: {Processing.DataSequence.Count}\n " +
                        $"Пузырек: {times[0]}мс\n Шелл: {times[1]}мс\n Быстрая сортировка: {times[2]}мс\n";

                    Console.WriteLine(info);
                }
                else
                {
                    throw new Exception("Недопустимое кол-во итераций или элементов тестовой последовательности.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Делегат для методов сортировки
        /// </summary>
        /// <param name="data">Набор чисел для проверки алгоритма</param>
        private delegate void CurrSort(int[] data);

        /// <summary>
        /// Получение общего времени работы алгоритма (для определения среднего времени работы)
        /// </summary>
        /// <param name="sort">Делегат необходимого метода</param>
        /// <returns>Общее время работы</returns>
        private double Compare(CurrSort sort)
        {
            var stopWatch = new Stopwatch(); // Таймер

            // Метод алгоритма вызывается необходимое кол-во раз
            // и засекается скорость работы алгоритма
            for (var i = 0; i < Processing.QuantIterations; i++)
            {
                stopWatch.Start();
                sort(Processing.DataSequence.ToArray());
                stopWatch.Stop();
            }

            return stopWatch.Elapsed.TotalMilliseconds;
        }
    }
}
