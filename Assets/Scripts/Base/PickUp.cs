using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public abstract class PickUp : MonoBehaviour
{
    protected abstract void OnPickup(PlayerController playerController);


    [Header("Feedback")]
    [SerializeField] AudioClip _pickupSFX = null;
    [SerializeField] ParticleSystem _particlePrefab = null;
    Collider _collider = null;
    AudioSource _audioSource = null;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, 200 * Time.deltaTime);
    }

    // Reset gets called whenever you add a component to an object
    private void Reset()
    {
        // set isTrigger in the Inspector, just in case Designer forgot
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        // guard clause
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController == null)
            return;

        // found the player! call our abstract method and supporting feedback
        OnPickup(playerController);

        if (_pickupSFX != null)
        {
            SpawnAudio(_pickupSFX);
        }

        if (_particlePrefab != null)
        {
            SpawnParticle(_particlePrefab);
        }

        Disable();
    }
   

    void SpawnAudio(AudioClip pickupSFX)
    {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
    }

    void SpawnParticle(ParticleSystem pickupParticles)
    {
        ParticleSystem newParticles =
            Instantiate(pickupParticles, transform.position, Quaternion.identity);
        newParticles.Play();
    }

    // allow override in case subclass wants to object pool, etc.
    protected virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
