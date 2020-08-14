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
        Log.Instance.Info(LOG_SOURCE, $"Buying {_ticketsToBuy} tickets for {_pack.Name}");

        // Create pack if it doesn't exist
        if (_gameController.BoughtTicketPacks == null)
        {
            _gameController.BoughtTicketPacks = new System.Collections.Generic.List<BoughtTicketsPack>();
        }

        // Find existing pack if it exists
        if( !_gameController.BoughtTicketPacks.Contains(_pack))
        {
            // Add this pack
            _gameController.BoughtTicketPacks.Add(_pack);
        }

        // Update pack and buy new tickets
        _pack.BuyTickets(_ticketsToBuy);
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
