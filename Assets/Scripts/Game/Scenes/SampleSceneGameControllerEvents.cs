using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneGameControllerEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EntryPoint()
    {
        var command = new EntryPointSampleSceneCommand(GameController.Instance);
        command.Execute();
    }

    public void UnveilNextTicket()
    {
        var command = new UnveilTicketCommand(GameController.Instance.CurrentTicketPack);
        command.Execute();
    }
}
