﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitch : MonoBehaviour {

	public int selectedWeapon = 0;

	// Use this for initialization
	void Start () {
		SelectWeapon ();
	}
	
	// Update is called once per frame
	void Update () {
		int previousSelectedWeapon = selectedWeapon;

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			selectedWeapon = 0;
		}

		if (Input.GetKeyDown (KeyCode.Alpha2) && transform.childCount >=2) {
			selectedWeapon = 1;
		}
		if (previousSelectedWeapon != selectedWeapon) {
			SelectWeapon ();
		}
	}

	void SelectWeapon (){
		int i = 0;
		foreach (Transform weapon in transform) {
			if (i == selectedWeapon)
				weapon.gameObject.SetActive (true);
			else
				weapon.gameObject.SetActive (false);
			i++;
		}
	}
}
