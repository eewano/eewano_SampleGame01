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

		//プレイヤーのライフが0になったらゲームオーバー
		if (player.Life () <= 0) {
			//これ以上のUpdateは止める
			enabled = false;

			//ハイスコアを更新する
			if(PlayerPrefs.GetInt ("Hiscore") < score)
			{
				PlayerPrefs.SetInt ("Hiscore", score);
			}

			// 4秒後にReturnToTitleを呼び出す
			Invoke ("ReturnToTitle", 4.0f);
		}
	}

	int CalcScore()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}

	void ReturnToTitle()
	{
		//タイトルシーンに切り替える
		Application.LoadLevel ("Title");
	}
}
