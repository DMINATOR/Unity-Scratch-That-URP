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

    public void EntryPoint(GameController controller)
    {
        var command = new EntryPointSampleSceneCommand(controller);
        command.Execute();
    }
}
