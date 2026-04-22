using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickPocketManager : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionAsset inputActionAsset;
    private InputAction interactAction;
    private InputAction confirmPickpocketAction;

    [Header("UI Elements")]
    [SerializeField] private GameObject pickPocketMiniGameUI;
    [SerializeField] private RectTransform redBarUI;
    [SerializeField] private RectTransform greenBarUI;
    [SerializeField] private RectTransform indicatorUI;

    private float successZoneCenter;
    private float successZoneHalfWidth;
    private float leftRedBound;
    private float rightRedBound;
    
    private bool isPlaying = false;
    private bool isGameEnded = false;

    [Header("Pickpocket Settings")]
    [SerializeField] private float moveSpeed = 300f;
    private float currentIndicatorPosition;
    private int currentDirection = 1;
    private PickpocketTarget currentTarget;

    [SerializeField] private float minSuccessSize = 5f;
    [SerializeField] private float maxSuccessSize = 20f;

    [Header("Raycast Settings")]
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float interactDistance = 3.0f;
    [SerializeField] private LayerMask npcLayer;

    [Header("Failure Settings")]
    private bool isFailed; 
    [HideInInspector] public bool isPoliceAlerted; 
    [SerializeField] private NPCPolice police;

    #region Input Setup

    private void OnEnable()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("InputActionAsset missing!");
            return;
        }

        var actionMap = inputActionAsset.FindActionMap("Player");

        if (actionMap == null)
        {
            Debug.LogError("Player ActionMap not found!");
            return;
        }

        interactAction = actionMap.FindAction("Pickpocket");
        confirmPickpocketAction = actionMap.FindAction("PickpocketConfirm");

        if (interactAction == null || confirmPickpocketAction == null)
        {
            Debug.LogError("Input actions missing!");
            return;
        }

        interactAction.performed += OnInteract;
        confirmPickpocketAction.performed += OnConfirmPickpocket;

        interactAction.Enable();
        confirmPickpocketAction.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.performed -= OnInteract;
            interactAction.Disable();
        }

        if (confirmPickpocketAction != null)
        {
            confirmPickpocketAction.performed -= OnConfirmPickpocket;
            confirmPickpocketAction.Disable();
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("INTERACT PRESSED");

        if (!TryFindNPC())
        {
            Debug.Log("NO NPC HIT");
            return;
        }

        Debug.Log("NPC FOUND");

        if (currentTarget.hasBeenPickpocketed)
        {
            Debug.Log("ALREADY PICKPOCKETED");
            return;
        }

        StartPickpocketMinigame();
    }

    #endregion

    #region Find NPC

    private bool TryFindNPC()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("HIT: " + hit.collider.name);

            PickpocketTarget target =
                hit.collider.GetComponentInParent<PickpocketTarget>();

            if (target != null)
            {
                Debug.Log("VALID NPC FOUND");
                currentTarget = target;
                return true;
            }
            else
            {
                Debug.Log("NO PickpocketTarget on object");
            }
        }
        else
        {
            Debug.Log("RAY HIT NOTHING");
        }

        currentTarget = null;
        return false;
    }

    #endregion

    #region Minigame

    void Start()
    {
        pickPocketMiniGameUI.SetActive(false);

        NPCPolice police = FindObjectOfType<NPCPolice>();
    }

    void Update()
    {
        // Always show ray for debugging
        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * interactDistance, Color.red);

        if (!isPlaying || isGameEnded) return;

        MoveIndicator();
    }

    private void StartGame()
    {
        leftRedBound = -redBarUI.rect.width / 2f;
        rightRedBound = redBarUI.rect.width / 2f;

        currentIndicatorPosition = leftRedBound;
        UpdateIndicatorPosition();

        SetupSuccessZone();

        isPlaying = true;
        isGameEnded = false;
    }

    private void StartPickpocketMinigame()
    {
        Debug.Log("STARTING MINIGAME"); // 👈 add this

        if (currentTarget == null)
        {
            Debug.LogError("No target found for minigame!");
            return;
        }

        pickPocketMiniGameUI.SetActive(true);
        StartGame();
        confirmPickpocketAction.Enable();
    }

    private void CloseMinigame()
    {
        pickPocketMiniGameUI.SetActive(false); 
    }

    private void MoveIndicator()
    {
        currentIndicatorPosition += moveSpeed * currentDirection * Time.deltaTime;

        if (currentIndicatorPosition >= rightRedBound)
        {
            currentIndicatorPosition = rightRedBound;
            currentDirection = -1;
        }
        else if (currentIndicatorPosition <= leftRedBound)
        {
            currentIndicatorPosition = leftRedBound;
            currentDirection = 1;
        }

        UpdateIndicatorPosition();
    }

    private void UpdateIndicatorPosition()
    {
        indicatorUI.anchoredPosition =
            new Vector2(currentIndicatorPosition, redBarUI.anchoredPosition.y);
    }

    #endregion 

    #region check success

    private void SetupSuccessZone()
    {
        float width = redBarUI.rect.width *
            UnityEngine.Random.Range(minSuccessSize, maxSuccessSize) / 100f;

        successZoneHalfWidth = width / 2f;

        float minCenter = leftRedBound + successZoneHalfWidth;
        float maxCenter = rightRedBound - successZoneHalfWidth;

        successZoneCenter = UnityEngine.Random.Range(minCenter, maxCenter);

        greenBarUI.sizeDelta = new Vector2(width, greenBarUI.sizeDelta.y);
        greenBarUI.anchoredPosition =
            new Vector2(successZoneCenter, redBarUI.anchoredPosition.y);
    }

    private void OnConfirmPickpocket(InputAction.CallbackContext context)
    {
        if (currentTarget == null) return;
        if (!isPlaying || isGameEnded) return;

        CheckSuccess();
    }

    private void CheckSuccess()
    {
        bool success =
            currentIndicatorPosition >= (successZoneCenter - successZoneHalfWidth) &&
            currentIndicatorPosition <= (successZoneCenter + successZoneHalfWidth);

        Debug.Log("Success: " + success);

        if (success)
        {
            Debug.Log("Pickpocket successful");
            CloseMinigame();
        }
        else
        {
            Debug.Log("failed"); 
            Failure();
        }

        isGameEnded = true;
        isPlaying = false;
    }


    #endregion 

    #region failure

    public void Failure()
    {
        if(isFailed = true)
        {
            Debug.Log("Police is alerted"); 
            police.Alerted();
        }
    }

    #endregion 
}