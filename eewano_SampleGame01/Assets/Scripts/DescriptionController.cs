using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {

	DescriptionSoundEffect descriptionsoundEffect;

	public void Start()
	{
		descriptionsoundEffect = GameObject.Find("DescriptionSoundController").
			GetComponent<DescriptionSoundEffect>();
	}

	//-----ゲームをスタートさせる-----
	public void OnStartButtonClicked()
	{
		//スタートサウンドを再生する
		descriptionsoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage", 1.0f);
	}
	
	void GoToStage()
	{
		Application.LoadLevel ("Stage");
	}
	//-----ゲームをスタートさせる-----
}