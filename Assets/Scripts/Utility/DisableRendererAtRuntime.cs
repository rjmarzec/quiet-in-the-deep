using UnityEngine;

public class DisableRendererAtRuntime : MonoBehaviour {

    void Start() {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null) { renderer.enabled = false; }
    }

}
