using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed


public class SlingShotHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField]private LineRenderer rightLineRenderer;

    [SerializeField]private Transform leftStartPosition;
    [SerializeField] private Transform rightStartPosition;
    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            // will run
            DrawSlingShot();

        }
    }
    private void DrawSlingShot()
    {
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        SetLine(touchPosition);
    }
    private void SetLine(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
