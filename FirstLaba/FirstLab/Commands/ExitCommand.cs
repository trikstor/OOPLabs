using System.Collections.Generic;

namespace FirstLab.Commands
{
    class ExitCommand : ICommand
    {
        public string Name => "exit";
        public string Help => "Выход из программы";
        public string[] Synonyms => new[] { "ext", "out" };

        public void Execute(List<int> param)
        {
        }
    }
}
