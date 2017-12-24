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
    [SerializeField]
    private float fireRate = 0.3f;

    private Transform trans;
    private Vector3 vectorSpeed;
    private Rigidbody2D rig;
    protected float horizontalMove;
    protected float verticalMove;
    private float MaxX;
    private float MinX;
    private float MaxY;
    private float MinY;
    private int health = 100;
    private Collider2D coll;
    private float fireTimer;

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
        
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    FireOnce();
        //}
        //if (Input.GetButton("Fire1"))
        //{
        //    FireStart();
        //}
        
        ClampFrame();
    }

    public void FireStart()
    {
        if (health <= 0) return;

        fireTimer += Time.deltaTime;
        if (fireTimer > fireRate)
        {
            Instantiate(bullet, trans.position, Quaternion.identity);
            fireTimer = 0; 
        }
    }
    public virtual void SetHorizontalMove(float value)
    {
        horizontalMove = value;
    }
    public virtual void SetVerticalMove(float value)
    {
        verticalMove = value;
    }
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(horizontalMove, verticalMove, 0);
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
    public void FireOnce()
    {
        if (health <= 0) return;
        Instantiate(bullet, trans.position, Quaternion.identity);

        fireTimer = 0;
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
