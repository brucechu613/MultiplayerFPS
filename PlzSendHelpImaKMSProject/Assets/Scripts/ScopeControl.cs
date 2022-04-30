using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ScopeControl : NetworkBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] Camera mainCamera = null;
    [SerializeField] Camera weaponCamera = null;

    bool scoping = false;
    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;

        if (Input.GetKey(KeyCode.Mouse1))
        {
            scoping = true;

            StartCoroutine(changeFOV());
        }
        else
        {
            scoping = false;

            mainCamera.fieldOfView = 70f;
        }

        animator.SetBool("Scoping", scoping);
    }
    IEnumerator changeFOV()
    {
        yield return new WaitForSeconds(.15f);
        mainCamera.fieldOfView = 50f;
    }
}
