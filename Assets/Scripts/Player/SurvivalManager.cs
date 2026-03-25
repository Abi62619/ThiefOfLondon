using UnityEngine; 

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger; 
    [SerializeField] private float hungerDepletionRate = 1f; 
    private float currentHunger; 
    public float HungerPercent => currentHunger / maxHunger; 

    [Header("Thrist")]
    [SerializeField] private float maxThrist 100f; 
    [SerializeField] private float thristDepletionRate = 1f;
    private float currentThrist; 
    public float ThristPercent => currentThrist / maxThrist; 

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f; 
    [SerializeField] private float staminaDepletionRate = 1f; 
    [SerializeField] private float staminaRechargeRate = 2f; 
    [SerializeField] private float staminaRechargeDelay = 1f; 
    private float currentStamina; 
    private float currentStaminaDelayCounter; 
    public float StaminaPercent => currentStamina / maxStamina; 

    
}