using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideSpeed = 10f;
    public float slideDuration = 0.75f;
    public float slideCooldown = 1f;
    public float slideHeight = 0.5f;

    private Rigidbody rb;
    private CapsuleCollider capsule;

    private float originalHeight;
    private float slideTimer;
    private float nextSlideTime;

    private bool isSliding;
    private bool isGrounded;
    private Vector2 moveInput;

    public bool IsSliding => isSliding;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        originalHeight = capsule.height;
    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public void StartSlide()
    {
        if (!isGrounded) return;
        if (Time.time < nextSlideTime) return;
        if (moveInput.magnitude < 0.1f) return;

        isSliding = true;
        slideTimer = slideDuration;
        nextSlideTime = Time.time + slideCooldown;

        capsule.height = slideHeight;
        rb.AddForce(transform.forward * slideSpeed, ForceMode.VelocityChange);
    }

    public void StopSlide()
    {
        if (!isSliding) return;

        isSliding = false;
        capsule.height = originalHeight;
    }

    void Update()
    {
        if (!isSliding) return;

        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0f)
        {
            StopSlide();
        }
    }
}
