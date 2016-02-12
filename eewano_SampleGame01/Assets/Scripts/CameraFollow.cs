using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	Vector3 diff;

	[SerializeField] private PlayerController player;
	[SerializeField] private GameObject target;
	[SerializeField] private float followSpeed;

	void Start()
	{
		//プレイヤーを追従する距離を計算する
		diff = target.transform.position - transform.position;
	}

	void LateUpdate()
	{
		if (player.Life () <= 0) {
			Invoke ("CameraStop", 1.5f);
		}

		if (PlayerController.Fall) {
			return;
		}

		//プレイヤーとの距離が離れている程、追従速度が上がる
		transform.position = Vector3.Lerp (
			transform.position,
			target.transform.position - diff,
			Time.deltaTime * followSpeed);
	}

	void CameraStop()
	{
		return;
	}
}