using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	[SerializeField] Text hiScoreLabel = null;
	private TitleSoundEffect titlesoundEffect;

	void Start()
	{
		hiScoreLabel.text = "Hi-score : " + PlayerPrefs.GetInt("Hiscore") + "pts";
		titlesoundEffect = GameObject.Find("TitleSoundController").GetComponent<TitleSoundEffect>();
	}

	void OnStage01ButtonClicked()
	{
		titlesoundEffect.GameStart();
		Invoke ("GoToStage01", 1.0f);
	}

	void OnStage02ButtonClicked()
	{
		titlesoundEffect.GameStart();
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

	void OnDescriptionButtonClicked()
	{
		titlesoundEffect.Description();
		Invoke ("GoToDescription", 1.0f);
	}
	
	void GoToDescription()
	{
		SceneManager.LoadScene ("Description");
	}
}