using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRoll : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Renderer rend;
    private Material mat;
   

	private void Awake () {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }
	
	
	private void Update () {
        mat.SetTextureOffset("_MainTex", new Vector3(0, Time.time * speed));
        

    }
}
