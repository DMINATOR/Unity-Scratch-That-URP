public class BuyTicketCommand : ICommand
{
    SaveSlotInstance _instance;

    public BuyTicketCommand(SaveSlotInstance instance)
    {
        _instance = instance;
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
