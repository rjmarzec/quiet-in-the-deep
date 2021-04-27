using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }

}
