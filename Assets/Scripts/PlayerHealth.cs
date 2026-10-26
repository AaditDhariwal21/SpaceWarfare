using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth = 150f;
    public float MaxHealth => maxHealth; 
    [SerializeField] private float currentHealth;
    private bool isAlive;
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private SpriteRenderer shipRenderer;
    [SerializeField] private Color flashColor = Color.black;
    [SerializeField] private int flashCount = 3;
    [SerializeField] private float flashInterval = 0.05f;

    private Coroutine flashCoroutine;

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

        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(Flash());
    }
    public float getCurrentHealth()
    {
        return currentHealth;
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < flashCount; i++)
        {
        shipRenderer.color = flashColor;
        yield return new WaitForSeconds(flashInterval);
        shipRenderer.color = Color.white;
        yield return new WaitForSeconds(flashInterval);
        }
        flashCoroutine = null;
    }
}
