using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public PlayerController player;
	public Text scoreLabel;

	public void Update()
	{
		//スコアラベルを更新する
		int score = CalcScore ();
		scoreLabel.text = "Score : " + score + "pts";
	}

	int CalcScore()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}
}
