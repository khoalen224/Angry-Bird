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
    [SerializeField] private float maxDistance = 2.5f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;

    [Header("Bird")]
    [SerializeField] private GameObject angryBirdPrefab;
    [SerializeField] private float angryBirdOffset = 0.06f; 

    private Vector2 slingShotLinesPosition;
    private Vector2 direction;
    private Vector2 directionNormalized;


    private bool clickWithinThisArea;
    private GameObject spawnedAngryBird;

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
            PostioningTheAngryBird();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            clickWithinThisArea = false;
        }
    }

    #region AngryBridLaunch()
    public void SpawnAngryBird()
    {
        Vector2 direction = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPos = (Vector2)idlePosition.position + direction * angryBirdOffset;
        SetLine(idlePosition.position);

        spawnedAngryBird = Instantiate(angryBirdPrefab, spawnPos, Quaternion.identity);
        spawnedAngryBird.transform.right = direction;
    }
    private void PostioningTheAngryBird()
    {
        spawnedAngryBird.transform.position = slingShotLinesPosition+ directionNormalized* angryBirdOffset ;
        spawnedAngryBird.transform.right = directionNormalized;
    }

    #endregion

    #region DrawSlingShot()
    private void DrawSlingShot()
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        slingShotLinesPosition = centerPosition.position + Vector3.ClampMagnitude(touchPosition - centerPosition.position, maxDistance );
        SetLine(slingShotLinesPosition);

        direction = (Vector2)centerPosition.position - slingShotLinesPosition;
        directionNormalized = direction.normalized;
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
    #endregion
}
