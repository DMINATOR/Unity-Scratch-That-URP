using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerLocator : MonoBehaviour
{
    [Header("Prefabs")]
    [Tooltip("Events that are declared and controlled by game controller")]
    public SampleSceneGameControllerEvents GameEvents;

    [Tooltip("Contains currently bought tickets")]
    public GameObject BoughtTicketsRoot;

    [Header("Prefabs")]

    [Tooltip("Available ticket pack prefabs")]
    public List<GameObject> TicketPacksPrefabs;

    //[Tooltip("Prefab to create Bought tickets packs")]
    //public GameObject BoughtTicketsPackPrefab;
}
