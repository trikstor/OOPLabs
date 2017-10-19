using System;
using System.Collections.Generic;
using System.IO;

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
            try
            {
                if (param.Count <= 0)
                {
                    throw new IndexOutOfRangeException();
                }
                if (param[0] > 0)
                {
                    Data.QuantIterations = param[0];
                    Console.WriteLine("Количество итераций: {0}", Data.QuantIterations);
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Должен быть один параметр.");
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Заданы некорректные данные, " +
                                  "параметр должен быть целочисленным и больше 0.");
            }
        }
    }
}
