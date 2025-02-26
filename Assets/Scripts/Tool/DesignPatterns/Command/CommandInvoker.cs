using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
public abstract class CommandInvoker
{
    /// <summary>
    /// Ҫ����������ջ
    /// </summary>
    protected Stack<ICommand> _undoStack = new Stack<ICommand>();

    /// <summary>
    /// Ҫ����������ջ
    /// </summary>
    protected Stack<ICommand> _redoStack = new Stack<ICommand>();

    /// <summary>
    /// ִ������
    /// </summary>
    /// <param name="command">Ҫִ�е�����</param>
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);

        _redoStack.Clear();
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void UndoCommand()
    {
        if (_undoStack.Count > 0)
        {
            ICommand activeCommand = _undoStack.Pop();
            _redoStack.Push(activeCommand);
            activeCommand.Undo();
        }
    }

    /// <summary>
    /// ������һ������������
    /// </summary>
    public void RedoCommand()
    {
        if ( _redoStack.Count > 0)
        {
            ICommand activeCommand = _redoStack.Pop();
            _undoStack.Push(activeCommand);
            activeCommand.Execute();
        }
    }
}
