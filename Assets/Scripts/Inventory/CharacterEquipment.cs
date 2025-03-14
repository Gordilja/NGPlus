public class CharacterEquipment : Inventory
{
    public bool IsEquiped(ItemType SlotType) 
    {
        switch (SlotType) 
        {
            case ItemType.HeadGear:
                return CheckSlotList(ItemType.HeadGear);
            case ItemType.Shield:
                return CheckSlotList(ItemType.Shield);
            case ItemType.Weapon:
                return CheckSlotList(ItemType.Weapon);
            default:
                return false;
        }
    }

    private bool CheckSlotList(ItemType Type) 
    {
        for (int i = 0; i < Slots.Count; i++) 
        {
            if (Slots[i].AllowedItemType == Type) 
            {
                return Slots[i].IsOccupied;
            }
        }

        return false;
    }
}