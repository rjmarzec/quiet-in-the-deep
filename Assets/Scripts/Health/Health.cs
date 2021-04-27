using System;
using UnityEngine;

public class Health : MonoBehaviour {

    // previous health, current health, max health
    public event Action<int, int, int> OnHealthChanged;

    [Header("Settings")]

    [SerializeField] private int maxHealth;
    public int MaxHealth {
        get { return maxHealth; }
        set {
            int previousMaxHealth = maxHealth;
            maxHealth = Mathf.Max(0, value);
            if (maxHealth != previousMaxHealth) {
                OnHealthChanged?.Invoke(currentHealth, currentHealth, maxHealth);
            }
        }
    }

    [Header("States")]

    [SerializeField] private int currentHealth;
    public int CurrentHealth {
        get { return currentHealth; }
        set {
            int previousHealth = currentHealth;
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            if (currentHealth != previousHealth) {
                if (currentHealth > previousHealth && soundOnHealthIncrease != null) { soundOnHealthIncrease.Play(); }
                if (currentHealth < previousHealth && soundOnHealthDecrease != null) { soundOnHealthDecrease.Play(); }
                OnHealthChanged?.Invoke(previousHealth, currentHealth, maxHealth);
            }
        }
    }

    [Header("References")]
    [SerializeField] private SFXEvent soundOnHealthIncrease;
    [SerializeField] private SFXEvent soundOnHealthDecrease;

    public bool HasFullHealth {
        get { return CurrentHealth == MaxHealth; }
    }

    private void Start() {
        CurrentHealth = MaxHealth;
    }
}
