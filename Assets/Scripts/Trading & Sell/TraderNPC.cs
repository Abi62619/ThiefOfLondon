using UnityEngine;
using System.Collections.Generic;

public class TraderNPC : MonoBehaviour
{
    [Header("Trader Settings")]
    public string traderName = "Merchant";
    public float interactRange = 2f;
    public KeyCode interactKey = KeyCode.E;

    [Header("Stock")]
    public List<TradeSlot> stock = new List<TradeSlot>();

    private bool playerInRange;
    private TradeManager tradeManager;

    void Start()
    {
        tradeManager = FindObjectOfType<TradeManager>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
            tradeManager.OpenTrade(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            tradeManager.CloseTrade();
        }
    }
}

[System.Serializable]
public class TradeSlot
{
    public ItemObject item;
    public int stock;         // -1 = unlimited
    public int buyPrice;      // cost for player to buy
    public int sellPrice;     // coins player gets when selling
}