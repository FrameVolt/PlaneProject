using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private GameObject explosionFX;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
    private Vector3 direction;

    private MainAirplane mainPlane;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        maxX = ScreenXY.MaxX;
        minX = ScreenXY.MinX;
        maxY = ScreenXY.MaxY;
        minY = ScreenXY.MinY;
        direction = Vector3.left;
    }


    private void Update()
    {
        if (transform.position.x > maxX)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x < minX)
        {
            direction = Vector3.right;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    }

    public override void Damage(int val, GameObject initiator)
    {
        Health -= val;
        if (Health <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        LevelDirector.Instance.Score += 10;
        Destroy(this.gameObject);
    }
}
