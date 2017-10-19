using System;
using System.Collections.Generic;

namespace FirstLab.Commands
{
    public class SeqCommand : ICommand
    {
        public string Name
        {
            get { return "sequence"; }
        }

        public string Help
        {
            get { return "Задает последовательность для обработки алгоритмами"; }
        }

        public string[] Synonyms
        {
            get { return new[] {"seq"}; }
        }

        /// <summary>
        /// Задает последовательность для обработки алгоритмами
        /// </summary>
        /// <param name="param">Список чисел для обработки алгоритмами</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public void Execute(List<int> param)
        {
            Data.DataSequence = param;
            Console.WriteLine("Последовательность установлена");
        }
    }
}
