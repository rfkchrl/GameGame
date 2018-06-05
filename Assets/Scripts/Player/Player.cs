using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveController))]


public class Player : MonoBehaviour {

	private Rigidbody rb;
	[System.Serializable]
	public class MouseInput	{
		public Vector2 Damping;
		public Vector2 Sensitivity;
		public bool LockMouse;
	}
		
	[SerializeField] float speed = 10f;
	[SerializeField] float jumpForce = 8f;
	[SerializeField] float gravity = 30f;

	private Vector3 moveDir = Vector3.zero;

	[SerializeField]MouseInput MouseController;


	private MoveController m_MoveController;
	public MoveController MoveController{
		get { 
			if (m_MoveController == null)
				m_MoveController = GetComponent<MoveController> ();
			return m_MoveController;
		}
	}

	private Crosshair m_Crosshair;
	private Crosshair Crosshair {
		get { 
			if (m_Crosshair == null)
				m_Crosshair = GetComponentInChildren<Crosshair> ();
			return m_Crosshair;
		}
	}

	InputController playerInput;
	Vector2 mouseInput;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		playerInput = GameManager.Instance.InputController;
		GameManager.Instance.LocalPlayer = this;

		if (MouseController.LockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

		}
	}

	void Update () {
		Move ();
		LookAround ();
		//Jump ();
	}


	void LookAround ()
	{
		mouseInput.x = Mathf.Lerp (mouseInput.x, playerInput.MouseInput.x, 1f / MouseController.Damping.x);
		mouseInput.y = Mathf.Lerp (mouseInput.y, playerInput.MouseInput.y, 1f / MouseController.Damping.y);
		transform.Rotate (Vector3.up * mouseInput.x * MouseController.Sensitivity.x);
//		Crosshair.LookHeigth (mouseInput.y * MouseController.Sensitivity.y);
	}

	void Move(){
		CharacterController controller = gameObject.GetComponent<CharacterController> ();

		if (controller.isGrounded) {
			moveDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

			moveDir = transform.TransformDirection (moveDir);

			moveDir *= speed;

			if (Input.GetButtonDown ("Jump")) {
				moveDir.y = jumpForce;
			}
		}

		moveDir.y -= gravity * Time.deltaTime;
		controller.Move (moveDir * Time.deltaTime);
	}
	}



//void Move(){

//	float moveSpeed = runSpeed;
//
//	if (playerInput.IsWalking)
//		moveSpeed = walkSpeed;
//
//	if (playerInput.IsSprinting)
//		moveSpeed = sprintSpeed;
//
//	if (playerInput.IsCrouched)
//		moveSpeed = crouchSpeed;
//
//
//	Vector2 direction = new Vector2 (playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
//	MoveController.Move(direction);	


//}
//void Jump(){
//	CharacterController controller = gameObject.GetComponent<CharacterController> ();

//	if (controller.isGrounded) {
//		moveDir = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

//		moveDir = transform.TransformDirection (moveDir);

//		moveDir *= speed;

//		if (Input.GetButtonDown ("Jump")) {
//			moveDir.y = jumpForce;
//		}
//	}

//	moveDir.y -= gravity * Time.deltaTime;
//	controller.Move (moveDir * Time.deltaTime);
//}
