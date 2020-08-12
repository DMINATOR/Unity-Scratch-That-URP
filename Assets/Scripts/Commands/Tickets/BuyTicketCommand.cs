public class BuyTicketCommand : ICommand
{
    SaveSlotInstance _instance;

    public BuyTicketCommand(SaveSlotInstance instance)
    {
        _instance = instance;
    }

    public void Execute()
    {
        //_instance.BoughtTicketPacks
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
