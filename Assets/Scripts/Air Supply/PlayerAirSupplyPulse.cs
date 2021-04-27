using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerAirSupplyPulse : MonoBehaviour {

    private CanvasGroup canvasGroup;

    private Coroutine pulseCoroutine;

    private float maxAlpha;
    private float pulsePace;

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        if (Player.instance != null && Player.instance.AirSupply != null) {
            Player.instance.AirSupply.OnAirSupplyChanged += OnAirSupplyChanged;
        }
    }

    private void OnDestroy() {
        if (Player.instance != null && Player.instance.AirSupply != null) {
            Player.instance.AirSupply.OnAirSupplyChanged -= OnAirSupplyChanged;
        }
    }

    private void OnAirSupplyChanged(float previous, float current, float max) {
        if (current > 15 && pulseCoroutine != null) {
            maxAlpha = 0;
            StopAllCoroutines();
        }

        if (current <= 15 && pulseCoroutine == null) {
            pulseCoroutine = StartCoroutine(Pulse());
        }

        if (current < 2) { maxAlpha = 1f; } else if (current < 4) {
            maxAlpha = .8f;
            pulsePace = .3f;
        } else if (current < 6) {
            maxAlpha = .6f;
            pulsePace = .4f;
        } else if (current < 8) {
            maxAlpha = .4f;
            pulsePace = .5f;
        } else if (current < 10) {
            maxAlpha = .3f;
            pulsePace = .6f;
        } else if (current < 12) {
            maxAlpha = .2f;
            pulsePace = .7f;
        } else if (current < 14) {
            maxAlpha = .1f;
            pulsePace = .8f;
        } else {
            maxAlpha = 0f;
            pulsePace = .9f;
        }
    }

    private IEnumerator Pulse() {
        float elapsedTime = 0f;

        while (Player.instance.AirSupply.CurrentAirSupply < 15) {

            elapsedTime = 0;
            float target = maxAlpha;
            float waitTime = pulsePace;

            canvasGroup.alpha = 0f;
            while (elapsedTime < waitTime) {
                canvasGroup.alpha = Mathf.Lerp(0, target, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0;
            float initial = target;
            waitTime = pulsePace;

            canvasGroup.alpha = target;
            while (elapsedTime < waitTime) {
                canvasGroup.alpha = Mathf.Lerp(initial, 0, elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0f;
        }

        pulseCoroutine = null;
    }

}