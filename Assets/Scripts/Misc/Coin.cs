using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioSource coinAudio;
    private Renderer rend;
    private Collider2D coll;

	private void Awake () {
        coinAudio = GetComponent<AudioSource>();
        rend = GetComponentInChildren<Renderer>();
        coll = GetComponent<Collider2D>();
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coll.enabled = false;
            coinAudio.Play();
            rend.enabled = false;
            LevelDirector.Instance.Score += 10;
            Destroy(this.gameObject, coinAudio.clip.length);
        }
    }

}
