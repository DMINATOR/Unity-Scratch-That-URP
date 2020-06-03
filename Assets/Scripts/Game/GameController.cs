using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GameControllerLocator))]
public class GameController : MonoBehaviour, ISaveGame
{
    SaveGameData _saveData;

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

    [Header("Status")]
    */

    [Header("Variables")]

    [Tooltip("Ticket packs that were bought so far")]
    public List<BoughtTicketsPack> BoughtTicketPacks;

    [Header("Input")]

    [Tooltip("Buttons to be used somewhere")]
    [SerializeField]
    public InputButton ButtonUsedSomewhere;


    [Header("Save Instance")]

    [Tooltip("Current Save Instance")]
    public SaveSlotInstance CurrentSaveInstance;

    public void LoadGameData()
    {
        ClearGameState();

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
        foreach(var pack in instance.BoughtTicketPacks)
        {
            var gameObject = Instantiate(Locator.BoughtTicketsPackPrefab, Vector3.zero, Quaternion.identity, Locator.BoughtTicketsRoot.gameObject.transform);
            var boughtTickets = gameObject.GetComponent<BoughtTicketsPack>();
            boughtTickets.Init(pack);

            BoughtTicketPacks.Add(boughtTickets);
        }
    }

    public void SaveGameData()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saving");

        SaveGameController.Instance.Save(_saveData);

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saved");
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
    }

    private void Start()
    {
        LoadGameData();

        StartGame();
    }

    void Update()
    {
        
    }

    private void StartGame()
    {
        
    }
}
