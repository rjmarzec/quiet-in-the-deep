using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PlayerHealthPulse : MonoBehaviour {

    private CanvasGroup canvasGroup;

    [Header("Settings")]
    [SerializeField] private bool pulseOnIncrease;
    [SerializeField] private bool pulseOnDecrease;

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        if (Player.instance != null && Player.instance.Health != null) {
            Player.instance.Health.OnHealthChanged += OnHealthChanged;
        }
    }

    private void OnDestroy() {
        if (Player.instance != null && Player.instance.Health != null) {
            Player.instance.Health.OnHealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(int previous, int current, int max) {
        if (pulseOnDecrease && current < previous) {
            StopAllCoroutines();
            StartCoroutine(Pulse());
        }
        if (pulseOnIncrease && current > previous) {
            StopAllCoroutines();
            StartCoroutine(Pulse());
        }
    }

    private IEnumerator Pulse() {
        float elapsedTime = 0;
        float waitTime = .5f;

        canvasGroup.alpha = 1f;

        while (elapsedTime < waitTime) {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / waitTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}
