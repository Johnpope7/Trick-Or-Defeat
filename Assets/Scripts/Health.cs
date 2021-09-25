using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField]
    private float MaxHealth = 100f;
    [SerializeField]
    private float currentHealth = 100f;
    private float percent;

    [Header("Events")]
    [SerializeField, Tooltip("Called every time the object is healed.")]
    private UnityEvent onHeal;
    [SerializeField, Tooltip("Called every time the object is damaged.")]
    private UnityEvent onDamage;
    [SerializeField, Tooltip("Called once when the object's health reaches 0.")]
    private UnityEvent onDeath;


    public float GetHealth() 
    {
        return currentHealth;
    }

    private void SetHealth(float value) 
    {
        currentHealth = value;
    }

    public float GetMaxHealth() 
    {
        return MaxHealth;
    }

    private void SetMaxHealth(float value) 
    {
        MaxHealth = value;
    }

    public float GetPercent() 
    {
        percent = currentHealth / MaxHealth;
        return percent;
    }

    private void SetPercent() 
    {
        percent = currentHealth / MaxHealth;
    }

    public void Heal(float heal) 
    {
        heal = Mathf.Max(heal, 0f);
        currentHealth = Mathf.Clamp(currentHealth + heal, 0f, MaxHealth);
        SendMessage("onHeal", SendMessageOptions.DontRequireReceiver);
    }

    public void FullHeal() 
    {
        currentHealth = MaxHealth;
    }

    public void Damage(float damage) 
    {
        damage = Mathf.Max(damage, 0f);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0f, MaxHealth);
        if (currentHealth <= 0f) 
        {
            onDeath.Invoke();
        }
    }

    public void Kill() 
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

    public void InvokeOnDamage() 
    {
        onDamage.Invoke();
    }

    public void InvoneOnHeal() 
    {
        onHeal.Invoke();
    }

    /// <summary>
    /// This exists to call player death on this object instead of a prefab
    /// </summary>
    public void Die() 
    {
        //Object Pull or destroy
    }
}
