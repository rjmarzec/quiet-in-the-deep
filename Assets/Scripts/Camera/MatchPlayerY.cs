using UnityEngine;

public class MatchPlayerY : MonoBehaviour {

    void LateUpdate() {
        if (Player.instance != null) {
            transform.position = new Vector3(transform.position.x, Player.instance.transform.position.y, transform.position.z);
        }
    }

}
