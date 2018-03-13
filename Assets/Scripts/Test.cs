using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private void Awake()
    {
        print("Awake");
        StartCoroutine(Move());
    }
	void Start () {
        print("start");
	}
	
	// Update is called once per frame
	void Update () {
        print("Update");
    }

    void LateUpdate()
    {
        print("LateUpdate");
    }
    void FixedUpdate()
    {
        print("FixedUpdate");
    }

    void OnTriggerEnter(Collider coll)
    {
        print("OnTriggerEnter");
    }
    private void OnDestroy()
    {
        print("OnDestroy");
    }
    private void OnEnable()
    {
        print("OnEnable");
    }

    private void OnDisable()
    {
        print("OnDisable");
    }

    private IEnumerator Move() {
        while (true) {
            print("Move");
            yield return new WaitForSeconds(1);
        }
    }
}
