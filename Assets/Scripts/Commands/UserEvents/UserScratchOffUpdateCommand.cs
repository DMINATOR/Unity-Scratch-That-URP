using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScratchOffUpdateCommand : ICommand
{
    Vector3 _mousePosition;

    public UserScratchOffUpdateCommand(Vector3 mousePosition)
    {
        _mousePosition = mousePosition;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
