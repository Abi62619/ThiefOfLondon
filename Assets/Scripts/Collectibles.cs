using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public string itemName;
  private ForestManager manager;

    void Start()
    {
        manager = FindObjectOfType<ForestManager>();
    }

private void OnTriggerEnter(Collider other)    {
    Debug.Log("Collided with: " + other.name);
        
        if (!other.CompareTag("Player")) return;

        // Use this object's tag to identify what it is
        string itemTag = gameObject.tag;

        manager.AddCollected(itemTag);

        Destroy(gameObject);
    }
}