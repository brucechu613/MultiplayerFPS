using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundPlay :MonoBehaviour
{
    [SerializeField] AudioSource source=null;
    bool isPlaying = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (isPlaying == false)
            {
                isPlaying = true;
                source.Play();
                StartCoroutine(Wait());
            }
        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(6.2f);
        isPlaying = false;
    }


}
