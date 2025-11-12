using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform playerLocation; 

    private float moveX = 0f;
    private float moveY = 0f;
    private Vector3 velocity; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = 0f;
        moveY = 0f;

        if(Keyboard.current.wKey.isPressed)
        {
            moveY += 1f;
            //Debug.Log("w is pressed"); 
        }

        if (Keyboard.current.aKey.isPressed)
        {
            moveX -= 1f;
            //Debug.Log("a is pressed");
        }

        if (Keyboard.current.sKey.isPressed)
        {
            moveY -= 1f;
            //Debug.Log("s is pressed");
        }

        if (Keyboard.current.dKey.isPressed)
        {
            moveX += 1f;
            //Debug.Log("d is pressed");
        }

        // direction 
        Vector3 move = new Vector3(moveX, 0f, moveY);

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        //multiply
        velocity = move * moveSpeed * Time.deltaTime;

        //move
        characterController.Move(velocity);
    }
}
