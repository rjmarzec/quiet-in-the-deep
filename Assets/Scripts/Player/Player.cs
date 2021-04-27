using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(AirSupply))]
[RequireComponent(typeof(Stealth))]
public class Player : MonoBehaviour {

    public enum DeathMethod {
        Drowned,
        Eaten
    };

    public static Player instance;

    public event Action OnTrackingEnemiesUpdated;
    public event Action<DeathMethod> OnPlayerDied;

    [Header("States")]
    [SerializeField] private bool alive;
    public bool Alive {
        get {
            return alive;
        }
        private set {
            alive = value;
        }
    }

    private Health health;
    public Health Health {
        get {
            if (health == null) { health = GetComponent<Health>(); }
            if (health == null) { health = gameObject.AddComponent<Health>(); }
            return health;
        }
    }

    private Stealth stealth;
    public Stealth Stealth {
        get {
            if (stealth == null) { stealth = GetComponent<Stealth>(); }
            if (stealth == null) { stealth = gameObject.AddComponent<Stealth>(); }
            return stealth;
        }
    }

    private AirSupply airSupply;
    public AirSupply AirSupply {
        get {
            if (airSupply == null) { airSupply = GetComponent<AirSupply>(); }
            if (airSupply == null) { airSupply = gameObject.AddComponent<AirSupply>(); }
            return airSupply;
        }
    }

    [SerializeField] private List<Enemy> trackingEnemies;
    public List<Enemy> TrackingEnemies {
        get {
            if (trackingEnemies == null) { trackingEnemies = new List<Enemy>(); }
            return trackingEnemies;
        }
    }

    public bool IsTracked {
        get {
            return TrackingEnemies.Count > 0;
        }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else instance = this;
    }

    private void Start() {
        AirSupply.OnAirSupplyChanged += OnAirSupplyChanged;
        Health.OnHealthChanged += OnHealthChanged;

        Alive = true;
    }

    private void OnDestroy() {
        AirSupply.OnAirSupplyChanged -= OnAirSupplyChanged;
        Health.OnHealthChanged -= OnHealthChanged;
    }

    public void OnAirSupplyChanged(float previous, float current, float max) {
        if (Alive && current <= 0.01) {
            Alive = false;
            OnPlayerDied?.Invoke(DeathMethod.Drowned);
        }
    }

    public void OnHealthChanged(int previous, int current, int max) {
        if (Alive && current == 0) {
            Alive = false;
            OnPlayerDied?.Invoke(DeathMethod.Eaten);
        }
    }

    public void RegisterTrackingEnemy(Enemy enemy) {
        if (!TrackingEnemies.Contains(enemy)) {
            TrackingEnemies.Add(enemy);
            OnTrackingEnemiesUpdated?.Invoke();
        }
    }

    public void RemoveTrackingEnemy(Enemy enemy) {
        if (TrackingEnemies.Contains(enemy)) {
            TrackingEnemies.Remove(enemy);
            OnTrackingEnemiesUpdated?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.collider.tag) {

            case "Enemy":
                Health.CurrentHealth -= 1;
                break;

        }
    }

    private void OnCollisionStay2D(Collision2D collision) {

    }

    private void OnCollisionExit2D(Collision2D collision) {

    }

}
