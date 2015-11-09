using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

	public void OnStartButtonClicked()
	{
		//Stageシーンを呼び出す
		Application.LoadLevel ("Stage");
	}
}
