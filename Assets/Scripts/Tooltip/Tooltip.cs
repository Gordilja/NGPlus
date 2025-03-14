using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI Description;
    private RectTransform recTransform;
    void Start()
    {
        recTransform = GetComponent<RectTransform>();
        Deactivate();
    }

    public void Activate(Item _item) 
    {
        transform.gameObject.SetActive(true);
        ItemName.text = _item.Name;
        Description.text = _item.Description;       
    }

    public void Deactivate() 
    {
        transform.gameObject.SetActive(false);
    }
}