using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	ParticleSystem particle;

	void Start()
	{
		particle = GetComponent<ParticleSystem> ();
	}

	void Update()
	{
		//Particleの再生が終了したらParticleを削除する。
		if (particle.isPlaying == false)
			Destroy (gameObject);
	}
}