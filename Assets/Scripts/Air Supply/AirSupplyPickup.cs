using UnityEngine;

public class AirSupplyPickup : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private int amount;
    public int Amount {
        get { return amount; }
    }

    [Header("References")]
    [SerializeField] private SFXEvent soundEffect;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (Player.instance != null && Player.instance.AirSupply != null) {
                Player.instance.AirSupply.CurrentAirSupply += amount;
                if (soundEffect != null) { soundEffect.Play(); }
                Destroy(gameObject);
            }
        }
    }

}
