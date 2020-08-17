using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewSaveInstanceCommand : ICommand
{
    SaveSlotInstance _saveSlotInstance;
    GameController _gameController;

    public static string LOG_SOURCE = nameof(LoadSaveGameCommand);

    public CreateNewSaveInstanceCommand(SaveSlotInstance saveSlotInstance, GameController gameController)
    {
        _saveSlotInstance = saveSlotInstance;
        _gameController = gameController;
    }

    public void Execute()
    {
        _gameController.CreateNewSaveGameInstance(_saveSlotInstance);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
