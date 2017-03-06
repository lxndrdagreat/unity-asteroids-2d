using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_RotationSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var cam = Camera.main;
		float camDis = cam.transform.position.y - transform.position.y;

		Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));

		float angleRad = Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x);
		float angle = (180 / Mathf.PI) * angleRad;

		var rotation = Quaternion.Euler (0, 0, angle);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * m_RotationSpeed);
	}
}
