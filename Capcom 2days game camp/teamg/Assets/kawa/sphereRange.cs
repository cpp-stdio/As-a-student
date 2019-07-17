using UnityEngine;
using System.Collections;

public class sphereRange : MonoBehaviour 
{
	public bool m_isCollision = false;

	// Use this for initialization
	void Start () 
	{
		transform.parent = GameObject.Find("gameMaster(Clone)").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	

	//----------------------------------------------------------------------
	//
	//----------------------------------------------------------------------
    public void SetPos( Vector3 pos )
	{
		transform.position = pos;
	}


	//----------------------------------------------------------------------
	//
	//----------------------------------------------------------------------
	void OnTriggerEnter( Collider c )
    { 
		if( c.tag != "Enemy" )	return;
		m_isCollision = true;

		Debug.Log("range vs enemy");
    }
    void OnTriggerExit( Collider c )
    {	
		if( c.tag != "Enemy" )	return;
		m_isCollision = false;

        Debug.Log("range vs enemy乖離");
    }
}
