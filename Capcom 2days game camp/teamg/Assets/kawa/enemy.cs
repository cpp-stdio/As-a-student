using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
	public	GameObject		m_orgSphere;
	public	grovalParam		m_gloval;

	private GameObject[]	m_spheres;
	private bool			m_isStart	= false;
	private float 			m_speed		= 0.0f;
	private const int		m_wayMax	= 4;
	private int				m_eraseTimer = 500;

	// Use this for initialization
	void Awake()
	{	
		m_spheres = new GameObject[ m_wayMax ];

		for( int i=0; i<m_wayMax; ++i )
		{
			m_spheres[i] = Instantiate( m_orgSphere );
			m_spheres[i].transform.parent = transform;
		}

		transform.parent = GameObject.Find("gameMaster(Clone)").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( !m_isStart )return;

		Vector3[]	moves		= new Vector3[ m_wayMax ];
		float		speed		= m_speed;

		if( m_gloval.g_slowFlg )
			speed *= grovalParam.g_slowRate;


		moves[0] = new Vector3( 0,		0,	speed	);
		moves[1] = new Vector3( -speed,	0,	0		);
		moves[2] = new Vector3( 0,		0,	-speed	);
		moves[3] = new Vector3( speed,	0,	0		);

		for( int i=0; i<m_wayMax; ++i )
		{ 
			if( m_spheres[i] == null )continue;
			
			m_spheres[i].transform.position += moves[i];

			if( CheckErase( ref m_spheres[i] ))
			{		
				GameObject.Find("Effecter").GetComponent<Effecter>().SetP1( m_spheres[i].transform.position );
				GameObject.Find("gameMaster(Clone)").GetComponent<gameManager>().AddScore(100);				
				SoundManager.Play("bombSE", 0.5f);

				//transform.position = new Vector3(9999,9999,9999);
				Destroy( m_spheres[i] );
			}
		}


		if( --m_eraseTimer < 0 )
		{
			Destroy( gameObject );
		}
	}

	bool CheckErase( ref GameObject g )
	{
		if( g == null )return false;
			
		if( g.transform.position.x > 9 ) return true;
		if( g.transform.position.x < 0 ) return true;
		if( g.transform.position.z > 9 ) return true;
		if( g.transform.position.z < 0 ) return true;

		return false;
	}

	//
	public void Set( int x, int y, float speed )
	{
		for( int i=0; i<m_wayMax; ++i )
		{ 
			m_spheres[i].transform.position = new Vector3( x, 2.0f, y );
		}

		m_speed		= speed;
		m_isStart	= true;
	}
}
