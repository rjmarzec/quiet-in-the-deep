using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpDownButton : Button {

    public event Action onPointerDown;
    public event Action onPointerUp;
    public event Action onPointerEnter;
    public event Action onPointerExit;
    public event Action onPointerClick;
    public event Action<bool> onInteractableChanged;

    public new bool interactable {
        get {
            return base.interactable;
        }
        set {
            if (value != interactable) {
                base.interactable = value;
                onInteractableChanged?.Invoke(interactable);
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData) {
        base.OnPointerDown(eventData);
        onPointerDown?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData) {
        base.OnPointerUp(eventData);
        onPointerUp?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        base.OnPointerEnter(eventData);
        onPointerEnter?.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData) {
        base.OnPointerExit(eventData);
        onPointerExit?.Invoke();
    }

    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);
        onPointerClick?.Invoke();
    }

}