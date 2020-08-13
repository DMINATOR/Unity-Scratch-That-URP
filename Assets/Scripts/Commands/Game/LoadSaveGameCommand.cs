using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadSaveGameCommand : ICommand
{
    SaveGameController _saveGameController;
    GameController _gameController;

    public static string LOG_SOURCE = nameof(LoadSaveGameCommand);

    public LoadSaveGameCommand(SaveGameController saveGameController, GameController gameController)
    {
        _saveGameController = saveGameController;
        _gameController = gameController;
    }

    public void Execute()
    {
        Log.Instance.Info(LOG_SOURCE, $"Game Data Loading");

        // Load save data to game controller
        _gameController.SaveData = _saveGameController.Load();

        if (_gameController.SaveData != null)
        {
            // Save data already exists, pick the current save instance by default
            _gameController.CurrentSaveInstance = _gameController.SaveData.SaveSlots?.Where(x => x.Current == true).SingleOrDefault();
            Log.Instance.Info(LOG_SOURCE, $"Game Data Loaded from {_gameController.CurrentSaveInstance}");
        }
        else
        {
            // First load, no save game data, create one
            Log.Instance.Info(LOG_SOURCE, $"Game Data First time creation");
            _gameController.SaveData = new SaveGameData();
        }

        //if (currentSaveInstance == null)
        //{
        //    // No save instances created yet
        //}
        //else
        //{
        //    Log.Instance.Info(LOG_SOURCE, $"Game Data Loaded from {currentSaveInstance.Created}");
        //}

        Log.Instance.Info(LOG_SOURCE, $"Game Data Loaded");
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
