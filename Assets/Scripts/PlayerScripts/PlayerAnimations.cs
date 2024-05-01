using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private static PlayerAnimations instance;
    public static PlayerAnimations Instance
    {
        get { 
            if (instance == null)
                instance = FindAnyObjectByType<PlayerAnimations>();
            return instance;
        }
    }

    [SerializeField] private Animator animator;
    [SerializeField] private string idleName;
    [SerializeField] private string jumpname;
    [SerializeField] private string walkName;
    [SerializeField] private string shootName;
    private string currentState;


    public void ChangeAnimation(PlayerAnim newAnim)
    {

        switch (newAnim) {
            case PlayerAnim.Idle: 
                ChangeAnimationState(idleName); break;

            case PlayerAnim.Jump:
                ChangeAnimationState(jumpname); break;

            case PlayerAnim.Walk:
                ChangeAnimationState(walkName); break;

            case PlayerAnim.Shoot:
                ChangeAnimationState(shootName); break;

            default: throw new System.Exception("error"); 
        }
    }
    private void ChangeAnimationState(string newState)
    {
        Debug.Log(newState);
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}

public enum PlayerAnim
{
    Idle,
    Jump,
    Walk,
    Shoot
}
