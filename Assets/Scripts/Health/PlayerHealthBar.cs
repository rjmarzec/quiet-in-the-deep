using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Image healthContainer;
    [SerializeField] private Image healthFill;

    void Start() {
        if (Player.instance != null && Player.instance.Health != null) {
            Player.instance.Health.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(
                Player.instance.Health.CurrentHealth,
                Player.instance.Health.CurrentHealth,
                Player.instance.Health.MaxHealth);
        }
    }

    private void OnDestroy() {
        if (Player.instance != null && Player.instance.Health != null) {
            Player.instance.Health.OnHealthChanged -= OnHealthChanged;
        }
    }

    void OnHealthChanged(int previous, int current, int max) {
        if (healthFill != null) {
            float percent = (float)current / max;
            healthFill.fillAmount = percent;
        }
    }

}
