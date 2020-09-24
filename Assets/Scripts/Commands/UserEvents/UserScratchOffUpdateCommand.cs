using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScratchOffUpdateCommand : ICommand
{
    BoughtTicketScratchOffSurface _surface;
    Vector3 _mousePosition;

    public UserScratchOffUpdateCommand(BoughtTicketScratchOffSurface surface, Vector3 mousePosition)
    {
        _surface = surface;
        _mousePosition = mousePosition;
    }

    public void Execute()
    {
        var rayOrigin = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(rayOrigin, out raycastHit))
        {
            var collider = raycastHit.collider;

            if (collider.tag == "GameTicketPlane")
            {
                Debug.Log($"Update - UserScratchOff {Input.mousePosition}");
                Debug.DrawRay(rayOrigin.origin, rayOrigin.direction * 1000, Color.yellow, 10);

                var surface = collider.gameObject.GetComponent<BoughtTicketScratchOffSurface>();

                surface.UpdateScratching(Input.mousePosition, raycastHit);
                return;
            }
        }

        // if no longer hit the ticket, just do nothing
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
