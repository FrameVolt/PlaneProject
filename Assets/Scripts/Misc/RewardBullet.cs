using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBullet : MonoBehaviour {
    private AudioSource coinAudio;
    private SpriteRenderer rend;
    private Collider2D coll;

    private void Awake()
    {
        coinAudio = GetComponent<AudioSource>();
        rend = GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coll.enabled = false;
            coinAudio.Play();
            rend.enabled = false;
            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            weapon.ChangeWeaponType((int)weapon.CurrentWeaponType + 1);
            Destroy(this.gameObject, coinAudio.clip.length);
        }
    }

}