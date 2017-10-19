using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using FirstLab.Commands;

namespace FirstLab
{
    /// <summary>
    /// Класс для обработки команды и ее поиска
    /// </summary>
    class Processing
    {
        /// <summary>
        /// Список строк с необработанными командами
        /// </summary>
        private List<string> CurrListComm { get; set; }

        /// <summary>
        /// Словарь всех возможных команд, реализующих интерфейс ICommand
        /// </summary>
        public static Dictionary<string, ICommand> AllComm { get; private set; }

        // Запущено ли приложение
        private bool isRunning = true;
        private bool Interactive { get; set; }

        public List<ICommand> Commands { get; set; }

        public Processing(TextReader reader, bool interactive)
        {
            AllComm = new Dictionary<string, ICommand>();
            Commands = new List<ICommand>
            {
                {new ExitCommand(this)},
                {new HelpCommand()},
                {new TestCommand()},
                {new RandCommand()},
                {new SeqCommand()},
                {new IterCommand()},
                {new Clear()}
            };

            Interactive = interactive;

            if (interactive) return;
            string str;

            while ((str = reader.ReadLine()) != null)
            {
                CurrListComm.Add(str.Replace("\n", ""));
            }
        }

        public void AddCommands()
        {
            foreach (var currComm in Commands)
            {
                AllComm.Add(currComm.Name, currComm);
                foreach (var synonym in currComm.Synonyms)
                {
                    AllComm.Add(synonym, currComm);
                }
            }
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Run()
        {
            AddCommands();

            List<string>.Enumerator commandsFormFile = new List<string>.Enumerator();
            if(!Interactive)
                commandsFormFile = CurrListComm.GetEnumerator();

            while (isRunning)
            {
                string currStr;

                if (Interactive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("> ");
                    currStr = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    commandsFormFile.MoveNext();
                    currStr = commandsFormFile.Current;
                }

                var comParam = Parser.ParseComm(currStr);

                ICommand currComm = null;
                if (comParam != null)
                {
                    currComm = FindCommand(comParam.Name);
                }

                if (currComm != null)
                {
                    currComm.Execute(comParam.Params);
                }
            }
        }

        /// <summary>
        /// Поиск команды в словаре среди имени и его синонимов
        /// </summary>
        /// <param name="currCommand">Имя команды, заданное пользователем</param>
        /// <returns>Возвращает экземпляр класса команды</returns>
        private ICommand FindCommand(string currCommand)
        {
            currCommand = currCommand.ToLower();
            try
            {
                // Проверка наличия в словаре по имени
                if (AllComm.ContainsKey(currCommand))
                {
                    return AllComm[currCommand];
                }
                throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Команда '{0}' не найдена", currCommand);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        /// <summary>
        /// Поиск команды по ее синонимам
        /// </summary>
        /// <param name="currCommand">Имя команды, заданное пользователем</param>
        /// <returns>Возвращает экземпляр класса команды</returns>
        private ICommand DeepCommSearch(string currCommand)
        {
            // Перебор команд
            foreach (var comm in AllComm.Values)
            {
                // Перебор синонимов у каждой из команд
                foreach (var syn in comm.Synonyms)
                {
                    if (currCommand == syn)
                    {
                        return comm;
                    }
                }
            }
            return null;
        }
    }
}
