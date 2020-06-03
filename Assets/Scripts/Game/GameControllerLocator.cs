using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLocator : MonoBehaviour
{
    [Tooltip("Contains currently bought tickets")]
    public GameObject BoughtTicketsRoot;

    [Header("Prefabs")]

    [Tooltip("Prefab to create Bought tickets packs")]
    public GameObject BoughtTicketsPackPrefab;
}
