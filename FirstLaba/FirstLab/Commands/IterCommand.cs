using System;
using System.Collections.Generic;

namespace FirstLab.Commands
{
    public class IterCommand : ICommand
    {
        public string Name => "iterations";
        public string Help => "Задает кол-во итераций цикла определения среднего времени";
        public string[] Synonyms => new[] { "iter" };

        /// <summary>
        /// Задает кол-во итераций цикла определения среднего времени
        /// </summary>
        /// <param name="param">Первый элемент списка - кол-во итераций</param>
        /// <returns>Возвращает информационное сообщение</returns>
        public void Execute(List<int> param)
        {
            Processing.QuantIterations = param[0];
            Console.WriteLine("Количество итераций:" + Processing.QuantIterations);
        }
    }
}
