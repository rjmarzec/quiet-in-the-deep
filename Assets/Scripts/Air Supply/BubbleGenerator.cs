using UnityEngine;

public class BubbleGenerator : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] private float spawnTimer;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float xPositionRange;
    [SerializeField] private float instantiatedLifetime;

    [Header("References")]
    [SerializeField] private GameObject bubblePrefab;

    private float timer;

    void Start() {
        timer = 0f;
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > spawnTimer) {
            timer = 0f;
            GameObject instantiated = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            instantiated.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            instantiated.transform.position = new Vector3(instantiated.transform.position.x + Random.Range(-xPositionRange, xPositionRange), instantiated.transform.position.y, 0);
            Destroy(instantiated, instantiatedLifetime);
        }
    }
}
