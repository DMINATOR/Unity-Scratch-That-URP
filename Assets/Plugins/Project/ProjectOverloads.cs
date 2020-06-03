using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SAVE GAME overloads
public partial class SaveGameData : DKAsset
{
    public List<SaveSlotInstance> SaveSlots;
}


[System.Serializable]
public class SaveSlotInstance
{
    // Indicates that this save slot is current
    public bool Current;

    // Name of the game
    public string Name;

    // Budget
    public int MaxBudget;

    // Money in the bank
    public int MoneyInTheBank;

    // Details about all the ticket packs bought
    public List<SaveSlotBoughtTicketPack> BoughtTicketPacks;

    // Info
    public DateTimeSerializer Created;
    public DateTimeSerializer Modified;
}


[System.Serializable]
public class SaveSlotBoughtTicketPack
{
    // Name of the game mode
    public SaveSlotGameMode GameMode;
}

[System.Serializable]
public class SaveSlotGameMode
{
    // Name of the game mode
    public string Name;

    // Seed
    public int Seed;
}