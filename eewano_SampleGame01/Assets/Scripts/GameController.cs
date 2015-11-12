using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public PlayerController player;
	public Text scoreLabel;
	public Text GameIsOver;
	public Text TapToTitle;
	StageSoundEffect stagesoundEffect;

	public void Start()
	{
		stagesoundEffect = GameObject.Find("StageSoundController").
			GetComponent<StageSoundEffect>();
		//GameOverの文字を非表示
		GameIsOver.enabled = false;
		TapToTitle.enabled = false;
	}

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
			if (PlayerPrefs.GetInt ("Hiscore") < score) {
				PlayerPrefs.SetInt ("Hiscore", score);
			}

			//0.5秒後にゲームオーバーとする
			Invoke ("GameOver", 0.5f);
		}
	}
	
	int CalcScore()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}

	void GameOver()
	{
		//スタートサウンドを再生する
		stagesoundEffect.GameOver ();
		//ゲームオーバー画面を表示する	
		GameIsOver.enabled = true;
		TapToTitle.enabled = true;

		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("Title");
		}
	}
}