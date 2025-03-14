using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name; 
    public int Id;                                         
    public string Description;                                 
    public Sprite Icon;                               
    public GameObject Model;                                
    public ItemType Type;
    
    public Item(string name, int id, string desc, Sprite icon, GameObject model) 
    {
        Name = name;
        Id = id;
        Description = desc;
        Icon = icon;
        Model = model;
    }

    public Item GetCopy()
    {
        return (Item)this.MemberwiseClone();        
    }   
}