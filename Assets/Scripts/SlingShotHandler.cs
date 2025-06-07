using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed


public class SlingShotHandler : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            // will run
            DrawSlingShot();
        }
    }
    private void DrawSlingShot()
    {

    }
}
