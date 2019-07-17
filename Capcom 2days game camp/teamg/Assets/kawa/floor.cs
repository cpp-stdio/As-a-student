using UnityEngine;
using System.Collections;

public class floor : MonoBehaviour 
{	
	public	GameObject		m_orgEnemy;
	private GameObject		m_enemy;

	Color		start;
	Color		end;
	float		lerp	= 0.0f;
	bool		up		= true;
	int			timer	= 0;
	float		speed	= 0;
	public int	idX		= 0;
	public int	idY		= 0;


	enum State
	{
		Wait,
		Light,
		Spawn,
		Intarval,
	};
	State m_state = State.Wait;


	// Use this for initialization
	void Awake() 
	{
		start	= new Color( 0, 0.5f, 1.0f );
		end		= Color.red;
		m_state = State.Wait;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch( m_state )
		{
			case State.Wait:		UpdateWait();		break;
			case State.Light:		UpdateLight();		break;
			case State.Spawn:		UpdateSpawn();		break;		
			case State.Intarval:	UpdateIntarval();	break;		
		}
	}


	//
	//
	//
	void UpdateWait()
	{
		GetComponent<Renderer>().material.color = start;
	}
	void UpdateLight()
	{
		if( up )	lerp += 0.05f;
		else		lerp -= 0.05f;

		if( lerp > 1.0f || lerp < 0.0f )
			up = !up;

		GetComponent<Renderer>().material.color = Color.Lerp( start, end, lerp );

		if( --timer < 0 )
			m_state = State.Spawn;
	}
	void UpdateSpawn()
	{
		m_enemy = Instantiate( m_orgEnemy );
		m_enemy.GetComponent<enemy>().Set( idX, idY, speed );

		m_state = State.Intarval;
		timer	= 0;
	}
	void UpdateIntarval()
	{
		if( --timer < 0 )
		{ 
			//Destroy( m_enemy );
			m_state = State.Wait;
		}
	}


	//
	//
	//
	public void Set( int time, float _speed )
	{
		if( m_state != State.Wait )
			return;

		speed	= _speed;
		timer	= time;
		lerp	= 0.0f;
		m_state = State.Light;
	}
}
