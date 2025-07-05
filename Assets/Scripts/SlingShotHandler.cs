using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed
using DG.Tweening;

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
    [SerializeField] private Transform elasticTransform;

    [Header("Sling Shot Stats")]
    [SerializeField] private float maxDistance = 2.5f;
    [SerializeField] private float shotForce = 10f;
    [SerializeField] private float birdLaunchDelay = 2f;
    [SerializeField] private float elasticDivider = 1.2f;
    [SerializeField] private AnimationCurve elasticCurve;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea slingShotArea;

    [Header("Bird")]
    [SerializeField] private AngryBird angryBirdPrefab;
    [SerializeField] private float angryBirdOffset = 0.06f;

    [SerializeField] private CameraManager cameraManager;

    private Vector2 slingShotLinesPosition;
    private Vector2 direction;
    private Vector2 directionNormalized;


    private bool clickWithinThisArea;
    private AngryBird spawnedAngryBird;
    private bool birdOnSlingShot = true;

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
            if (birdOnSlingShot)
            {
                cameraManager.SwitchToFollowCam(spawnedAngryBird.transform);
            }

        }
        if (Mouse.current.leftButton.isPressed && clickWithinThisArea && birdOnSlingShot)
        {
            // will run
            DrawSlingShot();
            UpdateAngryBirdPosition();
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame && birdOnSlingShot && clickWithinThisArea)
        {
            if (GameManager.Instance.HasEnoughBirds())
            {
                clickWithinThisArea = false;
                birdOnSlingShot = false;

                spawnedAngryBird.LaunchBird(direction, shotForce);
                GameManager.Instance.UseBird();
                AnimateSlingsshot();

                if (GameManager.Instance.HasEnoughBirds())
                {
                    StartCoroutine(SpawingAngryBirdAfter()); 
                }
            }
        }
    }

    #region AngryBirdLaunch()
    public void SpawnAngryBird()
    {
        Vector2 direction = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPos = (Vector2)idlePosition.position + direction * angryBirdOffset;
        SetLine(idlePosition.position);

        spawnedAngryBird = Instantiate(angryBirdPrefab, spawnPos, Quaternion.identity);
        spawnedAngryBird.transform.right = direction;

        birdOnSlingShot = true;
    }
    private void UpdateAngryBirdPosition()
    {
        spawnedAngryBird.transform.position = slingShotLinesPosition+ directionNormalized* angryBirdOffset ;
        spawnedAngryBird.transform.right = directionNormalized;
    }

    private IEnumerator SpawingAngryBirdAfter()
    {
        yield return new WaitForSeconds(birdLaunchDelay);
        SpawnAngryBird();
        cameraManager.SwitchToIdleCam();

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

    #region Animation
    private void AnimateSlingsshot()
    {
        elasticTransform.position = leftLineRenderer.GetPosition(0);
        float distance = Vector2.Distance(elasticTransform.position, centerPosition.position);

        float time = distance /elasticDivider;
        elasticTransform.DOMove(centerPosition.position, time).SetEase(elasticCurve);

        StartCoroutine(AnimationLines(elasticTransform, time));
    }

    private IEnumerator AnimationLines(Transform trans, float time)
    {
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            SetLine(trans.position);

            yield return null;
        }
    }
    #endregion
}
