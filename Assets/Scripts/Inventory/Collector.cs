using UnityEngine;

public class Collector : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>(); 
        if(collectable != null)
        {
            collectable.Collect(); 
        }
    }
}
