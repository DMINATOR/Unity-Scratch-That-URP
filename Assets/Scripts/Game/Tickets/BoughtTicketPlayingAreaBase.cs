using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoughtTicketPlayingAreaBase : MonoBehaviour
{
    [Header("Constants")]
    [TextArea]
    [Tooltip("Short description of a playing area")]
    public string DESCRIPTION;



    [Header("Variables")]

    [Tooltip("Seed used for this playing area generation")]
    public int Seed;

    [Tooltip("Winning Prizes that this playing area can provide")]
    public int[] WinningPrizes;



    [Header("Status")]

    [ReadOnly]
    [Tooltip("Random generator")]
    public System.Random _random;

    [ReadOnly]
    [Tooltip("Current Prize Value or 0")]
    public int PrizeValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ApplySeed(int seed, int offset)
    {
        Seed = seed;
        _random = new System.Random(seed);

        for (var i = 0; i < offset; i++)
        {
            UpdateArea(0);
        }
    }

    internal virtual void UpdateArea(int prizeValue)
    {
        PrizeValue = prizeValue;
        _random.Next();
    }


    public abstract void RenderArea();
}
