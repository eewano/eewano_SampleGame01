using UnityEngine;
using System.Collections;

public class StageSoundEffect : MonoBehaviour {

	private AudioSource JumpSound;
	private AudioSource MoveSound;
	private AudioSource DownSound;
	private AudioSource GameOverBGM;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		JumpSound = audioSources [0];
		MoveSound = audioSources [1];
		DownSound = audioSources [2];
		GameOverBGM = audioSources [3];
	}

	public void Jump() {
		JumpSound.PlayOneShot (JumpSound.clip);
	}

	public void Move() {
		MoveSound.PlayOneShot (MoveSound.clip);
	}

	public void Down() {
		DownSound.PlayOneShot (DownSound.clip);
	}

	public void GameIsOver() {
		//ステージ01サウンドを停止してゲームオーバーBGMを開始する
		GameOverBGM.PlayOneShot (GameOverBGM.clip);
	}
}
