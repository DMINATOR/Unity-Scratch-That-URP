using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GameControllerLocator))]
public class GameController : MonoBehaviour, ISaveGame
{
    SaveGameData _saveData;

    // Cache of ticket packs available, ordered by name
    public Dictionary<string, BoughtTicketsPack> TicketPacksCache;

    //Not exposed

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "GameController";


    //Public instance to game controller
    public static GameController Instance = null;

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public GameControllerLocator Locator;

    /*
    [Header("Prefabs")]

    [Header("Settings")]

    [Header("Loaded Settings")]

    [Header("Variables")]


    */

    [Header("Variables")]

    [Tooltip("Ticket packs that were bought so far")]
    public List<BoughtTicketsPack> BoughtTicketPacks;

    [Header("Status")]

    [Tooltip("Current ticket packet that is played")]
    public BoughtTicketsPack CurrentTicketPack;

    [Header("Input")]

    [Tooltip("Buttons to be used somewhere")]
    [SerializeField]
    public InputButton ButtonUsedSomewhere;




    [Header("Save Instance")]

    //[Tooltip("Current Save data state")]
    //public SaveGameData SaveData;

    [Tooltip("Current Save Instance")]
    public SaveSlotInstance CurrentSaveInstance;

    public void LoadGameData()
    {
        ClearGameState();

        Log.Instance.Info(LOG_SOURCE, $"Game Data Loading");

        // Load save data to game controller
        _saveData = SaveGameController.Instance.Load();//_saveGameController.Load();

        if (_saveData != null)
        {
            // Save data already exists, pick the current save instance by default
            CurrentSaveInstance = _saveData.SaveSlots?.Where(x => x.Current == true).SingleOrDefault();
            Log.Instance.Info(LOG_SOURCE, $"Game Data Loaded from {CurrentSaveInstance}");
        }
        else
        {
            // First load, no save game data, create one
            Log.Instance.Info(LOG_SOURCE, $"Game Data First time creation");
            _saveData = new SaveGameData();
        }

        Log.Instance.Info(LOG_SOURCE, $"Game Data Loaded");

        /*

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loading");

        // Search for latest save instance
        _saveData = SaveGameController.Instance.Load();
        if (_saveData != null)
        {
            CurrentSaveInstance = _saveData.SaveSlots?.Where(x => x.Current == true).SingleOrDefault();
        }
        else
        {
            Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data First time creation");
            _saveData = new SaveGameData();
        }

        // Create new save instance if there is none
        if (CurrentSaveInstance == null)
        {
            CurrentSaveInstance = new SaveSlotInstance
            {
                Current = true
            };

            if (_saveData.SaveSlots == null)
            {
                _saveData.SaveSlots = new System.Collections.Generic.List<SaveSlotInstance>();
            }

            _saveData.SaveSlots.Add(CurrentSaveInstance);
        }
        else
        {
            Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loaded from {CurrentSaveInstance.Created}");
        }

        // Load data from save instance
        LoadLatestSaveGameData(CurrentSaveInstance);

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loaded");

        */
    }

    public void CreateNewSaveGameInstance(SaveSlotInstance saveSlotInstance)
    {
        CurrentSaveInstance = saveSlotInstance;

        if (_saveData.SaveSlots == null)
        {
            _saveData.SaveSlots = new List<SaveSlotInstance>();
        }

        _saveData.SaveSlots.Add(saveSlotInstance);
    }

    public void ClearGameState()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Clear Game Data started");

        foreach (var pack in BoughtTicketPacks)
        {
            Destroy(pack);
        }

        BoughtTicketPacks.Clear();

        Log.Instance.Info(GameController.LOG_SOURCE, $"Clear Game Data finished");
    }

    private void LoadLatestSaveGameData(SaveSlotInstance instance)
    {
       
    }

    public void SaveGameData()
    {
        /*
        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saving");

        SaveGameController.Instance.Save(_saveData);

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saved");
        */
    }

    private void Awake()
    {
        //Create instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Create cache of tickets
        CreateTicketsCache();
    }

    private void Start()
    {
       
        /*
        // Get from save
        foreach (var pack in instance.BoughtTicketPacks)
        {
            var foundBoughtTicketsPack = TicketPacksCache[pack.GameMode.Name];
            foundBoughtTicketsPack.Init(pack);

            BoughtTicketPacks.Add(foundBoughtTicketsPack);
        }
        */

        /*
        LoadGameData();

        StartGame();
        */
        // Controlled by entry point command
        Locator.GameEvents.EntryPoint();
    }

    void Update()
    {
        
    }

    private void CreateTicketsCache()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Creating tickets cache");
        if (TicketPacksCache == null)
        {
            TicketPacksCache = new Dictionary<string, BoughtTicketsPack>();
            foreach (var ticketPackPrefab in Locator.TicketPacksPrefabs)
            {
                var gameObject = Instantiate(ticketPackPrefab, Vector3.zero, Quaternion.identity, Locator.BoughtTicketsRoot.gameObject.transform);
                var boughtTicketsPack = gameObject.GetComponent<BoughtTicketsPack>();
                boughtTicketsPack.ApplySeed();

                gameObject.SetActive(false);

                TicketPacksCache[boughtTicketsPack.Name] = boughtTicketsPack;
            }
        }
    }

    public void SelectTicketPack(BoughtTicketsPack pack)
    {
        if( CurrentTicketPack != null )
        {
            // Hide the old pack
            CurrentTicketPack.gameObject.SetActive(false);
        }

        CurrentTicketPack = pack;
    }

    public void BuyTicketPack(BoughtTicketsPack pack, int ticketsToBuy)
    {
        Log.Instance.Info(LOG_SOURCE, $"Buying {ticketsToBuy} tickets for {pack.Name}");

        // Create pack if it doesn't exist
        if (BoughtTicketPacks == null)
        {
            BoughtTicketPacks = new System.Collections.Generic.List<BoughtTicketsPack>();
        }

        // Find existing pack if it exists
        if (!BoughtTicketPacks.Contains(pack))
        {
            // Add this pack
            BoughtTicketPacks.Add(pack);
        }

        // Update pack and buy new tickets
        pack.BuyTickets(ticketsToBuy);
    }
}
