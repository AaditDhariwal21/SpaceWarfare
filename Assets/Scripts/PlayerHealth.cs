using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 150f;
    public float MaxHealth => maxHealth; 
    [SerializeField] private float currentHealth;
    private bool isAlive;
    public event Action<float, float> OnHealthChanged;

    void Awake()
    {
        isAlive = true;
        currentHealth = maxHealth;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (isAlive && currentHealth <= 0)
    {
        isAlive = false;
        GameManager.gmInstance.PlayerDied(gameObject.tag);
    }
    }

    public void TakeDamage(int dmg)
    {
        if ((!isAlive) || currentHealth <=0 ) return ;
        currentHealth = Math.Max(currentHealth - dmg , 0);

        OnHealthChanged?.Invoke(currentHealth,maxHealth);
    }
    public float getCurrentHealth()
    {
        return currentHealth;
    }
}
