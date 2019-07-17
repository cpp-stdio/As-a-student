using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]

public class TitleScript : MonoBehaviour
{
	//
	public		GameObject	m_orgImage;
	private		GameObject	m_image;
	
	//public Transform left,right,top,bottom;
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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetKeyDown( KeyCode.Return ))
		{
			GameObject.Find("framework").GetComponent<framework>().ChangeGame();
		}
	}
	
	void OnGUI()
	{
		//Rect buttonRect = new Rect(
		//	buttonPosition.x,
		//	buttonPosition.y,
		//	buttonPosition.width,
		//	buttonPosition.height );
			
		//if( GUI.Button( buttonRect, "Button1"))
		//{
		//	//	Button1押した時
		//	//GameObject.Find("framework").GetComponent<framework>().ChangeGame();
		//}

		//GUI.TextArea( buttonRect, "タイトル画面");
	}
}