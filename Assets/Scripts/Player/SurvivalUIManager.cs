using UnityEngine; 
using UnityEngine.UI; 

public class SurvivalUIManager : MonoBehaviour
{
    [SerializeField] private SurvivalManager survivalManager; 

    [SerializeField] private Image hungerBar; 
    [SerializeField] private Image thristBar; 
    [SerializeField] private Image staminaBar; 

    private void FixedUpdate(){
        hungerBar.fillAmount = survivalManager.HungerPercent; 
        thristBar.fillAmount = survivalManager.ThirstPercent; 
        staminaBar.fillAmount = survivalManager.StaminaPercent;
    }
}