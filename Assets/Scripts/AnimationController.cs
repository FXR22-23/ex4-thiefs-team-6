using System;
using UnityEngine.InputSystem;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject footsteps;

    private void OnEnable()
    {
        move.action.started += AnimateLegs;
        move.action.canceled += StopAnimation;
    }

    private void OnDisable()
    {
        move.action.started -= AnimateLegs;
        move.action.canceled -= StopAnimation;
    }

    private void AnimateLegs(InputAction.CallbackContext obj)
    {
        bool isMovingForward = move.action.ReadValue<Vector2>().y > 0;

        if (isMovingForward)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("AnimSpeed", 1);
            footsteps.SetActive(true);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("AnimSpeed", -1);
            footsteps.SetActive(true);
        }
    }

    private void StopAnimation(InputAction.CallbackContext obj)
    {
        animator.SetBool("IsWalking", false);
        animator.SetFloat("AnimSpeed", 0);
        footsteps.SetActive(false);
    }
}
