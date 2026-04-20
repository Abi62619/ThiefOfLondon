using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ForestManager : MonoBehaviour
{
    [System.Serializable]
    public class CollectibleData
    {
        public string itemName;

        [Header("Prefab")]
        public GameObject prefab;

        [Header("UI")]
        public Slider slider;

        [HideInInspector] public int amountNeeded;
        [HideInInspector] public int amountCollected;
    }

    [Header("Collectibles")]
    public CollectibleData acorn;
    public CollectibleData ladybug;
    public CollectibleData lily;

    [Header("Spawn Points")]
    public List<Transform> spawnPoints;
private List<Transform> availableSpawnPoints;
public DescriptionCreator descriptionCreator;
public Dialog dialog;
    void Start()
    {
     availableSpawnPoints = new List<Transform>(spawnPoints);

    InitializeCollectible(acorn);
    InitializeCollectible(ladybug);
    InitializeCollectible(lily);

    }

    void InitializeCollectible(CollectibleData data)
    {
        // 2 and 4
        data.amountNeeded = Random.Range(2, 5); 
        data.amountCollected = 0;

        //  slider
        data.slider.maxValue = data.amountNeeded;
        data.slider.value = 0;

        // Spawn 
        SpawnObjects(data);
    }

    void SpawnObjects(CollectibleData data)
{
    for (int i = 0; i < data.amountNeeded; i++)
    {
        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("Not enough spawn points for all collectibles!");
            return;
        }

        int randomIndex = Random.Range(0, availableSpawnPoints.Count);
        Transform spawnPoint = availableSpawnPoints[randomIndex];

        Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);

        // Remove  point so nothing else can use
        availableSpawnPoints.RemoveAt(randomIndex);
    }
}

   public void AddCollected(string tag)
{
    CollectibleData data = GetCollectibleByName(tag);

    if (data == null) return;

    data.amountCollected++;
    data.slider.value = data.amountCollected;

    if (data.amountCollected >= data.amountNeeded)
    {
        OnCollectibleComplete(data);
    }
}

   CollectibleData GetCollectibleByName(string tag)
{
    if (tag == "Acorn") return acorn;
    if (tag == "Ladybug") return ladybug;
    if (tag == "Lily") return lily;

    return null;
}

    void OnCollectibleComplete(CollectibleData data)
    {
        Debug.Log(data.itemName + " collection complete!");
        if (acorn.amountCollected == acorn.amountNeeded &&
            ladybug.amountCollected == ladybug.amountNeeded &&
            lily.amountCollected == lily.amountNeeded)
        {
            descriptionCreator.hasAllItems = true;
            dialog.IsQuestComplete = true;
            
        }
    }                                                             
}
