using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private float repeatRate = 0f;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private GameObject explosionFX = null;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
    private Vector3 direction;
    private Vector3 rightDirection;
    private Vector3 leftDirection;
   // private MainAirplane mainPlane = null;

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        maxX = ScreenXY.MaxX - 1;
        minX = ScreenXY.MinX + 1;
        maxY = ScreenXY.MaxY;
        minY = ScreenXY.MinY - 1;
        
        leftDirection = (Vector3.left + Vector3.down * 0.1f).normalized;
        rightDirection = (Vector3.right + Vector3.down * 0.1f).normalized;
        direction = leftDirection;
    }


    private void Update()
    {
        if (transform.position.y < minY)
            Destroy(this.gameObject);

        if (transform.position.x > maxX)
        {
            direction = leftDirection;
        }
        else if (transform.position.x < minX)
        {
            direction = rightDirection;
        }

        Move();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void Fire()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(bulletPrefab, this.transform.position, Quaternion.Euler(0, 0, 45 * i));
        }
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
        LevelDirector.Instance.GameWin();
        Destroy(this.gameObject);
    }
}
