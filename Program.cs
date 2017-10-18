using System;
using System.IO;
/* Лабораторная работа №1: Сравнение работы алгоритмов.
 * Исполнители: Зотов Антон, Сергеев Виктор. Группа: ФО - 260002
 */
namespace FirstLab
{
    class Program
    {
        private static void Main(string[] args)
        {
            var reader = args.Length > 0 ? File.OpenText(args[0]) : Console.In;
            var robot = new Processing(reader, interactive : args.Length == 0);
            robot.Run();

        }
    }
}
