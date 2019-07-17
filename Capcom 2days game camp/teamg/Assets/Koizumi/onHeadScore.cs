using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onHeadScore : MonoBehaviour
{

    public Text scoreText; //Text用変数
    //public GameObject Player;

    private int SCORE = 0; //スコア計算用変数
    private int Timer = 0;
    private bool Flag = false;

    void Start ()
    {
        Timer = 0;
        SCORE = 0;
        Flag = false;
        scoreText.text = "";
        transform.localPosition = new Vector3(-999999, -999999, 0);
    }

    void Update ()
    {
        //Debug.Log("X = " + Player.transform.position.x.ToString("F2"));
        //Debug.Log("Y = " + Player.transform.position.y.ToString("F2"));
       // Debug.Log("Z = " + Player.transform.position.z.ToString("F2"));

        Debug.Log("X = " + transform.localPosition.x.ToString("F2"));
        Debug.Log("Y = " + transform.localPosition.y.ToString("F2"));

        Timer--;
        if (Timer <= 0) { Timer = 0; }
        else { return; }

        Vector3 Pos = new Vector3(-999999, -999999, 0);
        if (true){//条件 : 当たったらの処理
            Timer = 30;
            Pos.x = UnityEngine.Random.Range(-256, 256);//あとで直す
            Pos.y = UnityEngine.Random.Range(-256, 256);//あとで直す
            SCORE = UnityEngine.Random.Range(0, 256);
            Flag = true;
        }else { return; }

        
        scoreText.text = SCORE.ToString();
        transform.localPosition = Pos;
    }
}