using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioSync))]
public class PlaySyncSound : NetworkBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private animationControl animationControl;
    bool isPlaying = false;
    bool isFiring = false;
    bool isOrange = false;
    float musicDuration = 6.2f;
    float shotsDuration = 0.1f;
    private AudioSync audioSync;
    void Start()
    {
        audioSync = this.GetComponent<AudioSync>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKey(KeyCode.F)&&!isPlaying&&!animationControl.isOrange)
            {
                audioSync.PlaySound(1);
                isPlaying = true;
                StartCoroutine(musicWait());
            }
            if (Input.GetKey(KeyCode.Mouse0)&&!isFiring&&!weaponManager.isReloading)
            {
                audioSync.PlaySound(2);
                isFiring = true;
                StartCoroutine(shotWait());
            }
            if (Input.GetKey(KeyCode.G)&&!isOrange&&!animationControl.isDancing)
            {
                audioSync.PlaySound(3);
                isOrange = true;
                StartCoroutine(orangeWait());
            }
        }
        
    }
    IEnumerator musicWait()
    {
        yield return new WaitForSeconds(musicDuration);
        isPlaying = false;
    }
    IEnumerator shotWait()
    {
        yield return new WaitForSeconds(shotsDuration);
        isFiring= false;
    }
    IEnumerator orangeWait()
    {
        yield return new WaitForSeconds(4.01f);
        isOrange= false;
    }
}
