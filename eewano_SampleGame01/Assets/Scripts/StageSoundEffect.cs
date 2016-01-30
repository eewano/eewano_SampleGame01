using UnityEngine;
using System.Collections;

public class StageSoundEffect : MonoBehaviour {

	private AudioSource JumpSound;
	private AudioSource MoveSound;
	private AudioSource DownSound;
	private AudioSource FallSound;
	private AudioSource GameOverBGM;

	void Start()
	{
		AudioSource[] audioSources = GetComponents<AudioSource> ();
		JumpSound = audioSources [0];
		MoveSound = audioSources [1];
		DownSound = audioSources [2];
		FallSound = audioSources [3];
		GameOverBGM = audioSources [4];
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

	public void Falling() {
		FallSound.PlayOneShot (FallSound.clip);
	}

	public void GameIsOver() {
		GameOverBGM.PlayOneShot (GameOverBGM.clip);
	}
}