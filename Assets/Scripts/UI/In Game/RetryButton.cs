using UnityEngine;

public class RetryButton : MonoBehaviour {

    public void OnClick() {
        if (LevelManager.instance != null) {
            LevelManager.instance.ReloadThisScene();
        }
    }

}
