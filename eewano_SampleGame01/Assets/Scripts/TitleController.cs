using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public static bool Stage01;
	public static bool Stage02;

	[SerializeField] private Text hiScore01Label;
	[SerializeField] private Text hiScore02Label;
	private TitleSoundEffect titlesoundEffect;

	void Start()
	{
		hiScore01Label.text = "Normal : " + PlayerPrefs.GetInt("Hiscore01") + "pts";
		hiScore02Label.text = "Hard : " + PlayerPrefs.GetInt("Hiscore02") + "pts";
		titlesoundEffect = GameObject.Find("TitleSoundController").GetComponent<TitleSoundEffect>();
		Stage01 = false;
		Stage02 = false;
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
		Stage01 = true;
		SceneManager.LoadScene ("Stage01");
	}

	void GoToStage02()
	{
		Stage02 = true;
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