using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UpDownButton))]
public class LevelSelectButton : MonoBehaviour {

    [SerializeField] private SceneData scene;

    [Header("References")]
    [SerializeField] private Text levelNameTextComponent;
    private UpDownButton button;

    private void Awake() {
        button = GetComponent<UpDownButton>();
    }

    void OnEnable() {
        if (scene != null) {
            if (levelNameTextComponent != null) { levelNameTextComponent.text = scene.LevelName; }
            if (button != null) { button.interactable = !scene.Locked; }
        }
    }

    public void OnClick() {
        if (scene != null && !scene.Locked) {
            SceneManager.LoadScene(scene.SceneName);
        }
    }
}
