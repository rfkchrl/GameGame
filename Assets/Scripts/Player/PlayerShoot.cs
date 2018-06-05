using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField]Shooter assaultRifle;
	public ParticleSystem MuzzleFlash;


	void Update(){
		if (GameManager.Instance.InputController.Fire1) {
			MuzzleFlash.Play ();
			assaultRifle.Fire();
		}
	}
}
