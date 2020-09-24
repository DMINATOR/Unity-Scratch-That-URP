using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoughtTicketsPackLocator : MonoBehaviour
{
    [Tooltip("Gathered statistics")]
    public BoughtTicketsStatistics Statistics;

    [Tooltip("User input to apply against ticket")]
    public TicketUserInput TicketUserInput;

    [Tooltip("Current ticket")]
    public BoughtTicket Ticket;

    [Tooltip("Scratch off surface")]
    public BoughtTicketScratchOffSurface ScratchOffSurface;
}
