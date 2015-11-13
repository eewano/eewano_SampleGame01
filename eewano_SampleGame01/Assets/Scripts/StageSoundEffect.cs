using UnityEngine;
using System.Collections;

public class StageSoundEffect : MonoBehaviour {

	private AudioSource JumpSound;
	private AudioSource MoveSound;
	private AudioSource DownSound;
	private AudioSource GameOverBGM;
	private AudioSource Stage01BGM;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		JumpSound = audioSources [0];
		MoveSound = audioSources [1];
		DownSound = audioSources [2];
		GameOverBGM = audioSources [3];
		Stage01BGM = audioSources [4];
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

	public void GameOver() {
		Destroy(Stage01BGM);
		GameOverBGM.PlayOneShot (GameOverBGM.clip);
	}
}
