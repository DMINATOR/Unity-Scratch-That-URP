using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rewrite this stuff to work better 
        if (Input.GetMouseButtonDown(0))
        {
            var command = new UserLeftMouseClickCommand(Input.mousePosition);
            command.Execute();
        }
    }
}
