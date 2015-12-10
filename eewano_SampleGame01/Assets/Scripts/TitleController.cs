using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public void OnStage01ButtonClicked()
	{
		//スタートサウンドを再生する
		titlesoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage01", 1.0f);
	}

	public void OnStage02ButtonClicked()
	{
		//スタートサウンドを再生する
		titlesoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage02", 1.0f);
	}
	
	void GoToStage01()
	{
		Application.LoadLevel ("Stage01");
	}

	void GoToStage02()
	{
		Application.LoadLevel ("Stage02");
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
		Application.LoadLevel ("Description");
	}
	//-----説明画面に飛ぶ-----
}
