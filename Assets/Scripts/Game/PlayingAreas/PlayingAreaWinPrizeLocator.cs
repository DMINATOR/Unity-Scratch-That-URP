using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingAreaWinPrizeLocator : MonoBehaviour
{
    [Tooltip("Object to use as a background")]
    public GameObject BackgroundLayer;

    [Tooltip("Game where objects will be shown")]
    public GameObject GameLayer;

    [Tooltip("Text on a game layer")]
    public TextMesh GameLayerText;

    [Tooltip("Area that will be scratched off")]
    public GameObject ScratchOffLayer;

}
