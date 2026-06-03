using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TradeUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject tradePanel;

    [Header("Trader Side (Left)")]
    public Transform traderItemContainer;
    public GameObject traderItemSlotPrefab;
    public TextMeshProUGUI traderNameText;

    [Header("Player Side (Right)")]
    public Transform playerItemContainer;
    public GameObject playerItemSlotPrefab;
    public TextMeshProUGUI playerCoinsText;

    [Header("Action Buttons")]
    public Button buyButton;
    public Button sellButton;
    public Button barterButton;
    public Button closeButton;

    [Header("Selected Info")]
    public TextMeshProUGUI selectedItemNameText;
    public TextMeshProUGUI selectedItemPriceText;

    private TradeManager tradeManager;
    private TradeSlot selectedTraderSlot;
    private ItemObject selectedPlayerItem;

    private List<GameObject> traderSlotObjects = new List<GameObject>();
    private List<GameObject> playerSlotObjects = new List<GameObject>();

    void Start()
    {
        tradeManager = FindObjectOfType<TradeManager>();

        buyButton.onClick.AddListener(OnBuyClicked);
        sellButton.onClick.AddListener(OnSellClicked);
        barterButton.onClick.AddListener(OnBarterClicked);
        closeButton.onClick.AddListener(OnCloseClicked);

        tradePanel.SetActive(false);
    }

    public void OpenTradeUI(TraderNPC trader)
    {
        tradePanel.SetActive(true);
        traderNameText.text = trader.traderName;
        selectedTraderSlot = null;
        selectedPlayerItem = null;
        UpdateButtons();
        RefreshTraderSlots(trader);
        RefreshPlayerSlots();
    }

    public void CloseTradeUI()
    {
        tradePanel.SetActive(false);
        ClearSlots();
    }

    void RefreshTraderSlots(TraderNPC trader)
    {
        ClearList(traderSlotObjects, traderItemContainer);

        foreach (var slot in trader.stock)
        {
            var obj = Instantiate(traderItemSlotPrefab, traderItemContainer);
            obj.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{slot.item.name}\nBuy: {slot.buyPrice}c  Sell: {slot.sellPrice}c\nStock: {(slot.stock == -1 ? "∞" : slot.stock.ToString())}";
            obj.GetComponent<Image>().sprite = slot.item.sprite;

            var capturedSlot = slot;
            obj.GetComponent<Button>().onClick.AddListener(() => OnTraderSlotClicked(capturedSlot));
            traderSlotObjects.Add(obj);
        }
    }

    void RefreshPlayerSlots()
    {
        ClearList(playerSlotObjects, playerItemContainer);

        int coins = tradeManager.GetPlayerCoins();
        playerCoinsText.text = $"Coins: {coins}";

        foreach (var slot in tradeManager.playerInventory.Container)
        {
            if (slot.item == tradeManager.coinItem) continue; // don't show coins as tradeable

            var obj = Instantiate(playerItemSlotPrefab, playerItemContainer);
            obj.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{slot.item.name}\nx{slot.amount}";
            obj.GetComponent<Image>().sprite = slot.item.sprite;

            var capturedItem = slot.item;
            obj.GetComponent<Button>().onClick.AddListener(() => OnPlayerSlotClicked(capturedItem));
            playerSlotObjects.Add(obj);
        }
    }

    void OnTraderSlotClicked(TradeSlot slot)
    {
        selectedTraderSlot = slot;
        selectedItemNameText.text = slot.item.name;
        selectedItemPriceText.text = $"Buy: {slot.buyPrice}c  |  Barter: offer an item";
        UpdateButtons();
    }

    void OnPlayerSlotClicked(ItemObject item)
    {
        selectedPlayerItem = item;
        selectedItemNameText.text = item.name;
        selectedItemPriceText.text = $"Sell value: {GetSellPrice(item)}c";
        UpdateButtons();
    }

    void OnBuyClicked()
    {
        if (selectedTraderSlot == null) return;
        bool success = tradeManager.BuyItem(selectedTraderSlot);
        if (success) Refresh();
    }

    void OnSellClicked()
    {
        if (selectedPlayerItem == null) return;
        bool success = tradeManager.SellItem(selectedPlayerItem);
        if (success) Refresh();
    }

    void OnBarterClicked()
    {
        if (selectedPlayerItem == null || selectedTraderSlot == null) return;
        bool success = tradeManager.BarterItem(selectedPlayerItem, selectedTraderSlot);
        if (success) Refresh();
    }

    void OnCloseClicked()
    {
        tradeManager.CloseTrade();
    }

    void Refresh()
    {
        selectedTraderSlot = null;
        selectedPlayerItem = null;
        selectedItemNameText.text = "";
        selectedItemPriceText.text = "";
        UpdateButtons();
        RefreshTraderSlots(tradeManager.currentTrader);
        RefreshPlayerSlots();
    }

    void UpdateButtons()
    {
        buyButton.interactable = selectedTraderSlot != null;
        sellButton.interactable = selectedPlayerItem != null;
        barterButton.interactable = selectedTraderSlot != null && selectedPlayerItem != null;
    }

    int GetSellPrice(ItemObject item)
    {
        if (tradeManager.currentTrader == null) return 0;
        var slot = tradeManager.currentTrader.stock.Find(s => s.item == item);
        return slot != null ? slot.sellPrice : 0;
    }

    void ClearSlots()
    {
        ClearList(traderSlotObjects, traderItemContainer);
        ClearList(playerSlotObjects, playerItemContainer);
    }

    void ClearList(List<GameObject> list, Transform container)
    {
        foreach (var obj in list) Destroy(obj);
        list.Clear();
    }
}