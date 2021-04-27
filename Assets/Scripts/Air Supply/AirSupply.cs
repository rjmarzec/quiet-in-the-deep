using System;
using UnityEngine;

public class AirSupply : MonoBehaviour {

    // previous supply, current supply, max supply
    public event Action<float, float, float> OnAirSupplyChanged;

    [Header("Settings")]

    [SerializeField] private float maxAirSupply;
    public float MaxAirSupply {
        get { return maxAirSupply; }
        set {
            float previousMaxHealth = maxAirSupply;
            maxAirSupply = Mathf.Max(0, value);
            OnAirSupplyChanged?.Invoke(CurrentAirSupply, CurrentAirSupply, MaxAirSupply);
        }
    }

    [Header("States")]

    [SerializeField] private float currentAirSupply;
    public float CurrentAirSupply {
        get { return currentAirSupply; }
        set {
            float previousAirSupply = currentAirSupply;
            currentAirSupply = Mathf.Clamp(value, 0, MaxAirSupply);
            OnAirSupplyChanged?.Invoke(previousAirSupply, CurrentAirSupply, MaxAirSupply);
        }
    }

    private void Start() {
        CurrentAirSupply = MaxAirSupply;
    }

    private void Update() {
        CurrentAirSupply -= Time.deltaTime;
    }
}
