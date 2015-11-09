using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//必要なコンポーネントを自動で取得する
	CharacterController controller;
	Animator animator;

	public static bool gameOver = false;

	void Start()
	{
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator>();

		gameOver = false;
		GetComponent<Collider>().isTrigger = true;

		animator.SetBool ("Left Run", false);
		animator.SetBool ("Right Run", false);
	}

	public float nowSpeed = 2.5f;
	public float speedPlus = 0.2f;

	void Update()
	{
		if(gameOver)
		{
			return;
		}

		nowSpeed += Time.deltaTime * speedPlus;

		float axisValue = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * axisValue * nowSpeed * Time.deltaTime);

		if (Input.GetKey ("left")) {
				animator.SetBool ("Left Run", true);
		} else {
			animator.SetBool ("Left Run", false);
		}

		if (Input.GetKey ("right")) {
			animator.SetBool ("Right Run", true);
		} else {
			animator.SetBool ("Right Run", false);
		}
	}
}