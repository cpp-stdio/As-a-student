using UnityEngine;
using System.Collections;

public class Effecter : MonoBehaviour {

	public GameObject m_orgp1;
	public GameObject m_orgp2;


	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void SetP1( Vector3 pos )
	{
		GameObject g = Instantiate( m_orgp1 );
		g.transform.position = pos;
	}
	public void SetP2( Vector3 pos )
	{
		GameObject g = Instantiate( m_orgp1 );
		g.transform.position = pos;
	}
}
