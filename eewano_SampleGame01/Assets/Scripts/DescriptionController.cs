using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DescriptionController : MonoBehaviour {

	private DescriptionSoundEffect descriptionsoundEffect;

	void Start()
	{
		descriptionsoundEffect = GameObject.Find("DescriptionSoundController").
			GetComponent<DescriptionSoundEffect>();
	}

	void OnStage01ButtonClicked()
	{
		descriptionsoundEffect.GameStart();
		Invoke ("GoToStage01", 1.0f);
	}

	void OnStage02ButtonClicked()
	{
		descriptionsoundEffect.GameStart();
		Invoke ("GoToStage02", 1.0f);
	}
	
	void GoToStage01()
	{
		TitleController.Stage01 = true;
		SceneManager.LoadScene ("Stage01");
	}

	void GoToStage02()
	{
		TitleController.Stage02 = true;
		SceneManager.LoadScene ("Stage02");
	}
}