using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed


public class SlingShotHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;

    [SerializeField] private Transform leftStartPosition;
    [SerializeField] private Transform rightStartPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [SerializeField] private float maxDistance = 7.5f;

    [SerializeField] private SlingShotArea slingShotArea; 

    private Vector2 slingShotLinesPosition;

    private bool clickWithinThisArea;

    // Update is called once per frame
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingShotArea())
        {
           //this
           clickWithinThisArea = true;
        }
        if (Mouse.current.leftButton.isPressed && clickWithinThisArea)
        {
            // will run
            DrawSlingShot();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickWithinThisArea = false;
        }
    }
    private void DrawSlingShot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        slingShotLinesPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance );
        SetLine(slingShotLinesPosition);
    }
    private void SetLine(Vector2 position)
    {
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }
}
