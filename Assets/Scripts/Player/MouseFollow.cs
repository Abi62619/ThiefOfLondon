using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [Header("Mouse Movement")]
    [SerializeField] private Vector2 senstivity;

    private Vector2 rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /*void Vector2 GetInput()
    {
        Vector2 input = new Vector2(
            
            )
            

    }*/
}
