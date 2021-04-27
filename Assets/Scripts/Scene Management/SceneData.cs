using UnityEngine;

[CreateAssetMenu(menuName = "Scene Data")]
public class SceneData : ScriptableObject {

    [SerializeField] private string sceneName;
    public string SceneName {
        get {
            return sceneName;
        }
    }

    [SerializeField] private string levelName;
    public string LevelName {
        get {
            return levelName;
        }
    }

    [SerializeField] private bool lockOverride;
    public bool Locked {
        get {
            return PlayerPrefs.GetInt(SceneName, 0) == 0 && !lockOverride;
        }
        set {

            // Lock
            if (value) {
                PlayerPrefs.SetInt(SceneName, 0);
            }

            // Unlock
            else {
                PlayerPrefs.SetInt(SceneName, 1);
            }

        }
    }
}
