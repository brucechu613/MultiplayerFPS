using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(AudioSource))]
public class AudioSync : NetworkBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip,clip2;
    
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void PlaySound(int ID)
    {
            cmdSendServerSound(ID);
    }
    
    [Command]
    void cmdSendServerSound(int ID)
    {
        rpcSendSoundToClient(ID);
    }

    [ClientRpc]
    void rpcSendSoundToClient(int ID)
    {
        if(ID==1)
            audioSource.PlayOneShot(clip);
        if (ID == 2)
            audioSource.PlayOneShot(clip2);
    }
}
