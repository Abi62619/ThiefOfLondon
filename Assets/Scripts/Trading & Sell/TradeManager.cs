using UnityEngine;

public class TradeManager : MonoBehaviour
{
    [Header("References")]
    public InventoryObject playerInventory;
    public ItemDatabaseObject database;
    public ItemObject coinItem;             

    public TraderNPC currentTrader;
    public TradeUI tradeUI;
    public bool isTrading;

    void Start()
    {
        tradeUI = FindObjectOfType<TradeUI>();
    }

    public void OpenTrade(TraderNPC trader)
    {
        currentTrader = trader;
        isTrading = true;
        tradeUI.OpenTradeUI(trader);
        Debug.Log($"Trading with {trader.traderName}");
    }

    public void CloseTrade()
    {
        currentTrader = null;
        isTrading = false;
        tradeUI.CloseTradeUI();
    }

    // Called when player buys an item from trader
    public bool BuyItem(TradeSlot slot)
    {
        if (!isTrading) return false;

        int playerCoins = GetPlayerCoins();
        if (playerCoins < slot.buyPrice)
        {
            Debug.Log("Not enough coins!");
            return false;
        }

        if (slot.stock == 0)
        {
            Debug.Log("Out of stock!");
            return false;
        }

        // Deduct coins, give item
        RemoveCoins(slot.buyPrice);
        playerInventory.AddItem(slot.item, 1);

        if (slot.stock > 0) slot.stock--;

        Debug.Log($"Bought {slot.item.name} for {slot.buyPrice} coins");
        return true;
    }

    // Called when player sells an item to trader
    public bool SellItem(ItemObject item)
    {
        if (!isTrading) return false;

        // Find matching slot in trader stock to get sell price
        TradeSlot slot = currentTrader.stock.Find(s => s.item == item);
        if (slot == null)
        {
            Debug.Log("Trader won't buy that!");
            return false;
        }

        // Check player actually has it
        if (!PlayerHasItem(item))
        {
            Debug.Log("You don't have that item!");
            return false;
        }

        RemoveItem(item, 1);
        playerInventory.AddItem(coinItem, slot.sellPrice);

        Debug.Log($"Sold {item.name} for {slot.sellPrice} coins");
        return true;
    }

    // Barter: swap one item for another directly
    public bool BarterItem(ItemObject offerItem, TradeSlot wantSlot)
    {
        if (!isTrading) return false;

        if (!PlayerHasItem(offerItem))
        {
            Debug.Log("You don't have that item!");
            return false;
        }

        if (wantSlot.stock == 0)
        {
            Debug.Log("Out of stock!");
            return false;
        }

        RemoveItem(offerItem, 1);
        playerInventory.AddItem(wantSlot.item, 1);

        if (wantSlot.stock > 0) wantSlot.stock--;

        Debug.Log($"Bartered {offerItem.name} for {wantSlot.item.name}");
        return true;
    }

    public int GetPlayerCoins()
    {
        foreach (var slot in playerInventory.Container)
            if (slot.item == coinItem) return slot.amount;
        return 0;
    }

    void RemoveCoins(int amount)
    {
        RemoveItem(coinItem, amount);
    }

    bool PlayerHasItem(ItemObject item)
    {
        return playerInventory.Container.Exists(s => s.item == item && s.amount > 0);
    }

    void RemoveItem(ItemObject item, int amount)
    {
        for (int i = 0; i < playerInventory.Container.Count; i++)
        {
            if (playerInventory.Container[i].item == item)
            {
                playerInventory.Container[i].AddAmount(-amount);
                if (playerInventory.Container[i].amount <= 0)
                    playerInventory.Container.RemoveAt(i);
                return;
            }
        }
    }
}