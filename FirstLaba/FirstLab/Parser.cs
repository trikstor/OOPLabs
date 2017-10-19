using System;
using System.Collections.Generic;

namespace FirstLab
{
    /// <summary>
    /// Разбиение строки по пробелам, формирование списка параметров команды
    /// вызов метода обработки команды
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Разбиение строки по пробелам, формирование списка параметров команды
        /// вызов метода обработки команды
        /// </summary>
        /// <param name="str">строка команды для парсинга</param>
        /// <returns>
        /// возвращает false если пустая строка, в таком случае завершается
        /// работа программы
        /// </returns>
        public static Command ParseComm(string str)
        {
            try
            {
                    var currListComm =
                        str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (currListComm.Length == 0)
                    {
                        return null;
                    }

                    var param = new List<int>();

                    for (var i = 1; i < currListComm.Length; i++)
                    {
                        param.Add(Convert.ToInt32(currListComm[i], 10));
                    }
                    return new Command(currListComm[0], param);
             
            }
            catch (Exception x)
            {
                Console.WriteLine("Ошибка при парсинге: {0}", x);
            }
            return null;
        }
    }
}
