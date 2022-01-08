using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class animationControl : NetworkBehaviour
{
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    float velocityY = 0.0f;
    float reloadTime = 3.26f;
    bool isReloading = false;
    bool isDancing = false;
    bool isFiring = false;
    [SerializeField] float acceleration = 5.0f;
    [SerializeField] float deceleration = 5.0f;
    [Header("References")]
    [SerializeField] CharacterController controller = null;
    [SerializeField]  Animator animator = null;
    float maxVelocity = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) return;
        //walk accelartion
        if (Input.GetKey(KeyCode.W) && velocityZ < maxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (Input.GetKey(KeyCode.A) && velocityX > -maxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (Input.GetKey(KeyCode.S) && velocityZ > -maxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (Input.GetKey(KeyCode.D) && velocityX < maxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        //walk deceleration
        if (!Input.GetKey(KeyCode.W) && velocityZ > 0)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ < 0)
                velocityZ = 0;
        }
        if (!Input.GetKey(KeyCode.A) && velocityX < 0)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX > 0)
                velocityX = 0;
        }
        if (!Input.GetKey(KeyCode.S) && velocityZ < 0)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ > 0)
                velocityZ = 0;
        }
        if (!Input.GetKey(KeyCode.D) && velocityX > 0)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX < 0)
                velocityX = 0;
        }

        //floating
        if (Input.GetKey(KeyCode.Space) && velocityY < maxVelocity)
        {
            velocityY += Time.deltaTime * acceleration;
        }
        if (!Input.GetKey(KeyCode.Space) && velocityY > 0)
        {
            velocityY -= Time.deltaTime * deceleration;
            if (velocityY < 0)
                velocityY = 0;
        }

        //firing
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isFiring = true;
        }
        else isFiring = false;

        //reload
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
        //dance
        if (Input.GetKey(KeyCode.F))
        {
            StartCoroutine(Dance());
        }

        animator.SetFloat("velocityX", velocityX);
        animator.SetFloat("velocityZ", velocityZ);
        animator.SetFloat("velocityY", velocityY);
        animator.SetBool("isFiring", isFiring);
        animator.SetBool("isReloading", isReloading);
        animator.SetBool("isDancing", isDancing);

    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
    IEnumerator Dance()
    {
        isDancing = true;
        yield return new WaitForSeconds(6.2f);
        isDancing = false;
    }
}
