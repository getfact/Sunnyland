using UnityEngine;

public class Magic : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate;

    private float nextFire;

    void Update () {

        CheckInput();
    }

    private void CheckInput () {

        if (Input.GetButton("Fire1") && Time.time > nextFire) {

            Shoot();
        }
    }

    private void Shoot () {

        nextFire = Time.time + fireRate;
        Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
      }
}
