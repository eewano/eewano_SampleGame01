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
	[SerializeField] Text scoreLabel = null;
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
			int score = CalcScore ();
			scoreLabel.text = "Score : " + score + "pts";

			if (player.Life () <= 0){
				enabled = false;

				//ハイスコアを更新する
				if (PlayerPrefs.GetInt ("Hiscore") < score) {
					PlayerPrefs.SetInt ("Hiscore", score);
				}
				Invoke("Gameover", 0.5f);
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

		ButtonLeft.gameObject.SetActive(false);
		ButtonRight.gameObject.SetActive(false);
		ButtonJump.gameObject.SetActive(false);
	}

	void Playing()
	{
		state = State.PLAY;
		AllFalse ();

		ButtonLeft.gameObject.SetActive(true);
		ButtonRight.gameObject.SetActive(true);
		ButtonJump.gameObject.SetActive(true);
	}

	void Gameover()
	{
		state = State.GAMEOVER;
		AllFalse ();
		enabled = true;

		GameIsOver.enabled = true;
		TapToTitle.enabled = true;

		stageBGM.Stop();
		stagesoundEffect.GameIsOver ();

		//ハイスコアを初期化する
		//PlayerPrefs.DeleteAll();
	}

	int CalcScore()
	{
		//プレイヤーの走行距離をスコアとする
		return(int)player.transform.position.z;
	}
}