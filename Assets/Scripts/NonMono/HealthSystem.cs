using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int health;
    private int maxHealth;
    public int Health => health;

    public HealthSystem(int newHealth)
    {
        this.health = newHealth;
        this.maxHealth = newHealth;
    }

    public float GetHealthPercent() => health / (float)maxHealth;

    void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }

    public void Damage(int damageAmount) => ChangeHealth(-damageAmount);

    public void Heal(int healAmount) => ChangeHealth(healAmount);
}
