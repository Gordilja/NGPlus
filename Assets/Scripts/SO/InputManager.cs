using UnityEngine;

[CreateAssetMenu(fileName = "InputManagerSO", menuName = "ScriptableObjects/InputManager", order = 1)]
public class InputManager : ScriptableObject
{
    public KeyCode InventoryKeyCode;
    public KeyCode StorageKeyCode;
    public KeyCode CharacterSystemKeyCode;
}