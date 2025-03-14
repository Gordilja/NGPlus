using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public InventoryItemData Data;
    [HideInInspector] public Item Item;
    public Slot originalSlot;
    [SerializeField] private Image Icon;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;
    private PlayerInventory PlayerInventory;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>(true);
        PlayerInventory = FindObjectOfType<PlayerInventory>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalSlot = transform.parent.GetComponent<Slot>();
        Data = new InventoryItemData();
        Data.ItemIndex = Item.Id;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        Slot newSlot = null;

        GameObject hoveredObject = eventData.pointerCurrentRaycast.gameObject; // Get precise UI element

        if (hoveredObject != null)
        {
            // Check if we hit an InventoryItem first
            InventoryItem hitItem = hoveredObject.GetComponentInParent<InventoryItem>();

            if (hitItem != null)
            {
                newSlot = hitItem.originalSlot;
            }
            else
            {
                // If not an InventoryItem, check if we hit a Slot
                newSlot = hoveredObject.GetComponentInParent<Slot>();
            }
        }

        if (newSlot != null)
        {
            if (!newSlot.IsOccupied && (newSlot.AllowedItemType == Item.Type || newSlot.AllowedItemType == ItemType.Default))
            {
                SetSlot(newSlot, false);
            }
            else if (newSlot.IsOccupied && (newSlot.Item.Type == Item.Type || newSlot.AllowedItemType == ItemType.Default))
            {
                SwapSlots(newSlot);
            }
            else
            {
                ResetToOriginalSlot();
            }
        }
        else
        {
            // Dropping outside the inventory
            Vector3 position = PlayerInventory.transform.position;
            Instantiate(Item.Model, position, Quaternion.identity);
            originalSlot.IsOccupied = false;
            originalSlot.Item = null;
            PlayerInventory.ItemEquiped(this);
            Destroy(gameObject);
        }
    }

    public void SetItem(Item _item)
    {
        Item = _item;
        Icon.sprite = Item.Icon;
        originalSlot.IsOccupied = true;
        originalSlot.Item = Item;
    }

    private void SetSlot(Slot newSlot, bool swap)
    {
        if (originalSlot != null && !swap)
        {
            originalSlot.IsOccupied = false;
            originalSlot.Item = null;
        }
        transform.SetParent(newSlot.transform);
        rectTransform.anchoredPosition = Vector3.zero;
        originalSlot = newSlot;
        originalSlot.IsOccupied = true;
        originalSlot.Item = Item;
        Data.SlotIndex = originalSlot.SlotId;
        PlayerInventory.ItemEquiped(this);
    }

    private void SwapSlots(Slot newSlot)
    {
        InventoryItem otherItem = newSlot.GetComponentInChildren<InventoryItem>();

        if (otherItem != null)
        {
            otherItem.SetSlot(originalSlot, true);
        }

        SetSlot(newSlot, true); // Move this item to the new slot
    }

    private void ResetToOriginalSlot()
    {
        transform.SetParent(originalSlot.transform);
        rectTransform.anchoredPosition = Vector3.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Item item = eventData.pointerEnter.transform.parent.GetComponent<InventoryItem>().Item;
            PlayerInventory.Tooltip.Activate(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerInventory.Tooltip.Deactivate();
    }
}