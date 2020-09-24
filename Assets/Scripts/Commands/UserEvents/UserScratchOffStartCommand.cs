using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triggers when user clicks left mouse button
public class UserScratchOffStartCommand : ICommand
{
    Vector3 _mousePosition;

    public UserScratchOffStartCommand(Vector3 mousePosition)
    {
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
                Debug.Log($"Start - UserScratchOff {Input.mousePosition}");
                Debug.DrawRay(rayOrigin.origin, rayOrigin.direction * 1000, Color.green, 10);

                var surface = collider.gameObject.GetComponent<BoughtTicketScratchOffSurface>();

                surface.StartScratching(Input.mousePosition, raycastHit);
            }
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
