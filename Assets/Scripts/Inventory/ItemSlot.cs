using UnityEngine; 
using UnityEngine.UI; 

public class ItemSlot : MonoBehaviour
{
    [SerializeField] public int slotIndex; 
    [SerializeField] public Image itemIcon; 
    [SerializeField] public Button button; 

    public void OnSlotClicked()
    {
        //logic to handle item selection and transfer 
        TradingManager.Instance.SelectItem(slotIndex, this.transform.parent.name); //pass inventoryy name 
    }
}