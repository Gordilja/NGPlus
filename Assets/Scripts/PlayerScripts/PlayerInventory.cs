using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Tooltip Tooltip;
    [SerializeField] private MainInventory MainInventory;
    [SerializeField] private CharacterEquipment CharacterGear;
    [SerializeField] private GameObject Helmet;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private InputManager InputManagerDatabase;

    private void OnEnable()
    {
        Inventory.PickedUpItem += AddItem;
        MainInventory.InitializeSlots();
    }

    private void OnDisable()
    {
        Inventory.PickedUpItem -= AddItem;
    }

    public void AddItem(Item item) 
    {
        for (int i = 0; i < MainInventory.Slots.Count; i++)
        {
            if (!MainInventory.Slots[i].IsOccupied)
            {
                MainInventory.Slots[i].AddItem(item);
                break;
            }
        }
    }

    public void ItemEquiped() 
    {
        Helmet.SetActive(CharacterGear.IsEquiped(ItemType.HeadGear));
        Weapon.SetActive(CharacterGear.IsEquiped(ItemType.Weapon));
    }

    public void InventoryPlayerInput()
    {
        if (Input.GetKeyDown(InputManagerDatabase.CharacterSystemKeyCode))
        {
            if (!CharacterGear.gameObject.activeSelf)
            {
                CharacterGear.Open();
            }
            else
            {
                CharacterGear.Close();
            }
        }

        if (Input.GetKeyDown(InputManagerDatabase.InventoryKeyCode))
        {
            if (!MainInventory.gameObject.activeSelf)
            {
                MainInventory.Open();
            }
            else
            {
                MainInventory.Close();
            }
        }
    }
}