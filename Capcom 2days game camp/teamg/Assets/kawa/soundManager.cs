using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	class SoundData
	{
		public GameObject	obj;
		public AudioSource  src;
		public string		name;
		
		public bool			fadeIn;
		public float		fadeInMaxVolume;

		public bool			fadeOut;
		public float		actionSpeed;
	}	
	static SoundData[] m_sounds;

	const int m_soundMax = 5;
	
	static public void Init () 
	{
		m_sounds = new SoundData[ m_soundMax ];

		for( int i=0; i<m_soundMax; ++i )
			m_sounds[i]= new SoundData();

		m_sounds[0].name = "moveSE";
		m_sounds[1].name = "bombSE";
		m_sounds[2].name = "damageSE";
		m_sounds[3].name = "bgm";
		m_sounds[4].name = "justSE";

		for( int i=0; i<m_soundMax; ++i )
		{
			m_sounds[i].obj			= GameObject.Find( m_sounds[i].name );	
			m_sounds[i].src			= m_sounds[i].obj.GetComponent<AudioSource>();
			m_sounds[i].fadeIn		= false;
			m_sounds[i].fadeOut		= false;
			m_sounds[i].actionSpeed	= 1.0f;
		}
	}

	static public void Release()
	{
		for( int i=0; i<m_soundMax; ++i )
		{
			Destroy( m_sounds[i].obj );
		}
	}

	static public void Update()
	{
		for( int i=0; i<m_soundMax; ++i )
		{
			//	イン
			if( m_sounds[i].fadeIn )
			{
				m_sounds[i].src.volume += m_sounds[i].actionSpeed * Time.deltaTime;

				if( m_sounds[i].src.volume >= m_sounds[i].fadeInMaxVolume ) 
				{
					m_sounds[i].fadeIn	= false;
				}
			}

			//	アウト
			if( m_sounds[i].fadeOut )
			{
				 m_sounds[i].src.volume -= m_sounds[i].actionSpeed * Time.deltaTime;

				if( m_sounds[i].src.volume <= 0.0f ) 
				{
					m_sounds[i].fadeOut		= false;
					m_sounds[i].src.Stop();
				}
			}
		}
	}

	static SoundData Search( string name )
	{
		for( int i=0; i<m_soundMax; ++i )
		{
			if( m_sounds[i].name == name )
			{
				return m_sounds[i];
			}
		}

		return null;
	}




	//****************************************************************************************
	//
	//****************************************************************************************
	static public void Play ( string name, float vol = 1.0f )
	{
		SoundData data = Search(name);

		if( data != null )
		{ 
			data.src.volume = vol;
			data.src.Play();
		}
	}
    static  public  void    PlayOne( string _Name, float _Volume )
    {
        SoundData   data    =   Search( _Name );

		if( data != null ){ 
			data.src.volume =   _Volume;
            data.src.PlayOneShot( data.src.clip );
		}
    }
    static  public  void    PlayNV( string _Name, bool _Solo, float _Pitch = 0.0f )
    {
        SoundData   data    =   Search( _Name );

		if( data != null ){
            if( _Pitch > 0.0f ) data.src.pitch  =   _Pitch;

            if( _Solo ) data.src.Play();
            else        data.src.PlayOneShot( data.src.clip );
		}
    }
	static public void Stop ( string name )
	{
		SoundData data = Search(name);

		if( data != null )
			data.src.Stop();
	}
	static public void FadeIn ( string name, float speed = 1.0f, float vol = 1.0f )
	{
		SoundData data = Search(name);

		if( data != null )
		{ 
			data.actionSpeed		= speed;
			data.fadeIn				= true;	
			data.fadeInMaxVolume	= vol;
			data.fadeOut			= false;
			data.src.volume			= 0.0f;
			data.src.Play();
		}
	}
	static public void FadeOut ( string name, float speed = 1.0f )
	{
		SoundData data = Search(name);

		if( data != null )
		{ 
			data.actionSpeed	= speed;
			data.fadeIn			= false;
			data.fadeOut		= true;
		}
	}
}