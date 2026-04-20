using UnityEngine;

public class CameraLag : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform lookTarget;

    [Header("Camera Lag Settings")]
    [Range(0f, 1f)]
    public float cameraLag = 0.8f; // How much the camera follows its OLD pos
    public Vector3 positionOffset = Vector3.zero;
    private Vector3 offsetLocal;
    private Quaternion targetRotation;

    private void Start()
    {
        if (player != null)
            offsetLocal = player.InverseTransformPoint(transform.position);
        else
            offsetLocal = transform.position;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // --- POSITION LAG ---
        Vector3 desiredPos = player.TransformPoint(offsetLocal);
        desiredPos += positionOffset;

        Vector3 newPos = Vector3.Lerp(
            transform.position,
            desiredPos,
            1f - cameraLag
        );
        transform.position = newPos;

        // --- LOOK AT TARGET WITH X ROTATION LOCKED ---
        if (lookTarget != null)
        {
            // Compute full look-at rotation
            Quaternion fullTargetRot = Quaternion.LookRotation(
                (lookTarget.position - transform.position).normalized,
                Vector3.up
            );

            // Smooth the rotation
            Quaternion slerped = Quaternion.Slerp(
                transform.rotation,
                fullTargetRot,
                1f - cameraLag
            );

            // Freeze X axis
            Vector3 e = slerped.eulerAngles;
            e.x = transform.eulerAngles.x;

            transform.rotation = Quaternion.Euler(e);
        }
    }
}
