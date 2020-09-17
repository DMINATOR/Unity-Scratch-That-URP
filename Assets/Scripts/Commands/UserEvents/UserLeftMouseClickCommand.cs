using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Triggers when user clicks left mouse button
public class UserLeftMouseClickCommand : ICommand
{
    Vector3 _mousePosition;

    public UserLeftMouseClickCommand(Vector3 mousePosition)
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
                // Show ray when triggered
                Debug.Log($"Click {Input.mousePosition}");
                Debug.DrawRay(rayOrigin.origin, rayOrigin.direction * 1000, Color.green, 10);

                var command = new ScratchOffTicketCommand(collider.gameObject, raycastHit);
                command.Execute();
            }
        }
    }

    public void Undo()
    {
        throw new System.NotImplementedException();
    }
}
