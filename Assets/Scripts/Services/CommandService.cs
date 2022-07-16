using Solitaire.Commands;
using System.Collections.Generic;
using UniRx;

namespace Solitaire.Services
{
    public class CommandService
    {
        public BoolReactiveProperty CanUndo { get; private set; } = new BoolReactiveProperty();

        readonly Stack<ICommand> _commands = new Stack<ICommand>();

        public void AddCommand(ICommand command)
        {
            if (command == null)
            {
                return;
            }

            _commands.Push(command);
            CanUndo.Value = _commands.Count > 0;
        }

        public void UndoCommand()
        {
            if (_commands.Count > 0)
            {
                _commands.Pop().Undo();
                CanUndo.Value = _commands.Count > 0;
            }
        }

        public void Reset()
        {
            _commands.Clear();
            CanUndo.Value = false;
        }
    }
}
