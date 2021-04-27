using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuUI : MonoBehaviour {

    [Header("References")]
    private CanvasGroup canvasGroup;
    private CanvasGroup CanvasGroup {
        get {
            if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
            if (canvasGroup == null) { canvasGroup = gameObject.AddComponent<CanvasGroup>(); }
            return canvasGroup;
        }
    }

    private bool open;

    void Start() {
        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            StopAllCoroutines();
            if (open) { Disappear(); } else { Appear(); }
        }
    }

    public void Appear() {
        if (open) { return; }
        open = true;
        Time.timeScale = 0;
        StopAllCoroutines();
        StartCoroutine(AppearRunner());
    }

    private IEnumerator AppearRunner() {
        float elapsedTime = 0;
        float waitTime = .2f;

        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;

        while (elapsedTime < waitTime) {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, 1, elapsedTime / waitTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        CanvasGroup.alpha = 1f;
    }

    public void Disappear() {
        if (!open) { return; }
        open = false;

        if (LevelManager.instance != null && !LevelManager.instance.LevelStarted) {
            Time.timeScale = 0;
        } else Time.timeScale = 1;

        StopAllCoroutines();
        StartCoroutine(DisappearRunner());
    }

    private IEnumerator DisappearRunner() {
        float elapsedTime = 0;
        float waitTime = .2f;

        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;

        while (elapsedTime < waitTime) {
            CanvasGroup.alpha = Mathf.Lerp(CanvasGroup.alpha, 0, elapsedTime / waitTime);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        CanvasGroup.alpha = 0f;
    }
}
