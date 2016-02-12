using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {

	Vector3 startPosition;

	const float amplitude = 5.0f;	//ボールのX軸の振れ幅
	[SerializeField] private float speed;

	void Start()
	{
		startPosition = transform.localPosition;
	}

	void Update ()
	{
		//変位を計算する
		float x = amplitude * Mathf.Cos(Time.time * speed);

		//xを変位させたポジションに再設定する
		transform.localPosition = startPosition + new Vector3(x, 0, 0);
	}
}