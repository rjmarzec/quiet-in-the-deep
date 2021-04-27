using UnityEngine;

public class Hazard : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private int damage;
    public int Damage {
        get { return damage; }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (Player.instance != null && Player.instance.Health != null) {
                Player.instance.Health.CurrentHealth -= damage;
            }
        }
    }
}
