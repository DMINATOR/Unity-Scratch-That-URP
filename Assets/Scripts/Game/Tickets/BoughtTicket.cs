using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoughtTicketLocator))]
public class BoughtTicket : MonoBehaviour
{
    [Header("Constants")]

    [ReadOnly]
    [Tooltip("Seed Offset value")]
    public static int SEED_OFFSET = 100000;

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public BoughtTicketLocator Locator;

    [Tooltip("Winning Playing Area")]
    public BoughtTicketPlayingAreaBase WinningArea;


    [Header("Variables")]

    [Tooltip("Is a winning ticket")]
    public bool IsWinning;

    [Tooltip("Winning ticket details")]
    public BoughtTicketsWinnings WinningDetails;

    [Header("Status")]

    [ReadOnly]
    [Tooltip("Random generator")]
    public System.Random _random;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ShowGenerationResults()
    {
        foreach( var playingArea in Locator.PlayingAreas)
        {

        }
    }

    internal void ApplySeed(int seed, int offset)
    {
        // Set and offset random value
        _random = new System.Random(seed);
        for (var i = 0; i < offset; i++)
        {
            _random.Next();
        }

        var seedCounter = seed;
        foreach (var playingArea in Locator.PlayingAreas)
        {
            seedCounter += SEED_OFFSET;

            playingArea.ApplySeed(seedCounter, offset);
        }
    }

    internal int PickPlayingArea()
    {
        return _random.Next(0, Locator.PlayingAreas.Length);
    }

    internal void UpdatePlayingAreas(int winningAreaIndex, int prizeValue)
    {
        WinningArea = null;

        for (int i = 0; i < Locator.PlayingAreas.Length; i++)
        {
            if( i == winningAreaIndex)
            {
                WinningArea = Locator.PlayingAreas[winningAreaIndex];
                Locator.PlayingAreas[winningAreaIndex].UpdateArea(prizeValue); // Generate prize
            }
            else
            {
                Locator.PlayingAreas[i].UpdateArea(0); // No prize to generate
            }

            // Render change
            Locator.PlayingAreas[i].RenderArea();
        }
    }

    internal void GenerateWin(BoughtTicketsWinnings winningTicket)
    {
        IsWinning = true;
        WinningDetails = winningTicket;

        var winningAreaIndex = PickPlayingArea();

        UpdatePlayingAreas(winningAreaIndex, winningTicket.PrizeValue);
    }

    internal void GenerateLoose()
    {
        IsWinning = false;
        WinningDetails = new BoughtTicketsWinnings();
        WinningArea = null;

        PickPlayingArea();

        UpdatePlayingAreas(-1, 0);
    }
}
