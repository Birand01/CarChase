using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UniversalHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private float health = 0;
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] Color zeroHealthColor;
    [SerializeField] Color fullHealthColor;
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

        }
    }

    protected virtual void Start()
    {
        healthBarSlider.value = maxHealth;
        if (health == 0)
        {
            Health = maxHealth;
        }

    }

    protected virtual void Update()
    {
        SetHealthBarUI();
    }

    protected virtual void SetHealthBarUI()
    {
        float healthPercentage = CalculateHealthPercentage();
        healthBarSlider.value = healthPercentage;
        healthBarFillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercentage / maxHealth);

    }
    protected virtual float CalculateHealthPercentage()
    {
        return ((float)Health / (float)maxHealth) * 100;
    }
}
