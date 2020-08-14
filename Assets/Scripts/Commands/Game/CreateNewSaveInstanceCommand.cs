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
        _gameController.CurrentSaveInstance = _saveSlotInstance;

        if (_gameController.SaveData.SaveSlots == null)
        {
            _gameController.SaveData.SaveSlots = new List<SaveSlotInstance>();
        }

        _gameController.SaveData.SaveSlots.Add(_saveSlotInstance);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
