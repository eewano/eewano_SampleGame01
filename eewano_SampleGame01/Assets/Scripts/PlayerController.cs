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
	}

	public float nowSpeed = 3.0f;
	public float speedPlus = 0.1f;

	void Update()
	{
		if(gameOver)
		{
			return;
		}

		nowSpeed += Time.deltaTime * speedPlus;

		if (Input.GetKey ("left")) {
			transform.localPosition = new Vector3(
				transform.localPosition.x - Time.deltaTime * nowSpeed ,
				transform.localPosition.y ,
				transform.localPosition.z );

			animator.SetBool ("Left Run", true);
		} else {
			animator.SetBool ("Left Run", false);
		}

		if (Input.GetKey ("right")) {
			transform.localPosition = new Vector3(
				transform.localPosition.x + Time.deltaTime * nowSpeed ,
				transform.localPosition.y ,
				transform.localPosition.z );
			
			animator.SetBool ("Right Run", true);
		} else {
			animator.SetBool ("Right Run", false);
		}
	}
}