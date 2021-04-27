using UnityEngine;

public class ScaleAlwaysUp : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 scaleDefault = new Vector3(1, 1, 1);
    [SerializeField] private Vector3 scaleInverted = new Vector3(1, -1, 1);

    [SerializeField] private bool useTargetUp;
    [SerializeField] private bool useTargetRight;

    [SerializeField] private bool defaultWhenGreaterThanZero;
    [SerializeField] private bool defaultWhenLessThanZero;

    void Update() {
        if (useTargetUp) {
            if (defaultWhenGreaterThanZero) { transform.localScale = target.up.x > 0 ? scaleDefault : scaleInverted; }
            if (defaultWhenLessThanZero) { transform.localScale = target.up.x > 0 ? scaleDefault : scaleInverted; }
        }

        if (useTargetRight) {
            if (defaultWhenGreaterThanZero) { transform.localScale = target.right.x > 0 ? scaleDefault : scaleInverted; }
            if (defaultWhenLessThanZero) { transform.localScale = target.right.x > 0 ? scaleDefault : scaleInverted; }
        }
    }

}
