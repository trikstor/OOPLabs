﻿using System;
using System.Collections.Generic;

namespace FirstLab.Commands
{
    public class HelpCommand : ICommand
    {
        public string Name => "help";
        public string Help => "Помощь по программе";
        public string[] Synonyms => new string[] { "?", "wtf" };

        public void Execute(List<int> param)
        {
            Console.WriteLine("Данная программа позволяет определить" +
                              "скорость различных видов сортировок целых чисел.\n" +
                              "Список команд:");
            foreach (var command in Processing.AllComm.Values)
            {
                Console.WriteLine($"\t{command.Name} - {command.Help}");

                Console.WriteLine("\t\tСинонимы команды:");
                foreach (var syn in command.Synonyms)
                {
                    Console.WriteLine("\t\t\t" + syn);
                }
            }
        }
    }
}
