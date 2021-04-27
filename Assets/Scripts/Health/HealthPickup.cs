using UnityEngine;

public class HealthPickup : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private int amount;
    public int Amount {
        get { return amount; }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (Player.instance != null && Player.instance.Health != null && !Player.instance.Health.HasFullHealth) {
                Player.instance.Health.CurrentHealth += amount;
                Destroy(gameObject);
            }
        }
    }

}
