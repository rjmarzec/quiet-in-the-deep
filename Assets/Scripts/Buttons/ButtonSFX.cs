using UnityEngine;

[RequireComponent(typeof(UpDownButton))]
public class ButtonSFX : MonoBehaviour {

    [Header("SFX")]
    [SerializeField] private SFXEvent DownSFX;
    [SerializeField] private SFXEvent UpSFX;

    [Header("References")]
    private UpDownButton button;

    private void Awake() {
        button = GetComponent<UpDownButton>();
        button.onPointerDown += OnPointerDown;
        button.onPointerClick += onPointerClick;
    }

    private void OnDestroy() {
        button.onPointerDown -= OnPointerDown;
        button.onPointerClick -= onPointerClick;
    }

    private void OnPointerDown() {
        if (!button.interactable) { return; }
        DownSFX?.Play();
    }

    private void onPointerClick() {
        if (!button.interactable) { return; }
        UpSFX?.Play();
    }

}