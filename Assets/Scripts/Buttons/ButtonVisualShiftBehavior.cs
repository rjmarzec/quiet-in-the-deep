using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UpDownButton))]
[RequireComponent(typeof(CanvasGroup))]
public class ButtonVisualShiftBehavior : MonoBehaviour {

    [Header("Settings")]

    [Range(0.0f, 1.0f)]
    [SerializeField] private float DisabledTransparency = 1f;
    [SerializeField] private bool DownWhenDisabled;

    [Header("Shifts applied when button is down.")]
    [SerializeField] private Vector2Int ShiftSelf;
    [SerializeField] private Vector2Int ShiftChildren;

    [Header("States")]
    private bool down;
    private bool pressed;

    [Header("References")]
    private UpDownButton button;
    private CanvasGroup canvasGroup;
    private Image image;

    private bool slicedImage;
    private float slicedMultiplier;

    private void OnEnable() {
        button = GetComponent<UpDownButton>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        slicedImage = image.type == Image.Type.Sliced;
        if (slicedImage) { slicedMultiplier = image.pixelsPerUnitMultiplier; }
        button.onPointerDown += OnPointerDown;
        button.onPointerUp += OnPointerUp;
        button.onPointerEnter += OnPointerEnter;
        button.onPointerExit += OnPointerExit;
        button.onInteractableChanged += OnInteractableChange;
        OnInteractableChange(button.interactable);
    }

    private void OnInteractableChange(bool interactable) {
        if (canvasGroup) { canvasGroup.alpha = interactable ? 1f : DisabledTransparency; }
        if (!interactable && DownWhenDisabled) { GoDown(); } else GoUp();
    }

    private void OnPointerDown() {
        if (!button.interactable) { return; }
        if (slicedImage) { image.pixelsPerUnitMultiplier = slicedMultiplier; }
        pressed = true;
        GoDown();
    }

    private void OnPointerUp() {
        if (!button.interactable) { return; }
        if (slicedImage) { image.pixelsPerUnitMultiplier = slicedMultiplier; }
        pressed = false;
        GoUp();
    }

    private void OnPointerEnter() {
        if (!button.interactable) { return; }
        if (slicedImage) { image.pixelsPerUnitMultiplier = slicedMultiplier; }
        if (pressed) { GoDown(); }
    }

    private void OnPointerExit() {
        if (!button.interactable) { return; }
        if (slicedImage) { image.pixelsPerUnitMultiplier = slicedMultiplier; }
        if (!pressed) { GoUp(); }
    }

    private void GoDown() {
        if (down) { return; }
        down = true;
        ShiftSelfXY(-ShiftSelf.x, -ShiftSelf.y);
        ShiftChildrenXY(-ShiftChildren.x, -ShiftChildren.y);
    }

    private void GoUp() {
        if (!down) { return; }
        down = false;
        ShiftSelfXY(ShiftSelf.x, ShiftSelf.y);
        ShiftChildrenXY(ShiftChildren.x, ShiftChildren.y);
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Helpers

    private void ShiftSelfXY(int x, int y) {
        transform.localPosition = new Vector3(
            transform.localPosition.x + x,
            transform.localPosition.y + y
        );
    }

    private void ShiftChildrenXY(int x, int y) {
        foreach (Transform child in transform) {
            child.localPosition = new Vector3(
                child.localPosition.x + x,
                child.localPosition.y + y
            );
        }
    }

    private void OnDestroy() {
        button.onPointerDown -= OnPointerDown;
        button.onPointerUp -= OnPointerUp;
        button.onPointerEnter -= OnPointerEnter;
        button.onPointerExit -= OnPointerExit;
        button.onInteractableChanged -= OnInteractableChange;
    }

}
