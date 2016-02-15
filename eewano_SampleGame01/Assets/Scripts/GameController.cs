using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	enum State {PLAYING, GAMEOVER}
	private State state;

	[SerializeField] private PlayerController player;
	[SerializeField] private Text GameIsOver;
	[SerializeField] private Text score01Label;
	[SerializeField] private Text score02Label;
	[SerializeField] private Text TapToTitle;
	[SerializeField] private GameObject ButtonLeft;
	[SerializeField] private GameObject ButtonRight;
	[SerializeField] private GameObject ButtonJump;

	private AudioSource stageBGM;
	private StageSoundEffect stageSoundEffect;

	void Start()
	{
		stageBGM = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		stageSoundEffect = GameObject.Find("StageSoundEffect").GetComponent<StageSoundEffect>();
		
		Playing ();
	}

	void Update()
	{
		switch (state) {

		case State.PLAYING:
			//-----NORMAL STAGE のスコアラベルを更新する-----
			if (TitleController.Stage01) {
				int score01 = CalcScoreStage01 ();
				score01Label.text = "Score : " + score01 + "pts";
				if (player.Life () <= 0) {
					//ここで1度GameController.csを無効にしないと、ゲームオーバーBGMが重複し続けてしまう
					//理由は現時点で解決出来ていない
					enabled = false;
					if (PlayerPrefs.GetInt ("Hiscore01") < score01) {
						PlayerPrefs.SetInt ("Hiscore01", score01);	//NORMAL STAGE のハイスコアを更新する
					}
					Invoke ("GameOver", 0.5f);
				}
			}
			//----------

			//-----HARD STAGE のスコアラベルを更新する-----
			else if(TitleController.Stage02) {
				int score02 = CalcScoreStage02 ();
				score02Label.text = "Score : " + score02 + "pts";
				if (player.Life () <= 0) {
					//ここで1度GameController.csを無効にしないと、ゲームオーバーBGMが重複し続けてしまう
					//理由は現時点で解決出来ていない
					enabled = false;
					if (PlayerPrefs.GetInt ("Hiscore02") < score02) {
						PlayerPrefs.SetInt ("Hiscore02", score02);	//HARD STAGE のハイスコアを更新する
					}
					Invoke ("GameOver", 0.5f);
				}
			}
			//----------

			break;

		case State.GAMEOVER:
			if(Input.GetMouseButtonDown(0))
				SceneManager.LoadScene("Title");
			break;
		}
	}

	//-----まずはすべてのテキストやボタンを非表示にしてから、各ステートで表示させたいものをtrueにしている-----
	void AllFalse()
	{
		GameIsOver.enabled = false;
		score01Label.enabled = false;
		score02Label.enabled = false;
		TapToTitle.enabled = false;

		ButtonLeft.gameObject.SetActive(false);
		ButtonRight.gameObject.SetActive(false);
		ButtonJump.gameObject.SetActive(false);
	}
	//----------

	void Playing()
	{
		state = State.PLAYING;
		AllFalse ();

		if (TitleController.Stage01) {
			score01Label.enabled = true;
		} else if (TitleController.Stage02) {
			score02Label.enabled = true;
		}

		ButtonLeft.gameObject.SetActive(true);
		ButtonRight.gameObject.SetActive(true);
		ButtonJump.gameObject.SetActive(true);
	}

	void GameOver()
	{
		state = State.GAMEOVER;
		AllFalse ();
		//PLAYINGステートでGameController.csを1度無効にしているので、
		//ゲームオーバー時に再度有効にしている
		enabled = true;

		if (TitleController.Stage01) {
			score01Label.enabled = true;
		} else if (TitleController.Stage02) {
			score02Label.enabled = true;
		}

		GameIsOver.enabled = true;
		TapToTitle.enabled = true;

		stageBGM.Stop();
		stageSoundEffect.GameIsOver ();

		//PlayerPrefs.DeleteAll();	//ハイスコアを初期化する
	}

	//-----プレイヤーの走行距離をスコアとする-----
	int CalcScoreStage01()
	{
		return(int)player.transform.position.z;	//NORMAL STAGE
	}

	int CalcScoreStage02()
	{
		return(int)player.transform.position.z;	//HARD STAGE
	}
	//----------
}