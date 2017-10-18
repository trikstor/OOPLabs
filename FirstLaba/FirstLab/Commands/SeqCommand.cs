using System;
using System.Collections.Generic;

namespace FirstLab.Commands
{
    public class SeqCommand : ICommand
    {
        public string Name => "sequence";
        public string Help => "Задает последовательность для обработки алгоритмами";
        public string[] Synonyms => new[] { "Seq" };

        /// <summary>
        /// Задает последовательность для обработки алгоритмами
        /// </summary>
        /// <param name="param">Список чисел для обработки алгоритмами</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public void Execute(List<int> param)
        {
            Processing.DataSequence = param;
            Console.WriteLine("Последовательность установлена");
        }
    }
}
