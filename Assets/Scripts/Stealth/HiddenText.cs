using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HiddenText : MonoBehaviour {

    private Text textComponent;
    private Text TextComponent {
        get {
            if (textComponent == null) { textComponent = GetComponent<Text>(); }
            if (textComponent == null) { textComponent = gameObject.AddComponent<Text>(); }
            return textComponent;
        }
    }

    void Start() {
        if (Player.instance != null && Player.instance.Stealth != null) {
            Player.instance.Stealth.OnHidingStateChange += OnHidingStateChange;
            Player.instance.OnTrackingEnemiesUpdated += ResolveState;
        }
        ResolveState();
    }

    private void OnEnable() {
        FollowPlayer();
    }

    void LateUpdate() {
        FollowPlayer();
    }

    private void FollowPlayer() {
        if (Player.instance != null) {
            transform.position = Camera.main.WorldToScreenPoint(Player.instance.transform.position);
        }
    }

    private void OnHidingStateChange(bool hiding) {
        ResolveState();
    }

    private void ResolveState() {
        if (Player.instance != null) {

            // Tracked
            if (Player.instance.IsTracked) { TextComponent.text = "Spotted!"; }

            // Hidden
            else if (Player.instance.Stealth != null && Player.instance.Stealth.Hiding) { TextComponent.text = "Hidden"; }

            // Default
            else TextComponent.text = "";

        } else TextComponent.text = "";
    }

}
