using UnityEngine;
using System.Collections;

public class framework : MonoBehaviour 
{
	public	GameObject	m_title;	
	public	GameObject	m_game;
	public	GameObject	m_result;
	public	GameObject	m_emptyCanvas;

	private GameObject	m_cur;
	private GameObject	m_canvas;


	// Use this for initialization
	void Awake () 
	{
		SoundManager.Init();
		m_cur = Instantiate( m_title );
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void ChangeGame()
	{
		Destroy( m_cur );
		m_cur = Instantiate( m_game );

		//	ごり押しですすいません
		Destroy( GameObject.Find("titleimage(Clone)"));
	}
	public void ChangeTitle()
	{
		Destroy( m_cur );
		m_cur = Instantiate( m_title );

		//	ごり押しですすいません
		Destroy( GameObject.Find("resultImage(Clone)"));
	}
	public void ChangeResult( bool suc )
	{
		SoundManager.Stop("bgm");

		GameObject	scoreText = GameObject.Find("ScoreText(Clone)");
		int			resScore = scoreText.GetComponent<score>().SCORE;

		Destroy( m_cur );
		m_cur = Instantiate( m_result );
		m_cur.GetComponent<result>().m_isClear	= suc;
		m_cur.GetComponent<result>().m_score	= resScore;

		//	ごり押しですすいません
		Destroy( scoreText );
	}
}
