using UnityEngine;
using UnityEngine.InputSystem;

public class ResetAllBndings : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    public void ResetBndings()
    {
        foreach (InputActionMap map in inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }
        PlayerPrefs.DeleteKey("rebinds");

    }
}
