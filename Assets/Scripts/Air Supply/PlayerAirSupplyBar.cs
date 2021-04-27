using UnityEngine;
using UnityEngine.UI;

public class PlayerAirSupplyBar : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Image airSupplyContainer;
    [SerializeField] private Image airSupplyFill;

    void Start() {
        if (Player.instance != null && Player.instance.AirSupply != null) {
            Player.instance.AirSupply.OnAirSupplyChanged += OnAirSupplyChanged;
            OnAirSupplyChanged(
                Player.instance.AirSupply.CurrentAirSupply,
                Player.instance.AirSupply.CurrentAirSupply,
                Player.instance.AirSupply.MaxAirSupply);
        }
    }

    private void OnDestroy() {
        if (Player.instance != null && Player.instance.AirSupply != null) {
            Player.instance.AirSupply.OnAirSupplyChanged -= OnAirSupplyChanged;
        }
    }

    void OnAirSupplyChanged(float previous, float current, float max) {
        if (airSupplyFill != null) {
            float percent = current / max;
            airSupplyFill.fillAmount = percent;
        }
    }

}
