using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtTicketsPackLocator : MonoBehaviour
{
    [Tooltip("Gathered statistics")]
    public BoughtTicketsStatistics Statistics;

    [Tooltip("Current ticket")]
    public BoughtTicket Ticket;
}
