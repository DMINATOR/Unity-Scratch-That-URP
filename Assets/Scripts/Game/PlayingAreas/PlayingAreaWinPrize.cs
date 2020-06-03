using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayingAreaWinPrizeLocator))]
public class PlayingAreaWinPrize : BoughtTicketPlayingAreaBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public PlayingAreaWinPrizeLocator Locator;

    [Header("Status")]

    [Tooltip("Prize to show")]
    public string PrizeToShow;

    internal override void UpdateArea(int prizeValue)
    {
        base.UpdateArea(prizeValue);
    }

    public override void RenderArea()
    {
        // Render results:
        PrizeToShow = PrizeValue == 0 ? "---" : $"${PrizeValue.ToString()}";
        Locator.GameLayerText.text = PrizeToShow;
    }
}
