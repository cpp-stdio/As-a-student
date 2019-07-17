using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class result : MonoBehaviour 
{
	//
	public		GameObject	m_orgImage;
	public		GameObject	m_orgCan;

	private		GameObject	m_image;
	private		GameObject	m_canvas;

	//
	public bool				m_isClear	= false;
	public int				m_score		= 0;


	//	初めのボタンの位置、大きさ
	public int x = 10, y = 10;
	public int width = 200,height = 50;
	//	隙間
	public int betweenMagnify = 100;
	
	struct stBUTTON
	{
		public int x,y;
		public int width,height;
	};
	stBUTTON buttonPosition;
	
	// initialization
	void Awake () 
	{
		buttonPosition.x		= x;
		buttonPosition.y		= y;
		buttonPosition.width	= width;
		buttonPosition.height	= height;
	}

	void Start()
	{
		m_image						= Instantiate( m_orgImage );
		m_image.transform.parent	= GameObject.Find("Canvas").transform;
		m_image.transform.position	= new Vector3( 0, 0, 0 );

		m_canvas					= Instantiate( m_orgCan );
		m_canvas.transform.parent	= transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown( KeyCode.Return ))
		{
			GameObject.Find("framework").GetComponent<framework>().ChangeTitle();
		}

		//
		GameObject clear = m_canvas.transform.FindChild("ReslutIsClear").gameObject;
		clear.GetComponent<Text>().text = "Clear:";
		clear.GetComponent<Text>().text += m_isClear? "success":"failure";

		//
		GameObject score = m_canvas.transform.FindChild("ResultScore").gameObject;
		score.GetComponent<Text>().text = "Score:";
		score.GetComponent<Text>().text += m_score.ToString();

		//
		GameObject res = m_canvas.transform.FindChild("ResultText").gameObject;
		res.GetComponent<Text>().text = "Result:";

		int rate		= m_isClear? 1:0;
		int trueScore	= m_score * rate;
		res.GetComponent<Text>().text += trueScore.ToString();
	}
	
	void OnGUI()
	{
		//Rect buttonRect = new Rect(
		//	buttonPosition.x,
		//	buttonPosition.y,
		//	buttonPosition.width,
		//	buttonPosition.height );
			
		////if( GUI.Button( buttonRect, "Button1"))
		////{
		////	//	Button1押した時
		////	//GameObject.Find("framework").GetComponent<framework>().ChangeGame();
		////}

		//GUI.TextField( buttonRect, "res");
	}
}
