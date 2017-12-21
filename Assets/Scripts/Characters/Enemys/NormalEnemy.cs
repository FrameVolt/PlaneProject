using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : EnemyBase
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float repeatRate;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private GameObject explosionFX;

    private float MinY;
    private Vector3 direction;
    
    private MainAirplane mainPlane;

    private void Start () {
        InvokeRepeating("Fire", 0f, repeatRate);
        MinY = ScreenXY.MinY - 1;
        direction = Vector3.down;
    }
	
	
	private void Update () {
        if (transform.position.y < MinY)
            Destroy(this.gameObject);

        Move();
    }

    private void Move()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void Fire() {
        GameObject bulletOBJ = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bulletOBJ.transform.rotation = Quaternion.Euler(0,0,180);
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
