using UnityEngine;

public class CameraRig180 : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;

    private float currentAngle = 0f;   // current orbit angle around player (0 = behind)

    private void LateUpdate()
    {
        if (player == null) return;

        // Get where the camera SHOULD be (opposite of behind the player)
        float targetAngle = player.eulerAngles.y + 180f;   // <-- flipped 180°

        // Keep orbit inside 180° but on the opposite side
        float minAngle = targetAngle - 90f;
        float maxAngle = targetAngle + 90f;

        // Smooth angle follow
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        // FINAL ANGLE after flipping
        float flippedAngle = currentAngle; // already flipped by adding 180 above

        // Position camera using the flipped angle
        Vector3 offset = new Vector3(
            Mathf.Sin(flippedAngle * Mathf.Deg2Rad) * distance,
            height,
            Mathf.Cos(flippedAngle * Mathf.Deg2Rad) * distance
        );

        Vector3 desiredPos = player.position + offset;

        // Smooth move
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * followSpeed);

        // Look at the player
        transform.LookAt(player.position + Vector3.up * height * 0.5f);
    }
}
