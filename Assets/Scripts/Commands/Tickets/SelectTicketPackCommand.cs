using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Selects ticket pack to play
public class SelectTicketPackCommand : ICommand
{
    BoughtTicketsPack _pack;
    GameController _gameController;

    public SelectTicketPackCommand(GameController gameController, BoughtTicketsPack pack)
    {
        _pack = pack;
        _gameController = gameController;
    }

    public void Execute()
    {
        _gameController.SelectTicketPack(_pack);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
