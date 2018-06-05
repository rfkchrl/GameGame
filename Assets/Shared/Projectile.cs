using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField]float speed;
	[SerializeField]float timeToLive;
	[SerializeField]float demage;


	void Start(){
		Destroy(gameObject, timeToLive);

	}

	void Update(){
		transform.Translate (Vector3.forward * speed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other){
		Debug.Log (other);
		var destructable = other.transform.GetComponent<Destructable>();
		if (destructable == null)
			return;
		
		destructable.TakeDamage (demage);

	}
}
