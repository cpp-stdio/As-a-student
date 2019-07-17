using UnityEngine;
using System.Collections;

public class enemySphere : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider c )
    { 
		if( c.tag != "Player" )		
			return;
		
		//	もう間に合わないー
		//transform.position = new Vector3(9999,9999,9999);
		Destroy( gameObject );
		//
		//c.gameObject.GetComponent<>
    }
}
