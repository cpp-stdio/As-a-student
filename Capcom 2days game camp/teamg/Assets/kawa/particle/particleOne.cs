using UnityEngine;
using System.Collections;

public class particleOne : MonoBehaviour 
{
	public	GameObject	org;
	private GameObject	mono;
	private int			m_time = 100;

	// Use this for initialization
	void Awake() 
	{	
		transform.parent = GameObject.Find("gameMaster(Clone)").transform;

		mono = Instantiate( org );
		mono.transform.parent = transform;
	}

	void Start()
	{
		//mono.GetComponent<ParticleSystem>().startSize = 1.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mono.transform.position = transform.position;

		if( --m_time < 0 )
		{	
			Destroy( mono );
			Destroy( gameObject );
		}
	}
}
