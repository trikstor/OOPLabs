using System;
using System.Collections.Generic;
using System.IO;

namespace FirstLab
{
    /// <summary>
    /// Класс для считывания пользовательских данных
    /// </summary>
    public class UserRobot
    {


        /// <summary>
        ///Путь к текстовому файлу (.txt) с командами
        /// </summary>
        private string Path { get; }

        /// <summary>
        /// Список строк с необработанными командами
        /// </summary>
        private List<string> CurrListComm { get; }

        /// <summary>
        /// Индекс текущей строки из текстового файла
        /// </summary>
        private int CurrInd { get; set; }

        public UserRobot(TextReader reader, bool interactive)
        {

        }
        /// <summary>
        /// В конструкторе определяется с каким IO будет работать программа,
        /// исходя из наличия пути к файлу с командами.
        /// Производится считывание команд из текстового файла.
        /// </summary>
        /// <param name="path">Путь к файлу с командами</param>
        public UserRobot(string[] args)
        {
            CurrListComm = new List<string>();
            CurrInd = 0;
            if (args.Length > 0)
            {
                Path = args[0];
                StreamReader rdr = new StreamReader(args[0]);
                string str;
                
                // Построчное чтение файла с командами
                while ((str = rdr.ReadLine()) != null)
                {
                    CurrListComm.Add(str.Replace("\n", ""));
                }
            }

            // Начало рекурсии обработки команд
            NextCommand();
        }

        /// <summary>
        /// Метод получения команды из консоли или списка, полученного после обработки
        /// текстового файла
        /// </summary>
        public void NextCommand()
        {
            // Определения источника команды
            if (Path != null)
            {
                // Перевод строки необработанной команды из файла в
                // экземпляр класса Command
                var comParam = Parser.ParseComm(CurrListComm[CurrInd]);

                // Если не пустая строка
                if (comParam != null)
                {
                    // Вызываем обработчик команды и передаем
                    var exit = _app.Run(comParam);

                    // Если весь файл не прочтен или не поступил сигнал выхода,
                    //продолжаем рекурсию
                    if (CurrListComm.Count - 1 > CurrInd || !exit)
                    {
                        CurrInd++;
                        NextCommand(); // рекурсия
                    }
                }
                // Если пустая строка, то все равно продолжаем рекурсию
                else
                {
                    NextCommand(); // рекурсия
                }
            }
            // Если источник команд - консоль
            else
            {
                // Считываем команду
                var currListComm = Console.ReadLine();

                // Перевод строки необработанной команды из консоли в
                // экземпляр класса Command                
                var param = Parser.ParseComm(currListComm);

                // Если не пустая строка
                if (param != null)
                {
                    var exit = _app.Run(param);

                    // Если не поступил сигнал выхода, то продалжаем рекурсию
                    if (!exit)
                    {
                        NextCommand(); // рекурсия
                    }
                }
                // Если пустая строка, то все равно продолжаем рекурсию
                else
                {
                    NextCommand(); // рекурсия
                }
            }
        }
    }
}