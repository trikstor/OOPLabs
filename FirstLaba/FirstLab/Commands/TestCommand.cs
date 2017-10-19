using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstLab.Commands
{
    public class TestCommand : ICommand
    {
        public string Name
        {
            get { return "test"; }
        }

        public string Help
        {
            get { return "Запуск сортировок и проверка времени их выполнения"; }
        }

        public string[] Synonyms
        {
            get { return new[] {"run", "start"}; }
        }

        public void Execute(List<int> param)
        {
            try
            {
                if (Data.QuantIterations > 0 && Data.DataSequence.Count > 0)
                {
                    // Список для хранения среднего времени работы каждого алгоритма
                    var times = new List<double>();

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
                        times[i] = times[i] / Data.QuantIterations;
                    }

                    var info =
                        string.Format("Итераций: {0}, Размер массива: {1}\n ", Data.QuantIterations,
                            Data.DataSequence.Count) +
                        string.Format("Пузырек: {0}мс\n Шелл: {1}мс\n Быстрая сортировка: {2}мс\n", times[0], times[1],
                            times[2]);

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
            var stopWatch = new Stopwatch();

            // Метод алгоритма вызывается необходимое кол-во раз
            // и засекается скорость работы алгоритма
            for (var i = 0; i < Data.QuantIterations; i++)
            {
                stopWatch.Start();
                sort(Data.DataSequence.ToArray());
                stopWatch.Stop();
            }

            return stopWatch.Elapsed.TotalMilliseconds;
        }
    }
}
