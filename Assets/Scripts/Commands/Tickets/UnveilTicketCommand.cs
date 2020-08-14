using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unveils next ticket from the pack
/// </summary>
public class UnveilTicketCommand : ICommand
{
    BoughtTicketsPack _pack;

    public UnveilTicketCommand(BoughtTicketsPack pack)
    {
        _pack = pack;
    }

    public void Execute()
    {
        _pack.UnveilNextTicket();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
