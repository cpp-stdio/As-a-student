using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
	private const float		m_playerHeight	= 2.0f;	
	private const float		m_moveSpeed		= 0.08f;
	

	public	GameObject		m_orgHP;
	private	GameObject[]	m_hps;
	private const int 		m_maxHP		= 5;
	private int 			m_curHP		= 5;


	private	camera			m_camera;
	public	grovalParam		m_gloval;
	

	private enum State
	{
		Wait,
		Move,
		Just,
	};

		
	
	private State			m_state;
	public int				m_curFloorX;
	public int				m_curFloorY;
	

	private float			m_lerp			= 0.0f;
	private Vector3			m_startPos;


	
	public	GameObject		m_orgCollisionRange;
	private GameObject		m_collisionRange;


	
	private const int		m_prevMax = 6;
	private Vector3[]		m_prevPoses;
	public	GameObject		m_orgAfterImage;
	public	GameObject[]	m_afterImages;
	

	void Awake()
	{
		m_curFloorX			= 0;
		m_curFloorY			= 0;
	}

	// Use this for initialization
	void Start ()
	{
		m_state				= State.Wait;
		

		//
		m_prevPoses			= new Vector3[ m_prevMax ];
		m_afterImages		= new GameObject[ m_prevMax ];

		for( int i=0; i<m_prevMax; ++i )
		{ 
			m_afterImages[i] = Instantiate( m_orgAfterImage );
			m_afterImages[i].transform.parent = transform;
		}


		//
		m_collisionRange = Instantiate( m_orgCollisionRange );
		//m_collisionRange.transform.parent = transform;

		//
		m_camera = GameObject.Find("Main Camera").GetComponent<camera>();


		//
		m_hps = new GameObject[ m_maxHP ];
		for( int i=0; i<m_maxHP; ++i )
		{ 
			m_hps[i]					= Instantiate( m_orgHP ); 
			m_hps[i].transform.parent	= GameObject.Find("Canvas").transform;
			m_hps[i].transform.position = new Vector3( i*30+670, 30, 0 );
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_state )
		{
		case State.Wait:
			UpdateWait();	
			break;

		case State.Move:
			UpdateMove();	
			break;

		case State.Just:	
			UpdateJust();	
			break;
		}

		
		//	コライダ
		m_collisionRange.GetComponent<sphereRange>().SetPos( transform.position );
		//m_collisionRange.SetMove();


		//	残像
		for( int i=m_prevMax-1; i>0; --i ) 
			m_prevPoses[i] = m_prevPoses[i-1];

		m_prevPoses[0] = transform.position;

		
		//	残像位置セット
		for( int i=0; i<m_prevMax; ++i )
			m_afterImages[i].transform.position = m_prevPoses[i];

		//
		ChangeResult();
	}


	//----------------------------------------------------------------------
	//
	//----------------------------------------------------------------------
	void UpdateWait()
	{
		//	補正
		transform.position = new Vector3( m_curFloorX, m_playerHeight, m_curFloorY );
		m_gloval.g_slowFlg	= false;

		//	移動データ
		bool bMove = false;
		Vector2[] moves = new Vector2[4];

		moves[0] = new Vector2( 0, 1 );
		moves[1] = new Vector2( -1, 0 );
		moves[2] = new Vector2( 0, -1 );
		moves[3] = new Vector2( 1, 0 );


		//	キー移動
		if( Input.GetKey( KeyCode.W ) && m_curFloorY < grovalParam.g_height-1 )
		{
			m_curFloorY++;
			bMove = true;
		}
		else if( Input.GetKey( KeyCode.A ) && m_curFloorX > 0 )
		{		
			m_curFloorX--;
			bMove = true;
		}
		else if( Input.GetKey( KeyCode.S ) && m_curFloorY > 0 )
		{		
			m_curFloorY--;
			bMove = true;
		}
		else if( Input.GetKey( KeyCode.D )&& m_curFloorX < grovalParam.g_width-1 )
		{	
			m_curFloorX++;
			bMove = true;
		}


		//	移動した
		if( bMove )
		{	
			SoundManager.Play( "moveSE", 0.5f );

			m_lerp		= 0.0f;
			m_startPos	= transform.position;

			if( m_collisionRange.GetComponent<sphereRange>().m_isCollision )
			{
				m_state	= State.Just;
				m_collisionRange.GetComponent<sphereRange>().m_isCollision = false;
				GameObject.Find("gameMaster(Clone)").GetComponent<gameManager>().AddScore(1000);
			}
			else								
			{
				m_state	= State.Move;
			}
		}
	}
	void UpdateMove()
	{ 
		m_lerp				+= m_moveSpeed;

		Vector3 start		= m_startPos;
		Vector3 end			= new Vector3( m_curFloorX, m_playerHeight, m_curFloorY );
		transform.position	= Vector3.Lerp( m_startPos, end, m_lerp );

		if( m_lerp >= 1.0f )
			m_state	= State.Wait;
	}
	void UpdateJust()
	{
		m_lerp				+= m_moveSpeed * grovalParam.g_slowRate;

		Vector3 start		= m_startPos;
		Vector3 end			= new Vector3( m_curFloorX, m_playerHeight, m_curFloorY );
		transform.position	= Vector3.Lerp( m_startPos, end, m_lerp );

		if( m_lerp >= 1.0f )
		{ 
			m_gloval.g_slowFlg	= false;
			m_state				= State.Wait;
		}
	}


	//----------------------------------------------------------------------
	//
	//----------------------------------------------------------------------
    void OnTriggerEnter( Collider c )
    { 
		if( c.tag != "Enemy" )		return;

		//	破壊演出
		if( m_state == State.Just )
		{			
			SoundManager.Play("justSE");
			GameObject.Find("Effecter").GetComponent<Effecter>().SetP1( transform.position );
			m_gloval.g_slowFlg	= true;
			return;
		}

		m_camera.SetShake( 20, .5f, .5f, .5f );
		SoundManager.Play("damageSE", 0.5f );


		if( m_curHP > 0 )
		{ 
			m_curHP--;
			Destroy( m_hps[ m_curHP ] );
		}

        //Debug.Log("衝突瞬間");
    }
    void OnTriggerExit( Collider c )
    {	
		if( c.tag != "Enemy" )		return;
		if( m_state == State.Just )	return;

        //Debug.Log("衝突乖離");
    }

	void ChangeResult()
	{
		if( m_curHP <= 0 )
			GameObject.Find("framework").GetComponent<framework>().ChangeResult( false );
	}
}
