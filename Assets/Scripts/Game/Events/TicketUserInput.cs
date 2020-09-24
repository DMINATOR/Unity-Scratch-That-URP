using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketUserInput : MonoBehaviour
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public BoughtTicketsPackLocator Locator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Locator.ScratchOffSurface.IsScratchingStarted)
        {
            // Currently scratching

            if (Input.GetMouseButtonUp(0))
            {
                // Button removed
                var commandUp = new UserScratchOffEndCommand(Locator.ScratchOffSurface);
                commandUp.Execute();
            }
            else
            {
                // Moving mouse
                var mousePosition = Input.mousePosition;

                if (mousePosition != Locator.ScratchOffSurface.ScratchOffMousePosition.Value)
                {
                    var commandUp = new UserScratchOffUpdateCommand(Locator.ScratchOffSurface, Input.mousePosition);
                    commandUp.Execute();
                }
            }
        }
        else
        {
            // Not scratching
            if (Input.GetMouseButtonDown(0))
            {
                var commandDown = new UserScratchOffStartCommand(Input.mousePosition);
                commandDown.Execute();
            }
        }
        /*
        // TODO - Rewrite to use actual keys

        if (Input.GetMouseButtonDown(0))
        {
            var commandDown = new UserScratchOffStartCommand(Input.mousePosition);
            commandDown.Execute();
        }

        if (GameController.Instance.CurrentTicketPack.Locator.Ticket)
            if (Input.GetMouseButtonUp(0))
            {
                var commandUp = new UserScratchOffEndCommand();
                commandUp.Execute();
            }
            */
    }
}
