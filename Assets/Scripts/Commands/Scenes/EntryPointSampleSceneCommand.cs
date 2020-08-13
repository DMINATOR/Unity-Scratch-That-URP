using System;
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
        // Load saved settings
        var loadSaveData = new LoadSaveGameCommand(SaveGameController.Instance, GameController.Instance);
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

        // Buy a ticket
        var buyTicket = new BuyTicketCommand(_controller.CurrentSaveInstance);
        buyTicket.Execute();

        // Present a ticket
        var presentTicket = new PresentTicketCommand();
        presentTicket.Execute();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
