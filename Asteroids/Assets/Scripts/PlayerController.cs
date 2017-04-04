using System.Collections;
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

	[Header("Weapon")]
	public GameObject BulletPrefab;
	[Tooltip("How often can you fire per second")]
	public float RateOfFire = 5.0f;
	private float _timeToFire = 0.0f;
	private float _fireTimer = 0.0f;

	void Awake(){
		m_RigidBody = GetComponent<Rigidbody2D> ();
		m_Renderer = GetComponent<SpriteRenderer> ();
		m_Collider = GetComponent<Collider2D> ();

		_timeToFire = 1.0f / RateOfFire;
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

		_fireTimer -= Time.deltaTime;

		if (Input.GetMouseButton (0) && _fireTimer <= 0.0f) {
			Fire ();
		}

		if (Input.GetKey (KeyCode.Space)) {
			m_RigidBody.AddForce (transform.right * Speed);
		}
	}

	void Fire(){
		_fireTimer = _timeToFire;
		var bullet = (GameObject)Instantiate (BulletPrefab);
		bullet.transform.position = transform.position;
		bullet.GetComponent<BulletComponent> ().Init (transform.position, transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D other){
//		m_Alive = false;
//		m_RigidBody.velocity = new Vector2 ();
	}
}
