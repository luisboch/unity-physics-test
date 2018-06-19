using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float rateOfFire = 0;

    private bool readyToShoot;
    private float timeRemainingToShoot;
    private Transform aim;
    private Transform sprite;

    // Use this for initialization
    void Start()
    {
        readyToShoot = true;
        aim = gameObject.transform.Find("aim");
        sprite = gameObject.transform.Find("ship");
    }

    // Update is called once per frame
    void Update()
    {
        timeRemainingToShoot -= Time.deltaTime;
        if(timeRemainingToShoot <= 0)
        {
            readyToShoot = true;
        }

        if (Input.GetAxisRaw("Fire4") > 0 && readyToShoot)
        {
            readyToShoot = false;
            timeRemainingToShoot = rateOfFire;
            
            GameObject bulletPrefab = Resources.Load("Murilo/Bullet", typeof(GameObject)) as GameObject;
            bulletPrefab.transform.position = transform.position;
            Vector3 dir = aim.position - transform.position;

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().SetDirection(dir.normalized);
        }
    }
}
