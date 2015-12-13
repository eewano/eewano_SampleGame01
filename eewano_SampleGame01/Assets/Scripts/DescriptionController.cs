﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DescriptionController : MonoBehaviour {

	DescriptionSoundEffect descriptionsoundEffect;

	public void Start()
	{
		descriptionsoundEffect = GameObject.Find("DescriptionSoundController").
			GetComponent<DescriptionSoundEffect>();
	}

	//-----ゲームをスタートさせる-----
	public void OnStage01ButtonClicked()
	{
		//スタートサウンドを再生する
		descriptionsoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage01", 1.0f);
	}

	public void OnStage02ButtonClicked()
	{
		//スタートサウンドを再生する
		descriptionsoundEffect.GameStart();

		//1秒後にステージシーンに切り替える
		Invoke ("GoToStage02", 1.0f);
	}
	
	void GoToStage01()
	{
		SceneManager.LoadScene ("Stage01");
	}

	void GoToStage02()
	{
		SceneManager.LoadScene ("Stage02");
	}
	//-----ゲームをスタートさせる-----
}