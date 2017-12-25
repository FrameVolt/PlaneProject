using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : EnemyBase
{

    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private float fireRate = 1f;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float rotateSpeed = 1;
    [SerializeField]
    private GameObject explosionFX = null;
    [SerializeField]
    private Transform barrelTrans;
    [SerializeField]
    private Transform fireInitPointTrans;

    private Vector3 direction;
    private Vector3 fireDirection;
    private MainAirplane mainPlane;
    private float fireTimer;

    private void Start()
    {
        //InvokeRepeating("Fire", 0f, repeatRate);
        direction = Vector3.left;
    }


    private void Update()
    {
        Move();

        if (GameManager.Instance.Player == null) return;
        mainPlane = GameManager.Instance.Player;

        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
        BarrelRotate();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed,Space.World);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, fireInitPointTrans.position, barrelTrans.rotation);
    }

    private void BarrelRotate() {
        fireDirection = mainPlane.transform.position - transform.position;
        fireDirection.z = 0;
        fireDirection = fireDirection.normalized;
        Quaternion q = Quaternion.FromToRotation(Vector3.up, fireDirection);
        barrelTrans.rotation = Quaternion.RotateTowards(barrelTrans.rotation, q, Time.deltaTime * rotateSpeed);
        
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
