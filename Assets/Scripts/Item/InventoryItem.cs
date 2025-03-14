﻿using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Camera mainCam;
    public Image Icon;
    public Item item;
    public Canvas canvas;
    public Slot originalSlot;
    public PlayerInventory PlayerInventory;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>(true);
        PlayerInventory = FindObjectOfType<PlayerInventory>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCam = Camera.main;
        originalSlot = transform.parent.GetComponent<Slot>();
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
            if (!newSlot.IsOccupied && (newSlot.AllowedItemType == item.Type || newSlot.AllowedItemType == ItemType.Default))
            {
                SetSlot(newSlot, false);
            }
            else if (newSlot.IsOccupied && (newSlot.Item.Type == item.Type || newSlot.AllowedItemType == ItemType.Default))
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
            Instantiate(item.Model, position, Quaternion.identity);
            originalSlot.IsOccupied = false;
            originalSlot.Item = null;
            PlayerInventory.ItemEquiped();
            Destroy(gameObject);
        }
    }

    public void SetItem(Item _item)
    {
        item = _item;
        Icon.sprite = item.Icon;
        originalSlot.IsOccupied = true;
        originalSlot.Item = item;
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
        originalSlot.Item = item;
        PlayerInventory.ItemEquiped();
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
            Item item = eventData.pointerEnter.transform.parent.GetComponent<InventoryItem>().item;
            PlayerInventory.Tooltip.Activate(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerInventory.Tooltip.Deactivate();
    }
}