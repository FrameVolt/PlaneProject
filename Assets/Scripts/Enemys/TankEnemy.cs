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

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private Vector3 direction;

    private MainAirplane mainPlane;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        MaxX = ScreenXY.MaxX;
        MinX = ScreenXY.MinX;
        MaxY = ScreenXY.MaxY;
        MinY = ScreenXY.MinY;
        direction = Vector3.left;
    }


    private void Update()
    {
        if (transform.position.x > MaxX)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x < MinX)
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
