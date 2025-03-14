using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAnimations PlayerAnimations;
    [SerializeField] private PlayerInventory PlayerInventory;

    public float moveSpeed = 5f; // Speed of movement
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        PlayerAnimations.UpdateVelocity(horizontal, vertical);
        PlayerInventory.InventoryPlayerInput();
    }
}