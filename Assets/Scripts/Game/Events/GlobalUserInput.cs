using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUserInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO - Rewrite to use actual keys

        if (Input.GetMouseButtonDown(0))
        {
            var commandDown = new UserScratchOffStartCommand(Input.mousePosition);
            commandDown.Execute();
        }

        if( GameController.Instance.CurrentTicketPack.Locator.Ticket)
        if( Input.GetMouseButtonUp(0))
        {
            var commandUp = new UserScratchOffEndCommand();
            commandUp.Execute();
        }
    }
}
