using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickPocketManger : MonoBehaviour
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

    [Header("Pickpocket Settings")]
    [SerializeField] private float moveSpeed = 300f; 
    private float currentIndicatorPosition; 
    private int currentDirection = 1;

    [SerializeField] private float minSuccessSize = 5f; 
    [SerializeField] private float maxSuccessSize = 20f;

    [Header("Raycast Settings")]
    [SerializeField] private Transform rayOrigin; // camera on player
    [SerializeField] private float interactDistance = 3.0f;
    [SerializeField] private LayerMask npcLayer; 

    [Header("Rewards")]
    [SerializeField] private int minGold = 5;
    [SerializeField] private int maxGold = 20;
    [SerializeField] private float itemChance = 0.25f; // 25%

    private PickpocketTarget currentTarget;

    private float successZoneCenter; 
    private float successZoneHalfWidth; 

    private float leftRedBound; 
    private float rightRedBound;

    private bool isPlaying = false; 
    private bool isGameEnded = false; 

    [SerializeField] private PlayerInventory playerInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame(); 
        
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying || isGameEnded) return;

        MoveIndicator();

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * interactDistance, Color.red);
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
        pickPocketMiniGameUI.SetActive(true);

        StartGame();
        confirmPickpocketAction.Enable();
    }

    private void UpdateIndicatorPosition()
    {
        indicatorUI.anchoredPosition = new Vector2(currentIndicatorPosition, redBarUI.anchoredPosition.y);
    }   

    private void SetupSuccessZone()
    {
        float randomSuccessZoneWidth = redBarUI.rect.width * UnityEngine.Random.Range(minSuccessSize, maxSuccessSize) / 100f;

        successZoneHalfWidth = randomSuccessZoneWidth / 2f;

        float minCenter = leftRedBound + successZoneHalfWidth;
        float maxCenter = rightRedBound - successZoneHalfWidth;
        successZoneCenter = UnityEngine.Random.Range(minCenter, maxCenter);

        greenBarUI.sizeDelta = new Vector2(randomSuccessZoneWidth, greenBarUI.sizeDelta.y);
        greenBarUI.anchoredPosition = new Vector2(successZoneCenter, redBarUI.anchoredPosition.y);
    }

    private void MoveIndicator()
    {
        currentIndicatorPosition += moveSpeed * currentDirection * Time.deltaTime;

        if(currentIndicatorPosition >= rightRedBound)
        {
            currentIndicatorPosition = rightRedBound;
            currentDirection = -1; 
        }
        else if(currentIndicatorPosition <= leftRedBound)
        {
            currentIndicatorPosition = leftRedBound;
            currentDirection = 1; 
        }

        UpdateIndicatorPosition();
    }

    private void OnEnable()
    {
        Debug.Log("PickPocket Manager assigned!"); 

        var actionMap = inputActionAsset.FindActionMap("Player");
        interactAction = actionMap.FindAction("Pickpocket");

        interactAction.Enable();

        interactAction.performed += OnInteract;

        confirmPickpocketAction = actionMap.FindAction("PickpocketConfirm");

        confirmPickpocketAction.Enable();
        confirmPickpocketAction.performed += OnConfirmPickpocket;
    }

    private void OnDisable()
    {
        interactAction.performed -= OnInteract;

        interactAction.Disable();

        confirmPickpocketAction.performed -= OnConfirmPickpocket;

        confirmPickpocketAction.Disable();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("[PickPocket] Interact pressed");

        if (!TryFindNPC())
        {
            Debug.Log("[PickPocket] No NPC found");
            return;
        }

        Debug.Log($"[PickPocket] NPC found: {currentTarget.name}");

        if (currentTarget.hasBeenPickpocketed)
        {
            Debug.Log("[PickPocket] NPC already pickpocketed");
            return;
        }

        StartPickpocketMinigame();
    }

    private void OnConfirmPickpocket(InputAction.CallbackContext context)
    {
        if (!isPlaying || isGameEnded) return;

        CheckSuccess();
    }

    private void CheckSuccess()
    {
        bool success =
            currentIndicatorPosition >= (successZoneCenter - successZoneHalfWidth) &&
            currentIndicatorPosition <= (successZoneCenter + successZoneHalfWidth);

        Debug.Log($"[PickPocket] CheckSuccess: {success}");

        if(success)
        {
            GiveRewards();
            Debug.Log("[PickPocket] Pickpocket successful");
        }
        else
        {
            Failure(); 
        }

        isGameEnded = true;
        isPlaying = false;
    }

    public void CloseMinigame()
    {
        Debug.Log("[PickPocket] CloseMinigame()");

        pickPocketMiniGameUI.SetActive(false);  
    }

    private bool TryFindNPC()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, npcLayer))
        {
            PickpocketTarget target = hit.collider.GetComponentInParent<PickpocketTarget>();

            if (target != null)
            {
                currentTarget = target;
                Debug.Log("[PickPocket] Raycast hit NPC");
                return true;
            }
        }

        currentTarget = null;
        return false;
    }

    private void GiveRewards()
    {
        Debug.Log($"[PickPocket] playerInventory null? {playerInventory == null}");

        int gold = UnityEngine.Random.Range(minGold, maxGold + 1);
        playerInventory.AddGold(gold);

        Debug.Log($"[PickPocket] Gold gained: {gold}");

        if (UnityEngine.Random.value <= itemChance)
        {
            Debug.Log("[PickPocket] Item stolen");
        }

        currentTarget.hasBeenPickpocketed = true;
    }

    private void Failure()
    {
        Debug.Log("[PickPocket] Pickpocket failed - NPC alerted");
    }   

    public void TestButton()
    {
        Debug.Log("[PickPocket] Test button works");
    }
}