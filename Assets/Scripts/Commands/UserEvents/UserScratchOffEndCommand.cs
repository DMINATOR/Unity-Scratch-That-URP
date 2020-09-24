using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScratchOffEndCommand : ICommand
{
    BoughtTicketScratchOffSurface _surface;

    public UserScratchOffEndCommand(BoughtTicketScratchOffSurface surface)
    {
        _surface = surface;
    }

    public void Execute()
    {
        Debug.Log($"Stop - UserScratchOff {Input.mousePosition}");
        _surface.StopScratching();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
