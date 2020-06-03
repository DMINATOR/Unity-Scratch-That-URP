using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tickets pack that was bought
[RequireComponent(typeof(BoughtTicketsPackLocator))]
public class BoughtTicketsPack : MonoBehaviour
{
    //Not exposed

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "GameController";



    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public BoughtTicketsPackLocator Locator;



    [Header("Variables")]

    [Tooltip("Seed used for random generation")]
    public int Seed;

    [Tooltip("Current index of generated ticket")]
    public int CurrentTicketIndex;

   
    [Space]


    [Header("Game settings")]

    [Tooltip("Total number of tickets that will be issued")]
    public int TotalTickets;

    [Tooltip("Available prizes")]
    public BoughtTicketsWinnings[] Prizes;


    [Header("Status")]

    [ReadOnly]
    [Tooltip("Random generator")]
    public System.Random _random;

    [ReadOnly]
    [Tooltip("Generated winnings")]
    public Dictionary<int, BoughtTicketsWinnings> GeneratedPrizeWinnings = new Dictionary<int, BoughtTicketsWinnings>();

    [Tooltip("Prizes won so far")]
    public GenericDictionary<int, BoughtTicketsWinnings> PrizesWon = new GenericDictionary<int, BoughtTicketsWinnings>();

    public void Init(SaveSlotBoughtTicketPack pack)
    {
        // This will be loaded from prefab collection list
        //GameMode = GameModeBase.AddModeByName( this.gameObject, pack.GameMode.Name);
    }

    #region Unity

    // Start is called before the first frame update
    void Start()
    {
        _random = new System.Random(Seed);

        Locator.Ticket.ApplySeed(Seed,CurrentTicketIndex);

        GenerateWinningTickets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    public void GenerateWinningTickets()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"{nameof(BoughtTicketsPack)} Generating winning prizes {Seed} - Start");

        for(var i = 0; i < CurrentTicketIndex; i++)
        {
            _random.Next();
        }

        GeneratedPrizeWinnings.Clear();

        var infiniteLoopCheck = 0;

        foreach (var prizeWinnings in Prizes)
        {
            for(var i = 0; i < prizeWinnings.PrizeCount; i++)
            {
                while(true)
                {
                    var randomPosition = _random.Next(0, TotalTickets - 1);
                    var positionTaken = GeneratedPrizeWinnings.ContainsKey(randomPosition);

                    if( positionTaken )
                    {
                        // Get next value, make sure we don't get stuck in a loop
                        infiniteLoopCheck++;

                        if( infiniteLoopCheck > 1000)
                        {
                            throw new System.Exception($"Infinite loop detected, iterated over {infiniteLoopCheck}");
                        }

                        continue;
                    }
                    else
                    {
                        // Assign winning value
                        GeneratedPrizeWinnings.Add(randomPosition, prizeWinnings);

                        infiniteLoopCheck = 0;
                        break;
                    }
                }

            }
        }

        Log.Instance.Info(GameController.LOG_SOURCE, $"{nameof(BoughtTicketsPack)} Generating winning prizes - Done");
    }

    public void GenerateNextTicket()
    {
        if( HasTicketsToUnveil() )
        {
            CurrentTicketIndex++;

            BoughtTicketsWinnings winningTicket;

            if (GeneratedPrizeWinnings.TryGetValue(CurrentTicketIndex, out winningTicket))
            {
                // Winning ticket
                Log.Instance.Info(GameController.LOG_SOURCE, $"{nameof(BoughtTicketsPack)} Winning ticket unlocked ({CurrentTicketIndex}) = {winningTicket.PrizeValue}");

                // Update winnings so far
                BoughtTicketsWinnings win;

                if ( PrizesWon.ContainsKey(winningTicket.PrizeValue))
                {
                    // Already exists
                    win = PrizesWon[winningTicket.PrizeValue];
                }
                else
                {
                    // Create new 
                    win = new BoughtTicketsWinnings()
                    {
                        PrizeValue = winningTicket.PrizeValue
                    };
                }

                win.PrizeCount++;
                PrizesWon[winningTicket.PrizeValue] = win;

                Locator.Ticket.GenerateWin(winningTicket);
            }
            else
            {
                // Not winning ticket
                Log.Instance.Info(GameController.LOG_SOURCE, $"{nameof(BoughtTicketsPack)} Loosing ticket unlocked ({CurrentTicketIndex})");
                Locator.Ticket.GenerateLoose();
            }

        }
    }

    public bool HasTicketsToUnveil()
    {
        return CurrentTicketIndex < TotalTickets;
    }
}
