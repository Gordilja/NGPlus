using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    //Tooltip Objects

    void Start()
    {
        Deactivate();
    }

    public void Activate(Item _item) 
    {
        transform.gameObject.SetActive(true);
        transform.GetChild(2).GetComponent<Text>().text = _item.Name;
        transform.GetChild(3).GetComponent<Text>().text = _item.Description;       
    }

    public void Deactivate() 
    {
        transform.gameObject.SetActive(false);
    }
}