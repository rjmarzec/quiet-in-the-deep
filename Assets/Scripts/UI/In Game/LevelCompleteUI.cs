using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LevelCompleteUI : MonoBehaviour {

    [Header("References")]
    private CanvasGroup canvasGroup;
    private CanvasGroup CanvasGroup {
        get {
            if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
            if (canvasGroup == null) { canvasGroup = gameObject.AddComponent<CanvasGroup>(); }
            return canvasGroup;
        }
    }

    void Start() {
        if (LevelManager.instance != null) {
            LevelManager.instance.OnLevelCompleted += OnLevelCompleted;
        }

        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        if (LevelManager.instance != null) {
            LevelManager.instance.OnLevelCompleted -= OnLevelCompleted;
        }
    }

    private void OnLevelCompleted() {
        gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Appear());
    }

    private IEnumerator Appear() {
        float elapsedTime = 0;
        float waitTime = .6f;

        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = true;

        while (elapsedTime < waitTime) {
            CanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / waitTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        CanvasGroup.alpha = 1f;
    }
}
