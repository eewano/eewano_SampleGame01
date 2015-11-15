using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	enum State
	{
		PLAY,
		GAMEOVER
	}

	State state;

	public PlayerController player;
	public Text scoreLabel;
	public Text GameIsOver;
	public Text TapToTitle;
	StageSoundEffect stagesoundEffect;

	void Start()
	{
		//ゲーム開始と同時にPlayingステートに移行する
		Playing ();
	}

	void Update()
	{
		//ゲームのステートごとにイベントを監視する
		switch (state) {

		case State.PLAY:

			//スコアラベルを更新する
			int score = CalcScore ();
			scoreLabel.text = "Score : " + score + "pts";

			//プレイヤーのライフが0になったらゲームオーバー
			if (player.Life () <= 0){
				enabled = false;

				//ハイスコアを更新する
				if (PlayerPrefs.GetInt ("Hiscore") < score) {
					PlayerPrefs.SetInt ("Hiscore", score);
				}
				//0.5秒後にGameoverステートに移行する
				Invoke("Gameover", 0.5f);
			}
			break;

		case State.GAMEOVER:
			//マウスを左クリックしたらTitleシーンに戻る
			if (Input.GetMouseButtonDown (0))
				Application.LoadLevel("Title");
			break;
		}
	}


	public void Playing()
	{
		state = State.PLAY;

		//GameOverの文字を非表示
		GameIsOver.enabled = false;
		TapToTitle.enabled = false;

		//設定したサウンドを読み込む
		stagesoundEffect = GameObject.Find("StageSoundController").
			GetComponent<StageSoundEffect>();
	}

	int CalcScore()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}

	void Gameover()
	{
		state = State.GAMEOVER;

		enabled = true;

		//ゲームオーバー画面を表示する
		GameIsOver.enabled = true;
		TapToTitle.enabled = true;

		//ステージBGMを停止しゲームオーバーBGMを再生する
		stagesoundEffect.GameIsOver ();

		//ハイスコアを初期化する
		//PlayerPrefs.DeleteAll();
	}
}