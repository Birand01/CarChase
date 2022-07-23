using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnParticleEffectHandler(bool state);
    public delegate void OnTrailEffectHandler(bool state);
    public delegate void OnDriftSoundHandler(bool state);

    //public static event OnDriftSoundHandler OnDriftSound;
    public static event OnParticleEffectHandler OnParticleEffect;
    public static event OnTrailEffectHandler OnTrailEffect;


    [SerializeField] private float speed;
    [SerializeField] private float angleSpeed;
    private Rigidbody _rb;
    private int currentAngle;
   
   public float CurrentSpeed
    {
        get { return speed; }
        set
        {
            speed = value;
        }
    }

    private void Awake()
    {
        OnParticleEffect?.Invoke(false);
        OnTrailEffect?.Invoke(false);
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(UIManager.GetInstance().GameStarted)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
       
    }
    private void Update()
    {
        if (UIManager.GetInstance().GameStarted)
        {
            RotateCar();
        }
    }

    private void MoveLeft()
    {
        transform.Rotate(-Vector3.up * angleSpeed * Time.deltaTime);
    }
    private void MoveRight()
    {
        transform.Rotate(Vector3.up * angleSpeed * Time.deltaTime);
    }

    private void RotateCar()
    {
        if (Input.GetMouseButton(0))
        {
            //OnDriftSound?.Invoke(true);
            OnTrailEffect?.Invoke(true);
            OnParticleEffect?.Invoke(true);
            float x = Input.mousePosition.x;
            if (x < Screen.width / 2 && x > 0)
            {
                MoveLeft();
            }
            if (x > Screen.width / 2 && x < Screen.width)
            {
                MoveRight();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            //OnDriftSound?.Invoke(false);
            OnParticleEffect?.Invoke(false);
            OnTrailEffect?.Invoke(false);
        }
    }
   
   
   
}
