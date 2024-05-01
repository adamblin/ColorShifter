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
    private PlayerAnim lastAnim;


    public void ChangeAnimationState(PlayerAnim newAnim)
    {
        if (lastAnim != newAnim) { 
            lastAnim = newAnim;

            switch (newAnim) {
                case PlayerAnim.Idle: 
                    animator.Play(idleName); break;

                case PlayerAnim.Jump: 
                    animator.Play(jumpname); break;

                case PlayerAnim.Walk: 
                    animator.Play(walkName); break;

                case PlayerAnim.Shoot: 
                    animator.Play(shootName); break;

                default: throw new System.Exception("error"); 
            }
        }
    }

    public enum PlayerAnim { 
        Idle,
        Jump,
        Walk,
        Shoot
    }
}
