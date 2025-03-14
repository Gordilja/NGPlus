using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Tooltip Tooltip;
    public PlayerData playerData;
    [SerializeField] private MainInventory MainInventory;
    [SerializeField] private CharacterEquipment CharacterGear;
    [SerializeField] private GameObject Helmet;
    [SerializeField] private GameObject Weapon;
    [SerializeField] private InputManager InputManagerDatabase;
    [SerializeField] private ItemDataBase ItemDatabase;

    private void OnEnable()
    {
        Inventory.PickedUpItem += AddItem;
        MainInventory.InitializeSlots();
        //playerData = new PlayerData();
        //playerData.LoadPlayerData();
        //LoadInventory();
        //LoadEquipment();
    }

    private void OnDisable()
    {
        Inventory.PickedUpItem -= AddItem;
        playerData.SavePlayerDataFile();
    }

    private void LoadInventory() 
    {
        for (int i = 0; i < playerData.InventoryItems.Count; i++) 
        {
            LoadItem(playerData.InventoryItems[i].ItemIndex, playerData.InventoryItems[i].SlotIndex);
        }
    }

    private void LoadEquipment() 
    {
        for (int i = 0; i < playerData.EquipmentItems.Count; i++)
        {
            LoadEquipment(playerData.EquipmentItems[i].ItemIndex, playerData.EquipmentItems[i].SlotIndex);
        }
    }

    public void AddItem(Item item) 
    {
        for (int i = 0; i < MainInventory.Slots.Count; i++)
        {
            if (!MainInventory.Slots[i].IsOccupied)
            {
                MainInventory.Slots[i].AddItem(item);
                playerData.InventoryItems.Add(MainInventory.Slots[i].InventoryItem.Data);
                break;
            }
        }
    }

    public void LoadItem(int itemIndex, int slotIndex)
    {
        Item item = ItemDatabase.GetById(itemIndex);
        MainInventory.Slots[slotIndex].AddItem(item);
    }

    public void LoadEquipment(int itemIndex, int slotIndex)
    {
        Item item = ItemDatabase.GetById(itemIndex);
        CharacterGear.Slots[slotIndex].AddItem(item);
    }

    public void ItemEquiped(InventoryItem item) 
    {
        Helmet.SetActive(CharacterGear.IsEquiped(ItemType.HeadGear));
        Weapon.SetActive(CharacterGear.IsEquiped(ItemType.Weapon));
        playerData.EquipmentItems.Add(item.Data);
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