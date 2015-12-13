using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public Text hiScoreLabel;
	TitleSoundEffect titlesoundEffect;

	public void Start()
	{
		//ハイスコアを表示する
		hiScoreLabel.text = "Hi-score : " + PlayerPrefs.GetInt("Hiscore") + "pts";
		titlesoundEffect = GameObject.Find("TitleSoundController").GetComponent<TitleSoundEffect>();
	}

	//-----ゲームをスタートさせる-----
	public void OnStartButtonClicked()
	{
		//スタートサウンドを再生する
		titlesoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage", 1.0f);
	}
	
	void GoToStage()
	{
		SceneManager.LoadScene ("Stage");
	}
	//-----ゲームをスタートさせる-----

	//-----説明画面に飛ぶ-----
	public void OnDescriptionButtonClicked()
	{
		//説明サウンドを再生する
		titlesoundEffect.Description();
		
		//1秒後に説明シーンに切り替える
		Invoke ("GoToDescription", 1.0f);
	}
	
	void GoToDescription()
	{
		SceneManager.LoadScene ("Description");
	}
	//-----説明画面に飛ぶ-----
}
