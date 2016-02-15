using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	public static bool Stage01;
	public static bool Stage02;

	[SerializeField] private Text hiScore01Label;
	[SerializeField] private Text hiScore02Label;
	private TitleSoundEffect titleSoundEffect;

	void Start()
	{
		hiScore01Label.text = "Normal : " + PlayerPrefs.GetInt("Hiscore01") + "pts";
		hiScore02Label.text = "Hard : " + PlayerPrefs.GetInt("Hiscore02") + "pts";
		titleSoundEffect = GameObject.Find("TitleSoundEffect").GetComponent<TitleSoundEffect>();
		Stage01 = false;
		Stage02 = false;
	}

	void OnDescriptionButtonClicked()
	{
		titleSoundEffect.Description();
		Invoke ("GoToDescription", 1.0f);
	}
	void OnStage01ButtonClicked()
	{
		titleSoundEffect.GameStart();
		Invoke ("GoToStage01", 1.0f);
	}
	void OnStage02ButtonClicked()
	{
		titleSoundEffect.GameStart();
		Invoke ("GoToStage02", 1.0f);
	}

	void GoToDescription()
	{
		SceneManager.LoadScene ("Description");
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
}