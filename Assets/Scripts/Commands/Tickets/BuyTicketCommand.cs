public class BuyTicketCommand : ICommand
{
    GameController _gameController;
    BoughtTicketsPack _pack;
    int _ticketsToBuy;

    public static string LOG_SOURCE = nameof(LoadSaveGameCommand);

    public BuyTicketCommand(GameController gameController, BoughtTicketsPack pack, int ticketsToBuy)
    {
        _gameController = gameController;
        _pack = pack;
        _ticketsToBuy = ticketsToBuy;
    }

    public void Execute()
    {
        _gameController.BuyTicketPack(_pack, _ticketsToBuy);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
