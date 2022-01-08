using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScopeControl : NetworkBehaviour
{
    [SerializeField] Animator animator = null;
    bool scoping = false;
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            scoping = true;
        }
        else scoping = false;

        animator.SetBool("Scoping", scoping);
    }
}
