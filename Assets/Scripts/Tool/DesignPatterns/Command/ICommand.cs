using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

/// <summary>
/// ????
/// </summary>
public interface ICommand
{
    /// <summary>
    /// ????
    /// </summary>
    void Execute();

    /// <summary>
    /// ????
    /// </summary>
    void Undo();
}
