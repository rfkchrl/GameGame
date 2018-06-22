using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 15f;
	public float impactForce = 30f;

	//Reload
	public int maxAmmo = 10;
	private int currentAmmo;
	public float reloadTime = 1f;
	private bool isReloading = false;

	//public Camera fpsCam;
	public Transform targetRaycast;
	public ParticleSystem muzzelFlash;
	public GameObject impactEffect;

	private float nextTimeToFire = 0f;
	//public Animator animator;

	void start () {
		currentAmmo = maxAmmo;

	}

	void OnEnable(){
		isReloading = false;
		//animator.SetBool ("Reloading", false);
	}


	// Update is called once per frame
	void Update () {

		if (isReloading)
			return;

		if (currentAmmo <= 0) {
			StartCoroutine (Reload ());
			return;
		}

		if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
		{
			nextTimeToFire = Time.time + 1f / fireRate;
			Shoot ();
		}
	}

	IEnumerator Reload(){
		isReloading = true;
		Debug.Log ("Reloading..");

		//animator.SetBool ("Reloading", true);
		yield return new WaitForSeconds (reloadTime - .25f);

		//animator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (- .25f);

		currentAmmo = maxAmmo;
		isReloading = false;

	}

	void Shoot()
	{
		muzzelFlash.Play ();

		currentAmmo--;


		RaycastHit hit;
		if (Physics.Raycast (targetRaycast.transform.position, targetRaycast.transform.forward, out hit, range)) 
		{
			Debug.Log (hit.transform.name);
			Target target = hit.transform.GetComponent<Target> ();

			if (target != null) 
			{
				target.TakeDamage(damage);
			}

			if (hit.rigidbody != null) 
			{
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 1f);
		}
	}
}
