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
        _controller.LoadGameData();

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
