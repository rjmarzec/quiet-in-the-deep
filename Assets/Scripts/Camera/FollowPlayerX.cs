using UnityEngine;

public class FollowPlayerX : MonoBehaviour {

    [SerializeField] private float miniumumX;
    [SerializeField] private float maximumX;

    void LateUpdate() {
        if (Player.instance != null) {
            float playerX = Player.instance.transform.position.x;
            float cameraX = Mathf.Clamp(playerX, miniumumX, maximumX);
            transform.position = new Vector3(cameraX, transform.position.y, transform.position.z);
        }
    }

}
