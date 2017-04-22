using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class AsteroidComponent : MonoBehaviour {

	private Rigidbody2D m_RigidBody;

	private Vector2 m_ForceDirection;

	private SpriteRenderer m_Renderer;
	private HealthComponent m_HealthComponent;

	private bool m_IsWrappingX = false;
	private bool m_IsWrappingY = false;

	[SerializeField]
	private int m_AsteroidSize = 0;
    public int GetAsteroidSize()
    {
        return m_AsteroidSize;
    }

	void Awake() {
		m_RigidBody = GetComponent<Rigidbody2D> ();

		m_Renderer = GetComponent<SpriteRenderer> ();
		m_HealthComponent = GetComponent<HealthComponent> ();

		m_HealthComponent.Died += () => {
            GameController.instance.DestroyAsteroid(this);
        };
	}

	// Use this for initialization
	void Start () {
		
	}

    public void Initialize(int size)
    {
        var s = (float) size;
        var x = Random.Range(s, 1.0f + s);
        var y = Random.Range(s, 1.0f + s);
        var which = Random.Range(0, 2);
        if (which == 1)
        {
            x *= -1.0f;
        }
        which = Random.Range(0, 2);
        if (which == 2)
        {
            y *= -1.0f;
        }

        m_ForceDirection = new Vector2 (x, y);

        m_RigidBody.velocity = m_ForceDirection;

        m_RigidBody.AddTorque (Random.Range(-5.0f, 5.0f));
    }
	
	// Update is called once per frame
	void Update () {
		m_RigidBody.AddForce (m_ForceDirection);

		var viewportPosition = Camera.main.WorldToViewportPoint (transform.position);

		var newPosition = transform.position;
			
		if (viewportPosition.x > 1 || viewportPosition.x < 0) {
			if (!m_IsWrappingX) {
				newPosition.x = -newPosition.x;
				m_IsWrappingX = true;
			}
		} else {
			m_IsWrappingX = false;
		}

		if (viewportPosition.y > 1 || viewportPosition.y < 0) {
			if (!m_IsWrappingY) {
				newPosition.y = -newPosition.y;
				m_IsWrappingY = true;
			}
		} else {
			m_IsWrappingY = false;
		}

		transform.position = newPosition;
	}

	void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag != "Asteroid")
        {
            m_HealthComponent.TakeDamage (1);
        }
    }
}
