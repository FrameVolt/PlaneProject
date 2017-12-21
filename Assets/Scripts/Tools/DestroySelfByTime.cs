using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfByTime : MonoBehaviour {

    [SerializeField]
    private float time = 1f;

	private void Start () {
        Destroy(this.gameObject,time);
	}
	
}
