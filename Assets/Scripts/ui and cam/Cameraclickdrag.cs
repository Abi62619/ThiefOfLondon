using UnityEngine;

public class Cameraclickdrag : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    public float dragSensitivity = 0.3f;

    [Header("Vertical Look")]
    public float maxPitch = 80f;
    public float pitchSpeed = 120f;
    public float pitchReturnSpeed = 6f;

    private float currentAngle;     // horizontal orbit
    private float targetAngle;

    private float currentPitch;     // vertical look offset
    private float targetPitch;

    private bool userDragging = false;

    private void Start()
    {
        if (player != null)
            RecenterCamera();
    }

    private void Update()
    {
        HandleDragInput();
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // ===== ORIGINAL HORIZONTAL LOGIC (UNCHANGED) =====
        float behindPlayer = player.eulerAngles.y + 180f;

        float minAngle = behindPlayer - 90f;
        float maxAngle = behindPlayer + 90f;

        if (!userDragging)
        {
            targetAngle = Mathf.LerpAngle(
                targetAngle,
                behindPlayer,
                Time.deltaTime * rotationSpeed
            );
        }

        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
        currentAngle = Mathf.LerpAngle(
            currentAngle,
            targetAngle,
            Time.deltaTime * rotationSpeed
        );

        // ===== VERTICAL LOOK (RETURNS TO SAME POINT) =====
        targetPitch = Mathf.Clamp(targetPitch, -maxPitch, maxPitch);

        if (!userDragging)
        {
            targetPitch = Mathf.Lerp(
                targetPitch,
                0f,
                Time.deltaTime * pitchReturnSpeed
            );
        }

        currentPitch = Mathf.Lerp(
            currentPitch,
            targetPitch,
            Time.deltaTime * pitchReturnSpeed
        );

        // ===== ORIGINAL CAMERA POSITION (UNCHANGED) =====
        Vector3 offset = new Vector3(
            Mathf.Sin(currentAngle * Mathf.Deg2Rad) * distance,
            height,
            Mathf.Cos(currentAngle * Mathf.Deg2Rad) * distance
        );

        Vector3 desiredPos = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            Time.deltaTime * followSpeed
        );

        // ===== ORIGINAL RETURN POINT + PITCH OFFSET =====
        Vector3 lookTarget =
    player.position +
    Vector3.up * (height * 0.5f + currentPitch * 0.02f);

        transform.LookAt(lookTarget);
    }

    private void HandleDragInput()
    {
        if (Input.GetMouseButtonDown(0))
            userDragging = true;

        if (Input.GetMouseButtonUp(0))
            userDragging = false;

        if (userDragging)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            targetAngle += mouseX * dragSensitivity * 100f;
            targetPitch -= mouseY * dragSensitivity * pitchSpeed;
        }
    }

    public void RecenterCamera()
    {
        if (player == null) return;

        targetAngle = player.eulerAngles.y + 180f;
        targetPitch = 0f;
    }
}
