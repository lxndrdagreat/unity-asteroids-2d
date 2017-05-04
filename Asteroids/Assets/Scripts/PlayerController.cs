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
    private bool m_IsWrappingX = false;
    private bool m_IsWrappingY = false;

    [Header("Weapons")]	
    public Weapon[] Weapons;

    [SerializeField] private int _CurrentWeapon = 0;

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

        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        var newPosition = transform.position;

        if (viewportPosition.x > 1 || viewportPosition.x < 0)
        {
            if (!m_IsWrappingX)
            {
                newPosition.x = -newPosition.x;
                m_IsWrappingX = true;
            }
        }
        else {
            m_IsWrappingX = false;
        }

        if (viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            if (!m_IsWrappingY)
            {
                newPosition.y = -newPosition.y;
                m_IsWrappingY = true;
            }
        }
        else {
            m_IsWrappingY = false;
        }

        transform.position = newPosition;

        Weapons[_CurrentWeapon].Update();

		if (Input.GetMouseButton (0)) {
            Weapons[_CurrentWeapon].Fire(transform.position, transform.rotation);
		}

		if (Input.GetKey (KeyCode.Space)) {
			m_RigidBody.AddForce (transform.right * Speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
//		m_Alive = false;
//		m_RigidBody.velocity = new Vector2 ();
	}
}
