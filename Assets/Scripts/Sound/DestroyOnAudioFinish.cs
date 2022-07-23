using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAudioFinish : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitCoroutine());
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(audioSource.clip.length/4);
        Destroy(this.gameObject);
    }
}
