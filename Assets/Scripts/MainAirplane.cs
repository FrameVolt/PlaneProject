using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAirplane : MonoBehaviour, IHealth
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject explosionFX;

    private Transform trans;
    private Vector3 vectorSpeed;
    private Rigidbody2D rig;

    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private int health = 100;
    private Collider2D coll;

    public delegate void OnDead();
    public event OnDead OnDeadEvent;

    
    public int Health{
        get { return health; }
        private set { health = value; }
    }

    private void Awake()
    {
        trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector3.up;
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        StartCoroutine(DelayColl());
    }
    private void Start() {
        MaxX = ScreenXY.MaxX;
        MinX = ScreenXY.MinX;
        MaxY = ScreenXY.MaxY;
        MinY = ScreenXY.MinY;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        ClampFrame();
    }
    

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Move(direction);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag(gameObject.tag))
        {
            Damage(100, this.gameObject);
        }

    }
    private IEnumerator DelayColl() {
        yield return new WaitForSeconds(2);
        coll.enabled = true;
    }
    private void ClampFrame()
    {
        trans.position = new Vector3(Mathf.Clamp(trans.position.x, MinX, MaxX),
                                     Mathf.Clamp(trans.position.y, MinY, MaxY),
                                     trans.position.z);
    }
    private void Move(Vector3 direction)
    {
        rig.velocity = direction * speed;
    }
    private void Fire()
    {
        Instantiate(bullet, trans.position, Quaternion.identity);
    }

   
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    print("Plane OnTriggerEnter2D");
    //}

    public void Damage(int val, GameObject initiator)
    {
        Health -= val;
        if (Health <= 0) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        Instantiate(explosionFX, trans.position, Quaternion.identity);
        if(OnDeadEvent != null)
        OnDeadEvent();
        Destroy(this.gameObject);
    }
}
