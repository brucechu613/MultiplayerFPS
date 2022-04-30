using Mirror;
using System.Collections;
using UnityEngine;

public class Robot : NetworkBehaviour
{

    [SerializeField]
    private static int maxHealth = 100;

    [SerializeField]
    private GameObject deathEffect;

    [SyncVar]
    private int currentHealth = maxHealth;

    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }
    //Is called on the server when a enemy attack
    [SyncVar]
    public RobotAnumaPara animationPara;

    [SyncVar]
    private float attackCD = 0;

    [Server]
    public void attack(Player target)
    {
        if (attackCD <= 0)
        {
            animationPara.triggerAttack();
            attackCD = 10;
            target.RpcTakeDamage(20, "");
        }
    }

    bool isDead = false;

    [Server]
    public void RpcSetTarget(string _sourceID)
    {
        if (!isDead)
        {
            currentHealth -= 5;
            if (currentHealth <= 0)
            {
                Die();
            }
            Player targetPlayer = GameManager.GetPlayer(_sourceID);
            target = targetPlayer;
        }
    }


    [ClientRpc]
    void Die()
    {
        StartCoroutine(aboutToDie());
    }

    IEnumerator aboutToDie()
    {
        //Print the time of when the function is first called.
        target = null;
        animationPara.setIsWalking(false);
        isDead = true;

        yield return new WaitForSeconds(0);

        GameObject _gfxIns = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);
        
        Destroy(gameObject, 0f);
    }

    [SyncVar]
    private Player target = null;
    [SerializeField]
    private float distance = 1f;
    [SerializeField]
    private float speed = 3f;

    [Server]
    void FixedUpdate()
    {
        if (!isDead)
        {
            transform.GetComponent<Rigidbody>().AddForce(0,-100,0,ForceMode.Acceleration);
            if (target != null)
            {
                transform.LookAt(target.transform);
                Vector3 eulerRotation = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
                if (Vector3.Distance(target.transform.position, transform.position) <= distance)
                {
                    attack(target);
                    if (target.isDead)
                    {
                        target = null;
                        animationPara.setIsWalking(false);
                    }
                    animationPara.setIsWalking(false);
                }
                else
                {
                    animationPara.setIsWalking(true);
                    transform.position += transform.forward * speed * Time.fixedDeltaTime;
                }
            }

            if (!(attackCD <= 0))
            {
                attackCD -= 8 * Time.fixedDeltaTime;
            }
        }
    }
}
