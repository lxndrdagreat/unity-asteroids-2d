﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float m_RotationSpeed = 10.0f;

	public float Speed = 1.0f;

	private Rigidbody2D m_RigidBody;
	private SpriteRenderer m_Renderer;
	private Collider2D m_Collider;

	private bool m_Alive = true;

	void Awake(){
		m_RigidBody = GetComponent<Rigidbody2D> ();
		m_Renderer = GetComponent<SpriteRenderer> ();
		m_Collider = GetComponent<Collider2D> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		m_Renderer.enabled = m_Alive;
		m_Collider.enabled = m_Alive;
		if (!m_Alive) {
			return;
		}

		var cam = Camera.main;
		float camDis = cam.transform.position.y - transform.position.y;

		Vector3 mouse = cam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camDis));

		float angleRad = Mathf.Atan2 (mouse.y - transform.position.y, mouse.x - transform.position.x);
		float angle = (180 / Mathf.PI) * angleRad;

		var rotation = Quaternion.Euler (0, 0, angle);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * m_RotationSpeed);

		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}

		if (Input.GetKey (KeyCode.Space)) {
			m_RigidBody.AddForce (transform.right * Speed);
		}
	}

	void Fire(){
		print ("Fire!");
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.right);

		if (hit.collider && hit.collider.gameObject.tag == "Asteroid") {
			print ("Hit!");
			var asteroid = hit.collider.gameObject.GetComponent<AsteroidComponent> ();
			var healthComp = hit.collider.gameObject.GetComponent<HealthComponent> ();
			healthComp.TakeDamage (1);
		} else if (hit.collider) {
			print (hit.transform.gameObject.name);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		m_Alive = false;
		m_RigidBody.velocity = new Vector2 ();
	}
}
