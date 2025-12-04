using UnityEngine;

public class PickpocketDetector : MonoBehaviour
{
    public PickpocketTarget currentTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickpocketTarget target))
        {
            currentTarget = target;
            Debug.Log("Target in range: " + target.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PickpocketTarget target))
        {
            if (currentTarget == target)
            {
                Debug.Log("Target left range: " + target.name);
                currentTarget = null;
            }
        }
    }
}