using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	enum State
	{
		PLAY,
		GAMEOVER
	}

	State state;

	[SerializeField] PlayerController player = null;
	[SerializeField] Text score01Label = null;
	[SerializeField] Text score02Label = null;
	[SerializeField] Text GameIsOver = null;
	[SerializeField] Text TapToTitle = null;
	[SerializeField] GameObject ButtonLeft = null;
	[SerializeField] GameObject ButtonRight = null;
	[SerializeField] GameObject ButtonJump = null;

	private StageSoundEffect stagesoundEffect;
	private AudioSource stageBGM;

	void Start()
	{
		stageBGM = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		stagesoundEffect = GameObject.Find("StageSoundController").
			GetComponent<StageSoundEffect>();
		Playing ();
	}

	void Update()
	{
		switch (state) {

		case State.PLAY:

			//スコアラベルを更新する
			if (TitleController.Stage01) {
				int score01 = CalcScoreSt01 ();
				score01Label.text = "Score : " + score01 + "pts";
				if (player.Life () <= 0) {
					enabled = false;

					//NORMAL STAGE のハイスコアを更新する
					if (PlayerPrefs.GetInt ("Hiscore01") < score01) {
						PlayerPrefs.SetInt ("Hiscore01", score01);
					}
					Invoke ("Gameover", 0.5f);
				}

			} else if(TitleController.Stage02) {
				int score02 = CalcScoreSt02 ();
				score02Label.text = "Score : " + score02 + "pts";
				if (player.Life () <= 0) {
					enabled = false;

					//HARD STAGE のハイスコアを更新する
					if (PlayerPrefs.GetInt ("Hiscore02") < score02) {
						PlayerPrefs.SetInt ("Hiscore02", score02);
					}
					Invoke ("Gameover", 0.5f);
				}
			}
			break;

		case State.GAMEOVER:
			if(Input.GetMouseButtonDown(0))
				SceneManager.LoadScene("Title");
			break;
		}
	}

	void AllFalse()
	{
		GameIsOver.enabled = false;
		TapToTitle.enabled = false;
		score01Label.enabled = false;
		score02Label.enabled = false;

		ButtonLeft.gameObject.SetActive(false);
		ButtonRight.gameObject.SetActive(false);
		ButtonJump.gameObject.SetActive(false);
	}

	void Playing()
	{
		state = State.PLAY;
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

	void Gameover()
	{
		state = State.GAMEOVER;
		AllFalse ();
		enabled = true;

		if (TitleController.Stage01) {
			score01Label.enabled = true;
		} else if (TitleController.Stage02) {
			score02Label.enabled = true;
		}

		GameIsOver.enabled = true;
		TapToTitle.enabled = true;

		stageBGM.Stop();
		stagesoundEffect.GameIsOver ();

		//ハイスコアを初期化する
		//PlayerPrefs.DeleteAll();
	}

	int CalcScoreSt01()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}

	int CalcScoreSt02()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}
}