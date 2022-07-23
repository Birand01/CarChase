using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedPowerUp : MonoBehaviour
{
    public delegate void OnSpeedPowerUpHandler(bool state);
    public static event OnSpeedPowerUpHandler OnSpeedPowerUp;


    [SerializeField] private float _speedIncreaseAmount;
    [SerializeField] private float _powerUpDuration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject artToDisable=null;
    [SerializeField] private AudioClip speedUpSound;
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if(playerController!=null)
        {
            StartCoroutine(PowerUpSequence(playerController));
            SpawnAudio(speedUpSound);
        }

    }
    public IEnumerator  PowerUpSequence(PlayerController playerController)
    {
        _collider.enabled = false;
        artToDisable.SetActive(false);
        OnSpeedPowerUp?.Invoke(true);
        playerController.CurrentSpeed += _speedIncreaseAmount;
        yield return new WaitForSeconds(_powerUpDuration);
        playerController.CurrentSpeed -= _speedIncreaseAmount;
        SpeedUpSpawner.GetInstance().CurrentSpeedUpGem--;
        OnSpeedPowerUp?.Invoke(false);
        gameObject.SetActive(false);
    }

    void SpawnAudio(AudioClip pickupSFX)
    {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
    }
} 
