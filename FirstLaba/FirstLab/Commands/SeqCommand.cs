using System;
using System.Collections.Generic;
using System.IO;

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
            try
            {
                if (param.Count > 0)
                {
                    Data.DataSequence = param;
                    Console.WriteLine("Последовательность установлена");
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("В последователности должен быть хотя бы один элемент.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
