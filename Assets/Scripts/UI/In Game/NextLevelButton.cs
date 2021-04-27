using UnityEngine;

public class NextLevelButton : MonoBehaviour {

    public void OnClick() {
        if (LevelManager.instance != null) {
            LevelManager.instance.LoadNextScene();
        }
    }

}
