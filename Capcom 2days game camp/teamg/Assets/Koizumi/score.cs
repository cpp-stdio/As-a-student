using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoreText; //Text用変数
    public int SCORE = 0; //スコア計算用変数
    private int Timer = 0;

	void Start () {
        SCORE = 0;
        Timer = 0;
        scoreText.text = "SCORE: 0"; //初期スコアを代入して画面に表示
	}
	
	void Update () {
		//Timer++;
		//if (Timer % 60 == 0){
		//	SCORE += 12;
		//}

        scoreText.text = "SCORE: " + SCORE.ToString().PadLeft(8, '0');
	}

	public void AddScore( int add )
	{
		SCORE += add;
	}
}