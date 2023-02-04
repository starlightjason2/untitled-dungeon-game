using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public AggroZone aggroZone;

    private float timer = 0;

    public void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 2 && aggroZone.detectedObjs.Count > 0)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
