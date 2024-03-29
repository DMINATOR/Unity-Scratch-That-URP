﻿using System;
using System.Linq;
/// <summary>
/// Entry point for sample scene, creates default ticket structure
/// </summary>
public class EntryPointSampleSceneCommand : ICommand
{
    GameController _controller;

    public EntryPointSampleSceneCommand(GameController controller)
    {
        _controller = controller;
    }

    public void Execute()
    {
        var ticketsPack = GameController.Instance.TicketPacksCache.Values.First();

        // Load saved settings
        var loadSaveData = new LoadSaveGameCommand(GameController.Instance);
        loadSaveData.Execute();

        // Create new save instance for this specific scene
        var saveInstance = new SaveSlotInstance
        {
            Name = "Sample Scene Instance",
            Created = DateTime.Now,
            MaxBudget = 1000,
            MoneyInTheBank = 1000,
            Current = true
        };
        var createSaveInstance = new CreateNewSaveInstanceCommand(saveInstance, GameController.Instance);
        createSaveInstance.Execute();

        // Select this ticket pack as active
        var selectTicketPack = new SelectTicketPackCommand(GameController.Instance, ticketsPack);
        selectTicketPack.Execute();

        // Buy a ticket
        var buyTicket = new BuyTicketCommand(GameController.Instance, ticketsPack, 50); // Buy 50 tickets of the first prefab
        buyTicket.Execute();

        // Present a ticket
        var presentTicket = new UnveilTicketCommand(ticketsPack);
        presentTicket.Execute();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
