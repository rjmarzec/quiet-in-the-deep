using UnityEngine;
using UnityEngine.UI;

public class LevelObjectivesUI : MonoBehaviour {

    [SerializeField] private Text textComponent;

    void Start() {
        if (LevelManager.instance != null) {
            LevelManager.instance.OnObjectiveCollected += OnObjectiveCollected;
            OnObjectiveCollected(LevelManager.instance.CountCollectedObjectives(), LevelManager.instance.Objectives.Count);
        }
    }

    private void OnDestroy() {
        if (LevelManager.instance != null) {
            LevelManager.instance.OnObjectiveCollected -= OnObjectiveCollected;
        }
    }

    void OnObjectiveCollected(int numCollected, int numTotal) {
        if (textComponent != null) {
            textComponent.text = numCollected + " / " + numTotal;
        }
    }
}
