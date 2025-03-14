using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void UpdateVelocity(float horizontal, float vertical) 
    {
        _animator.SetFloat("VelocityX", horizontal);
        _animator.SetFloat("VelocityZ", vertical);
    }
}
