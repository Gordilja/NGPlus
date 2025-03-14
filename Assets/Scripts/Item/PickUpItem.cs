using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private ItemDataBase ItemDataBase;
    [SerializeField] private int ItemId;
    private Item item;
    private PlayerInventory _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerInventory>();
        item = ItemDataBase.GetById(ItemId);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float distance = Vector3.Distance(this.gameObject.transform.position, _player.transform.position);

            if (distance <= 3)
            {
                _player.AddItem(item);
                Destroy(this.gameObject);
            }
        }
    }
}