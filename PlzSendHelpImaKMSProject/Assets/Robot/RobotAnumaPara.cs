using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RobotAnumaPara : NetworkBehaviour
{
    [SerializeField] Animator animator = null;


    [ClientRpc]
    public void setIsWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    [ClientRpc]
    public void triggerAttack()
    {
        animator.SetTrigger("Attack");
    }
}
