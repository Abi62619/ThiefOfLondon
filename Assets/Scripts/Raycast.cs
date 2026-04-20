using UnityEngine;

public class Raycast : MonoBehaviour
{

    public LayerMask raycastMask;
    public float maxDistance = 10f;
    public GameObject enemy;
    public float destroyDistance = 5f;
    public float checkInterval = 1f;
    private bool isAlive = true; 

    RaycastHit objectJustPinged;

    private void Start()
    {
        InvokeRepeating("CheckRaycast", 0f, checkInterval);
    }

    private void CheckRaycast()
    {
        if (enemy != null)
        {
            Vector3 directionToPlayer = enemy.transform.position  - transform.position;

            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out objectJustPinged, maxDistance, raycastMask))
            {
                if (objectJustPinged.collider.gameObject == enemy && objectJustPinged.distance <= destroyDistance)
                {
                   
                    Destroy(enemy);
                    Debug.Log("Destroy Enemy");
                    
                    
                }
            }
        }        
    }
}