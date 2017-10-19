using System;
using System.Collections.Generic;

namespace FirstLab.Commands
{
    public class IterCommand : ICommand
    {
        public string Name
        {
            get { return "iterations"; }
        }

        public string Help
        {
            get { return "Задает кол-во итераций цикла определения среднего времени"; }
        }

        public string[] Synonyms
        {
            get { return new[] {"iter"}; }
        }

        /// <summary>
        /// Задает кол-во итераций цикла определения среднего времени
        /// </summary>
        /// <param name="param">Первый элемент списка - кол-во итераций</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public void Execute(List<int> param)
        {
            Data.QuantIterations = param[0];
            Console.WriteLine("Количество итераций: {0}", Data.QuantIterations);
        }
    }
}
