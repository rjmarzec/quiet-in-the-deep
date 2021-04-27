using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    public event Action<int, int> OnObjectiveCollected;
    public event Action OnLevelStarted;
    public event Action OnLevelCompleted;
    public event Action<Player.DeathMethod> OnLevelFailed;

    [Header("Scenes")]
    [SerializeField] private SceneData thisScene;
    [SerializeField] private SceneData sceneUnlockedOnComplete;
    [SerializeField] private SceneData sceneOpenedOnComplete;

    public SceneData ThisScene {
        get { return thisScene; }
    }

    [Space]

    [SerializeField] private List<LevelObjective> objectives;
    public List<LevelObjective> Objectives {
        get {
            if (objectives == null) { objectives = new List<LevelObjective>(); }
            return objectives;
        }
    }

    [Header("States")]
    [SerializeField] private bool levelStarted;
    public bool LevelStarted {
        get { return levelStarted; }
        private set { levelStarted = value; }
    }

    [SerializeField] private bool levelFailed;
    public bool LevelFailed {
        get { return levelFailed; }
        private set { levelFailed = value; }
    }

    [SerializeField] private bool levelCompleted;
    public bool LevelCompleted {
        get { return levelCompleted; }
        private set { levelCompleted = value; }
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else instance = this;

        Time.timeScale = 1;
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    void Start() {
        for (int i = 0; i < Objectives.Count; i++) {
            Objectives[i].OnObjectiveCollected += ObjectiveCollected;
        }
        if (Player.instance != null) {
            Player.instance.OnPlayerDied += OnPlayerDied;
        }
    }

    private void OnDestroy() {
        for (int i = 0; i < Objectives.Count; i++) {
            Objectives[i].OnObjectiveCollected -= ObjectiveCollected;
        }
        if (Player.instance != null) {
            Player.instance.OnPlayerDied -= OnPlayerDied;
        }
    }

    public void StartLevel() {
        if (!LevelStarted) {
            LevelStarted = true;
            OnLevelStarted?.Invoke();
        }
    }

    public int CountCollectedObjectives() {
        int numCollected = 0;
        for (int i = 0; i < Objectives.Count; i++) {
            if (Objectives[i].Collected) {
                numCollected++;
            }
        }
        return numCollected;
    }

    private void ObjectiveCollected() {
        int numCollected = CountCollectedObjectives();
        OnObjectiveCollected?.Invoke(numCollected, Objectives.Count);
        if (numCollected == Objectives.Count) { CompleteLevel(); }
    }

    private void OnPlayerDied(Player.DeathMethod deathMethod) {
        if (!LevelFailed && !LevelCompleted) {
            LevelFailed = true;
            OnLevelFailed?.Invoke(deathMethod);
        }
    }

    private void CompleteLevel() {
        if (!LevelFailed && !LevelCompleted) {
            LevelCompleted = true;
            OnLevelCompleted?.Invoke();

            // Unlock scene
            if (sceneUnlockedOnComplete != null) {
                sceneUnlockedOnComplete.Locked = false;
            }
        }
    }

    public void ReloadThisScene() {
        // Reloads this scene
        if (thisScene != null) {
            SceneManager.LoadScene(thisScene.SceneName);
        }

        // Load default scene
        else {
            SceneManager.LoadScene("Level Select");
        }
    }

    public void LoadNextScene() {
        // Load next scene
        if (sceneOpenedOnComplete != null) {
            SceneManager.LoadScene(sceneOpenedOnComplete.SceneName);
        }

        // Load default scene
        else {
            SceneManager.LoadScene("Level Select");
        }
    }
}
