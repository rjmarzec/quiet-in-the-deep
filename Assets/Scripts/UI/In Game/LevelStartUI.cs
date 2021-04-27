using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LevelStartUI : MonoBehaviour {

    [Header("References")]
    private CanvasGroup canvasGroup;
    private CanvasGroup CanvasGroup {
        get {
            if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
            if (canvasGroup == null) { canvasGroup = gameObject.AddComponent<CanvasGroup>(); }
            return canvasGroup;
        }
    }

    [SerializeField] private Text levelNameTextComponent;

    void Start() {
        CanvasGroup.alpha = 1;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;

        if (LevelManager.instance != null
            && LevelManager.instance.ThisScene != null
            && levelNameTextComponent != null) {
            levelNameTextComponent.text = LevelManager.instance.ThisScene.LevelName;
        }

        Time.timeScale = 0;
    }

    public void Disappear() {
        if (LevelManager.instance != null) {
            LevelManager.instance.StartLevel();
        }

        Time.timeScale = 1;
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
