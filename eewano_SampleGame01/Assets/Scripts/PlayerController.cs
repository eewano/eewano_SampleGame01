using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private bool Left = false;
	private bool Right = false;
	private bool Jump = false;
	public static bool Fall = false;
	
	const int DefaultLife = 1;	//プレイヤーのライフ
	const float StunDuration = 1.0f;	//被ダメージ時の仰け反り時間

	private CharacterController controller;
	private Animator animator;
	private StageSoundEffect stageSoundEffect;

	Vector3 moveDirection = Vector3.zero;
	private int life = DefaultLife;
	private float recoverTime = 0.0f;

	[SerializeField] private float accelerationZ;
	[SerializeField] private float gravity;
	[SerializeField] private float speedX;
	[SerializeField] private float speedZ;
	[SerializeField] private float speedJump;
	[SerializeField] private float speedPlus;

	//-----ライフ取得用の関数-----
	public int Life()
	{
		return life;
	}
	//----------

	//-----仰け反り判定-----
	private bool IsStan()
	{
		return recoverTime > 0.0f || life <= 0;
	}
	//----------

	//-----移動ボタン長押し用の判定-----
	void PushLeftDown()
	{
		Left = true;
	}
	void PushLeftUp()
	{
		Left = false;
	}
	void PushRightDown()
	{
		Right = true;
	}
	void PushRightUp()
	{
		Right = false;
	}
	void PushJumpDown()
	{
		Jump = true;
	}
	void PushJumpUp()
	{
		Jump = false;
	}
	//----------

	void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		stageSoundEffect = GameObject.Find("StageSoundEffect").GetComponent<StageSoundEffect>();
		Fall = false;
	}

	void Update()
	{
		if (Life () <= 0) {
			Invoke("Dead", 1.5f);	//ライフが0になったらプレイヤーを消去する
		}

		//移動を実行する
		Vector3 globalDirection = transform.TransformDirection (moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

		//重力分の力を毎フレーム追加する
		moveDirection.y -= gravity * Time.deltaTime;

		//移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded)
			moveDirection.y = 0;

		//速度が０以上なら走るアニメーションにする
		animator.SetBool ("Run", moveDirection.z > 0.0f);

		speedZ += Time.deltaTime * speedPlus;

		if (Left && controller.isGrounded) {
			MoveLeft ();
		} else if (Right && controller.isGrounded) {
			MoveRight ();
		} else if (Jump) {
			MoveJump ();
		}

		//-----仰け反り時の行動-----
		if (IsStan ()) {
			//動きを止めて仰け反り状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		} else {
			//徐々に加速しながら前進する
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
			moveDirection.z = Mathf.Clamp (acceleratedZ, 0, speedZ);
		}
		//----------
	}

	//-----横移動及びジャンプ-----
	void MoveLeft()
	{
		this.transform.position += this.transform.right * Time.deltaTime * speedX * -1;
	}

	void MoveRight()
	{
		this.transform.position += this.transform.right * Time.deltaTime * speedX;
	}
		
	void MoveJump()
	{
		if (IsStan ())
			return;	//仰け反り時の入力キャンセル
		if (controller.isGrounded && Jump == true) {
			moveDirection.y = speedJump;
			animator.SetTrigger ("Jump");
			stageSoundEffect.Jump();
		}
	}
	//----------

	//-----各オブジェクトとの衝突判定-----
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (IsStan ())
			return;

		if (hit.gameObject.tag == "Obstacle") {
			//ライフを減らして仰け反り状態に移行
			life --;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stageSoundEffect.Down();
		}

		if (hit.gameObject.tag == "Ball") {
			life --;
			recoverTime = StunDuration;
			animator.SetTrigger ("Down");
			stageSoundEffect.Down();
			Destroy (hit.gameObject, 1.5f);
		}

		if (hit.gameObject.tag == "Fall") {
			Fall = true;
			life = 0;
			stageSoundEffect.Falling();
			Destroy (hit.gameObject);
		}
	}
	//----------

	void Dead()
	{
		gameObject.SetActive (false);
	}
}