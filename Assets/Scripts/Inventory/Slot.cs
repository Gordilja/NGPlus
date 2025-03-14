using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private InventoryItem prefab;
    public int SlotId;
    public bool IsOccupied;
    public Item Item;
    public InventoryItem InventoryItem;
    public ItemType AllowedItemType = ItemType.Default;

    public void SetSlotId(int id)
    {
        SlotId = id;
    }

    public void AddItem(Item item)
    {
        InventoryItem = Instantiate(prefab, transform);
        InventoryItem.originalSlot = this;
        InventoryItem.SetItem(item);
    }
}