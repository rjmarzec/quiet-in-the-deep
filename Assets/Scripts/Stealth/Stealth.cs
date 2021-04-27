using System;
using UnityEngine;

public class Stealth : MonoBehaviour {

    public event Action<bool> OnHidingStateChange;

    [Header("States")]
    [SerializeField] private int hidingSpots;

    public bool Hiding {
        get { return hidingSpots > 0; }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Hiding Spot") {
            hidingSpots++;
            OnHidingStateChange?.Invoke(hidingSpots > 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Hiding Spot") {

        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Hiding Spot") {
            hidingSpots--;
            OnHidingStateChange?.Invoke(hidingSpots > 0);
        }
    }
}
