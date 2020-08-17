using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadSaveGameCommand : ICommand
{
    GameController _gameController;

    public static string LOG_SOURCE = nameof(LoadSaveGameCommand);

    public LoadSaveGameCommand(GameController gameController)
    {
        _gameController = gameController;
    }

    public void Execute()
    {
        _gameController.LoadGameData();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
