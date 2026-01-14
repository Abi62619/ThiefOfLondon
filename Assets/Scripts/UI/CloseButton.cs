using UnityEngine;
using UnityEngine.SceneManagement; 

public class CloseButton : MonoBehaviour
{
    [SerializeField] GameObject pickpocketMiniGame; 

    public void Close()
    {
        pickpocketMiniGame.SetActive(false); 
    }
}
