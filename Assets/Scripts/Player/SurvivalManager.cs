using UnityEngine; 
using UnityEngine.Events; 

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerDepletionRate = 1f;
    public float currentHunger;
    public float HungerPercent => currentHunger / maxHunger;

    [Header("Thirst")]
    [SerializeField] private float maxThirst = 100f;
    [SerializeField] private float thirstDepletionRate = 1f;
    public float currentThirst;
    public float ThirstPercent => currentThirst / maxThirst;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDepletionRate = 1f;
    [SerializeField] private float staminaRechargeRate = 2f;
    [SerializeField] private float staminaRechargeDelay = 1f;
    public float currentStamina;
    private float currentStaminaDelayCounter;
    public float StaminaPercent => currentStamina / maxStamina;

    [Header("Player References")]
    [SerializeField] private PlayerController playerController;
    public static UnityEvent OnPlayerDied;

    private void Start()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        currentHunger -= hungerDepletionRate * Time.deltaTime;
        currentThirst -= thirstDepletionRate * Time.deltaTime;

        if (currentHunger <= 0 || currentThirst <= 0)
        {
            OnPlayerDied?.Invoke();
            currentHunger = 0;
            currentThirst = 0;
        }

        if (playerController != null && playerController.IsSprinting)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
            currentStaminaDelayCounter = 0f;
        }
        else
        {
            currentStaminaDelayCounter += Time.deltaTime;

            if (currentStaminaDelayCounter >= staminaRechargeDelay)
            {
                currentStamina += staminaRechargeRate * Time.deltaTime;

                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
            }
        }

        if (currentStamina < 0) currentStamina = 0;
    }
}