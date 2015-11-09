﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	const int MinLane = -5;
	const int MaxLane = 5;
	const float LaneWidth = 1.0f;

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;

	public float gravity;
	public float speedZ;
	public float speedX;
	public float speedJump;
	public float accelerationZ;

	void Start()
	{
		//必要なコンポーネントを自動で取得する
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		//デバッグ用
		if (Input.GetKeyDown ("left"))
			MoveToLeft ();
		if (Input.GetKeyDown ("right"))
			MoveToRight ();
		if (Input.GetKeyDown ("space"))
			Jump ();

		//徐々に加速しZ方向に前進させる
		float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
		moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);

		//X方向は目標のポジションまでの差分の割合で速度を計算
		float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
		moveDirection.x = ratioX * speedX;

		//重力分の力を毎フレーム追加
		moveDirection.y -= gravity * Time.deltaTime;

		//移動実行
		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

		//移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded)
			moveDirection.y = 0;

		//速度が０以上なら走っているフラグをtrueにする
		animator.SetBool ("Run", moveDirection.z > 0.0f);
	}

	//左のレーンに移動を開始
	public void MoveToLeft()
	{
		if (controller.isGrounded && targetLane > MinLane)
			targetLane--;
	}

	//右のレーンに移動を開始
	public void MoveToRight()
	{
		if (controller.isGrounded && targetLane < MaxLane)
			targetLane++;
	}

	public void Jump()
	{
		if (controller.isGrounded) {
			moveDirection.y = speedJump;

			//ジャンプトリガーを設定
			animator.SetTrigger ("jump");
		}
	}
}