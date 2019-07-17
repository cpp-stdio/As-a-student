using UnityEngine;
using System.Collections;

public class camara : MonoBehaviour 
{	
	private Vector3	m_startPos;

	private int		m_shakeTimer = 0;
	private Vector3	m_shakePower;

	// Use this for initialization
	void Start ()
	{
		m_startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown( KeyCode.Z ))
			SetShake( 20, 0.3f, 0.3f, 0.3f );

		if( --m_shakeTimer > 0 )
		{
			Vector3 power;
			power.x = m_shakePower.x * m_shakeTimer * ( Random.Range( -1, 2 )); 
			power.y = m_shakePower.y * m_shakeTimer * ( Random.Range( -1, 2 )); 
			power.z = m_shakePower.z * m_shakeTimer * ( Random.Range( -1, 2 ));
		
			transform.position += power;
		}
		else
		{
			m_shakePower		= Vector3.zero;
			m_shakeTimer		= 0;
			transform.position	= m_startPos;
		}
	}

	//
	public void SetShake( int timer, float powerX, float powerY, float powerZ )
	{
		Vector3 power = new Vector3( powerX, powerY, powerZ );

		m_shakePower = power / (float)timer;
		m_shakeTimer = timer;
	}
}
