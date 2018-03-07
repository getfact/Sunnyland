using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 0;

    private Vector2 dir;

    void Awake () {

        CheckDir();
    }

    void Update () {

        Move();
    }

    void OnBecameInvisible () {

        Destroy(gameObject);
    }

    private void CheckDir () {

        if (PlayerController.facingRight)
            dir = Vector2.right;
        else
            dir = Vector2.left;
    }

    private void Move () {

        transform.Translate(dir * speed * Time.deltaTime);
    }
}
