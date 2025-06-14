using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed


public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer leftLineRenderer;
    [SerializeField] private LineRenderer rightLineRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform leftStartPosition;
    [SerializeField] private Transform rightStartPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [Header("Sling Shot Stats")]
    [SerializeField] private float maxDistance = 7.5f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;

    [Header("Bird")]
    [SerializeField] private GameObject angryBirdPrefab;
    private Vector2 slingShotLinesPosition;

    private bool clickWithinThisArea;

    public void Awake()
    {
        leftLineRenderer.enabled = false;
        rightLineRenderer.enabled = false;

        SpawnAngryBird();
    }

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
        if (!leftLineRenderer.enabled && !rightLineRenderer.enabled)
        {
            leftLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;
        }
        leftLineRenderer.SetPosition(0, position);
        leftLineRenderer.SetPosition(1, leftStartPosition.position);

        rightLineRenderer.SetPosition(0, position);
        rightLineRenderer.SetPosition(1, rightStartPosition.position);
    }


    public void SpawnAngryBird()
    {
        SetLine(idlePosition.position);

        Instantiate(angryBirdPrefab, idlePosition.position, Quaternion.identity );
    }
}
