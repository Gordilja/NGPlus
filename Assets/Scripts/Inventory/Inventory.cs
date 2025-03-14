using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotNum;
    [SerializeField] private Transform slotParent;
    [SerializeField] private Slot slotPrefab;
    public List<Slot> Slots = new List<Slot>();

    public delegate void ItemDelegate(Item item);
    public static event ItemDelegate PickedUpItem;
    public static event ItemDelegate RemovedItem;

    public delegate void InventoryEvent();
    public static event InventoryEvent InventoryOpened;
    public static event InventoryEvent InventoryClosed;

    public void InitializeSlots()
    {
        for (int i = 0; i < slotNum; i++)
        {
            Slot newSlot = Instantiate(slotPrefab, slotParent);
            newSlot.name = i.ToString();
            newSlot.SetSlotId(i);
            Slots.Add(newSlot);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        InventoryOpened?.Invoke();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        InventoryClosed?.Invoke();
    }

    public void PickUpItem(Item item)
    {
        PickedUpItem?.Invoke(item);
    }

    public void RemoveItemFromInventory(Item item)
    {
        Slot slotToRemove = Slots.Find(slot => slot.Item == item);
        if (slotToRemove != null)
        {
            slotToRemove.IsOccupied = false;
            slotToRemove.Item = null;
            RemovedItem?.Invoke(item);
        }
    }
}