using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverUI : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Text deathMethodTextComponent;
    [SerializeField] private GameObject deadPlayerImageEaten;
    [SerializeField] private GameObject deadPlayerImageDrowned;

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
            LevelManager.instance.OnLevelFailed += OnLevelFailed;
        }

        CanvasGroup.alpha = 0;
        CanvasGroup.interactable = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        if (LevelManager.instance != null) {
            LevelManager.instance.OnLevelFailed -= OnLevelFailed;
        }
    }

    private void OnLevelFailed(Player.DeathMethod deathMethod) {
        ResolveDeathText(deathMethod);
        ResolveDeathImage(deathMethod);

        gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Appear());
    }

    private void ResolveDeathText(Player.DeathMethod deathMethod) {
        if (deathMethodTextComponent != null) {
            switch (deathMethod) {
                case Player.DeathMethod.Drowned:
                    deathMethodTextComponent.text = "<color=blue>You drowned.</color>";
                    break;
                case Player.DeathMethod.Eaten:
                    deathMethodTextComponent.text = "<color=red>You were eaten.</color>";
                    break;
            }
        }
    }

    private void ResolveDeathImage(Player.DeathMethod deathMethod) {
        if (deadPlayerImageEaten != null) {
            deadPlayerImageEaten.SetActive(deathMethod == Player.DeathMethod.Eaten);
        }
        if (deadPlayerImageDrowned != null) {
            deadPlayerImageDrowned.SetActive(deathMethod == Player.DeathMethod.Drowned);
        }
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
