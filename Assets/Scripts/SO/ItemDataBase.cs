using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDataBaseSO", menuName = "ScriptableObjects/ItemDataBase", order = 2)]
public class ItemDataBase : ScriptableObject
{
    [SerializeField]
    public List<Item> itemList = new List<Item>();

    public Item GetById(int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Id == id)
                return itemList[i].GetCopy();
        }
        return null;
    }
}