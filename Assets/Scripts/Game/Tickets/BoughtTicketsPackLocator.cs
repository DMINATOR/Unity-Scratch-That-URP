using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtTicketsPackLocator : MonoBehaviour
{
    [Tooltip("Gathered statistics")]
    public BoughtTicketsStatistics Statistics;

    [Tooltip("Plane to use for Ticket")]
    public GameObject TicketPlane;

    [Tooltip("Current ticket")]
    public BoughtTicket Ticket;
}
