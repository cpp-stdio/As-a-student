using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour 
{	
	public GameObject		m_orgFloor;
	public GameObject		m_orgPlayer;
	public GameObject		m_orgScore;

	
	private GameObject[,]	m_floors; 
	private	GameObject		m_player;
	private	GameObject		m_score;

	private const int		m_spawnMin	= 0;
	private const int		m_spawnMax	= 9;

	private int				m_counter	= 0;
	
	private const int		m_maxEnemy = 300;


	struct SpawnInfo
	{
		public bool enable;
		public int x;
		public int y;
		public int time;
		public float speed;

		public SpawnInfo( int _x, int _y, int _time, float s )
		{
			enable	= true;
			x		= _x;
			y		= _y;
			time	= _time;
			speed	= s;
		}
	}
	SpawnInfo[] m_spawnInfo;


	// Use this for initialization
	void Awake () 
	{		
		//	stage
		m_floors = new GameObject[ grovalParam.g_width, grovalParam.g_height ];

		for( int w=0; w<grovalParam.g_width; ++w )
		{
			for( int h=0; h<grovalParam.g_height; ++h )
			{
				m_floors[w,h] = Instantiate( m_orgFloor );
				m_floors[w,h].transform.position = new Vector3( w, 0, h );
				m_floors[w,h].GetComponent<floor>().idX = w;	
				m_floors[w,h].GetComponent<floor>().idY = h;
			}
		}

		//	player
		m_player		= Instantiate( m_orgPlayer ); 

		//
		m_spawnInfo	= new SpawnInfo[ m_maxEnemy ];


		// 出現データの設定等
		InitializeSpawnInfo();

		//
		m_score						= Instantiate( m_orgScore );
		m_score.transform.parent	= GameObject.Find("Canvas").transform;
		m_score.transform.position	= new Vector3( 730, 170, 0 );

		//
		SoundManager.Play("bgm");
	}
	void Start()
	{
		for( int w=0; w<grovalParam.g_width; ++w )
		{
			for( int h=0; h<grovalParam.g_height; ++h )
			{
				m_floors[w,h].transform.parent = transform;
			}
		}

		m_player.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		const int interval = 100;

		for( int i=0; i<m_maxEnemy; ++i )
		{
			if( !m_spawnInfo[i].enable )			continue;
			if( m_spawnInfo[i].time > m_counter )	continue;
		
			SetEnemy( m_spawnInfo[i].x, m_spawnInfo[i].y, interval, m_spawnInfo[i].speed );
			m_spawnInfo[i].enable = false;
		}

		m_counter++;


		//
		ChangeResult();
	}

	//
	//
	//
	void SetEnemy( int x, int y, int lightInterval, float speed )
	{
		if( x >= grovalParam.g_width )	return;
		if( y >= grovalParam.g_height )	return;

		m_floors[ x, y ].GetComponent<floor>().Set( lightInterval, speed );
	}


	//
	//
	//
	public void AddScore( int score )
	{
		m_score.GetComponent<score>().AddScore( score );
	}


	//
	void ChangeResult()
	{
		if( m_spawnInfo[ m_maxEnemy-1 ].time+300 < m_counter )
			GameObject.Find("framework").GetComponent<framework>().ChangeResult( true );
	}

	//
	//
	void InitializeSpawnInfo()
	{
		//m_counter = 7000;
		m_spawnInfo[0] = new SpawnInfo( 1, 5, 100, 0.05f );
		m_spawnInfo[1] = new SpawnInfo( 7, 7, 400, 0.05f );
		
		/* 0.05:300,0.01f:600 */
		
		m_spawnInfo[2] = new SpawnInfo( 1, 1, 700, 0.05f );
		//	中心
		m_spawnInfo[3] = new SpawnInfo( 4, 4, 1000, 0.05f );
		//	ちょっと連続
		m_spawnInfo[4] = new SpawnInfo( 3, 6, 1300, 0.05f );
		m_spawnInfo[5] = new SpawnInfo( 8, 3, 1500, 0.05f );
		m_spawnInfo[6] = new SpawnInfo( 7, 1, 1700, 0.05f );
		
		m_spawnInfo[7] = new SpawnInfo( 2, 3, 2000, 0.05f );
		m_spawnInfo[8] = new SpawnInfo( 7, 5, 2000, 0.05f );
		m_spawnInfo[9] = new SpawnInfo( 1, 7, 2300, 0.05f );
		m_spawnInfo[10] = new SpawnInfo( 6, 0, 2300, 0.05f );
		m_spawnInfo[11] = new SpawnInfo( 7, 3, 2600, 0.05f );
		m_spawnInfo[12] = new SpawnInfo( 3, 6, 2600, 0.05f );
		m_spawnInfo[13] = new SpawnInfo( 3, 6, 2900, 0.05f );
		m_spawnInfo[14] = new SpawnInfo( 7, 4, 2900, 0.05f );
		//	3つ連続
		m_spawnInfo[15] = new SpawnInfo( 0, 1, 3200, 0.05f );
		m_spawnInfo[16] = new SpawnInfo( 8, 4, 3200, 0.05f );
		m_spawnInfo[17] = new SpawnInfo( 0, 7, 3200, 0.05f );
		m_spawnInfo[18] = new SpawnInfo( 1, 3, 3500, 0.05f );
		m_spawnInfo[19] = new SpawnInfo( 4, 3, 3500, 0.05f );
		m_spawnInfo[20] = new SpawnInfo( 7, 3, 3500, 0.05f );
		m_spawnInfo[21] = new SpawnInfo( 1, 1, 3800, 0.05f );
		m_spawnInfo[22] = new SpawnInfo( 4, 4, 3800, 0.05f );
		m_spawnInfo[23] = new SpawnInfo( 7, 7, 3800, 0.05f );
		m_spawnInfo[24] = new SpawnInfo( 1, 6, 4100, 0.05f );
		m_spawnInfo[25] = new SpawnInfo( 4, 6, 4100, 0.05f );
		m_spawnInfo[26] = new SpawnInfo( 7, 6, 4100, 0.05f );
		//	4つ◇
		m_spawnInfo[27] = new SpawnInfo( 1, 4, 4400, 0.05f );
		m_spawnInfo[28] = new SpawnInfo( 4, 7, 4400, 0.05f );
		m_spawnInfo[29] = new SpawnInfo( 7, 4, 4400, 0.05f );
		m_spawnInfo[30] = new SpawnInfo( 4, 1, 4400, 0.05f );
		//	ずらして出る
		m_spawnInfo[31] = new SpawnInfo( 2, 5, 4700, 0.05f );
		m_spawnInfo[32] = new SpawnInfo( 5, 3, 4800, 0.05f );
		m_spawnInfo[33] = new SpawnInfo( 2, 3, 4900, 0.05f );
		m_spawnInfo[34] = new SpawnInfo( 7, 6, 5000, 0.05f );
		m_spawnInfo[35] = new SpawnInfo( 5, 5, 5200, 0.05f );
		m_spawnInfo[36] = new SpawnInfo( 2, 4, 5400, 0.05f );
		m_spawnInfo[37] = new SpawnInfo( 4, 3, 5600, 0.05f );
		m_spawnInfo[38] = new SpawnInfo( 2, 4, 5800, 0.05f );
		m_spawnInfo[39] = new SpawnInfo( 7, 3, 6000, 0.05f );
		
		m_spawnInfo[40] = new SpawnInfo( 5, 4, 6100, 0.05f );
		m_spawnInfo[41] = new SpawnInfo( 2, 5, 6200, 0.05f );
		m_spawnInfo[42] = new SpawnInfo( 5, 5, 6300, 0.05f );
		m_spawnInfo[43] = new SpawnInfo( 6, 2, 6400, 0.05f );
		m_spawnInfo[44] = new SpawnInfo( 2, 2, 6500, 0.05f );
		// 右上
		m_spawnInfo[45] = new SpawnInfo( 7, 7, 6600, 0.05f );
		
		m_spawnInfo[46] = new SpawnInfo( 4, 6, 6620, 0.06f );
		//m_spawnInfo[47] = new SpawnInfo( 2, 5, 6640, 0.06f );
		m_spawnInfo[48] = new SpawnInfo( 6, 5, 6660, 0.06f );
		// 左上
		m_spawnInfo[49] = new SpawnInfo( 1, 7, 6700, 0.05f );
		
		m_spawnInfo[50] = new SpawnInfo( 2, 3, 6720, 0.06f );
		//m_spawnInfo[51] = new SpawnInfo( 4, 2, 6740, 0.06f );
		m_spawnInfo[52] = new SpawnInfo( 6, 3, 6760, 0.06f );
		// 左下
		m_spawnInfo[53] = new SpawnInfo( 1, 1, 6800, 0.05f );
		
		m_spawnInfo[54] = new SpawnInfo( 2, 6, 6820, 0.06f );
		//m_spawnInfo[55] = new SpawnInfo( 1, 4, 6840, 0.06f );
		m_spawnInfo[56] = new SpawnInfo( 2, 2, 6860, 0.06f );
		// 右下
		m_spawnInfo[57] = new SpawnInfo( 7, 1, 6900, 0.05f );
		
		m_spawnInfo[58] = new SpawnInfo( 6, 6, 6920, 0.06f );
		//m_spawnInfo[59] = new SpawnInfo( 7, 4, 6940, 0.06f );
		m_spawnInfo[60] = new SpawnInfo( 6, 2, 6960, 0.06f );
		
		
		m_spawnInfo[61] = new SpawnInfo( 4, 4, 7000, 0.07f );
		
		// 2つ同時
		m_spawnInfo[62] = new SpawnInfo( 1, 6, 7200, 0.07f );
		m_spawnInfo[63] = new SpawnInfo( 5, 4, 7200, 0.07f );
		m_spawnInfo[64] = new SpawnInfo( 3, 3, 7400, 0.07f );
		m_spawnInfo[65] = new SpawnInfo( 4, 5, 7400, 0.07f );
		m_spawnInfo[66] = new SpawnInfo( 4, 5, 7600, 0.07f );
		m_spawnInfo[67] = new SpawnInfo( 6, 3, 7600, 0.07f );
		m_spawnInfo[68] = new SpawnInfo( 6, 5, 7800, 0.07f );
		m_spawnInfo[69] = new SpawnInfo( 2, 5, 7800, 0.07f );
		m_spawnInfo[70] = new SpawnInfo( 3, 2, 8000, 0.07f );
		m_spawnInfo[71] = new SpawnInfo( 5, 5, 8000, 0.07f );
		// 1->2->3->4->5つ同時
		m_spawnInfo[72] = new SpawnInfo( 3, 2, 8200, 0.07f );
		m_spawnInfo[73] = new SpawnInfo( 3, 2, 8400, 0.07f );
		m_spawnInfo[74] = new SpawnInfo( 3, 6, 8400, 0.07f );
		m_spawnInfo[75] = new SpawnInfo( 5, 4, 8600, 0.07f );
		m_spawnInfo[76] = new SpawnInfo( 7, 3, 8600, 0.07f );
		m_spawnInfo[77] = new SpawnInfo( 3, 6, 8600, 0.07f );
		m_spawnInfo[78] = new SpawnInfo( 1, 2, 8800, 0.07f );
		m_spawnInfo[79] = new SpawnInfo( 7, 3, 8800, 0.07f );
		m_spawnInfo[80] = new SpawnInfo( 0, 7, 8800, 0.07f );
		m_spawnInfo[81] = new SpawnInfo( 5, 8, 8800, 0.07f );
		m_spawnInfo[82] = new SpawnInfo( 4, 2, 9000, 0.07f );
		m_spawnInfo[83] = new SpawnInfo( 7, 1, 9000, 0.07f );
		m_spawnInfo[84] = new SpawnInfo( 2, 5, 9000, 0.07f );
		m_spawnInfo[85] = new SpawnInfo( 8, 8, 9000, 0.07f );
		m_spawnInfo[86] = new SpawnInfo( 0, 0, 9000, 0.07f );
		// スピード低速地帯
		// ピラミッド
		m_spawnInfo[87] = new SpawnInfo( 4, 5, 9400, 0.01f );
		m_spawnInfo[88] = new SpawnInfo( 3, 4, 9400, 0.01f );
		m_spawnInfo[89] = new SpawnInfo( 4, 4, 9400, 0.01f );
		m_spawnInfo[90] = new SpawnInfo( 5, 4, 9400, 0.01f );
		m_spawnInfo[91] = new SpawnInfo( 2, 3, 9400, 0.01f );
		m_spawnInfo[92] = new SpawnInfo( 3, 3, 9400, 0.01f );
		m_spawnInfo[93] = new SpawnInfo( 4, 3, 9400, 0.01f );
		m_spawnInfo[94] = new SpawnInfo( 5, 3, 9400, 0.01f );
		m_spawnInfo[95] = new SpawnInfo( 6, 3, 9400, 0.01f );
		
		m_spawnInfo[96] = new SpawnInfo( 3, 8, 10000, 0.1f );
		m_spawnInfo[97] = new SpawnInfo( 4, 8, 10000, 0.1f );
		m_spawnInfo[98] = new SpawnInfo( 5, 8, 10000, 0.1f );
		m_spawnInfo[99] = new SpawnInfo( 6, 8, 10000, 0.1f );
		//m_spawnInfo[100]= new SpawnInfo( 7, 8, 10000, 1.01f );


		for( int i=100; i<200; ++i )
		{
			m_spawnInfo[i] = m_spawnInfo[i-100];
			m_spawnInfo[i].x =  Random.Range( 0, 9 );	
			m_spawnInfo[i].y =  Random.Range( 0, 9 );
		}
		for( int i=200; i<300; ++i )
		{
			m_spawnInfo[i] = m_spawnInfo[i-200];
			m_spawnInfo[i].x =  Random.Range( 0, 9 );	
			m_spawnInfo[i].y =  Random.Range( 0, 9 );
		}
	}
}
