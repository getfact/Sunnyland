using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;

    private Vector3 targetPos;
    private Rigidbody2D targetRigidbody;

    void Start () {

        targetPos = transform.position;
        targetRigidbody = target.GetComponent<Rigidbody2D>();
    }

    void Update () {

        if (PlayerController.facingRight)
            offset.x = 0.2f;
        else
            offset.x = -0.2f;

        if (targetRigidbody.velocity.y > 0.1) {

            offset.y = 0.2f;
        }

        else if (targetRigidbody.velocity.y < -10) {

            offset.y = -0.1f;
        }

        else {

            offset.y = 0.1f;
        }
    }


    void FixedUpdate () {

        if (target) {

            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);

        }
    }
}
