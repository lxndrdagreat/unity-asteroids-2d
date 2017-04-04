using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour {

	private LineRenderer _lineRenderer;

	public float Lifetime = 0.1f;
	public float Range = 10.0f;

	void Awake() {
		_lineRenderer = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Lifetime -= Time.deltaTime;

		if (Lifetime <= 0.0f) {
			Destroy (gameObject);
		}
	}

	public void Init(Vector2 position, Quaternion direction) {
		transform.position = position;

		_lineRenderer.SetPosition (0, position);

		var center = position;
		float angleDegrees = (360.0f - direction.eulerAngles.z + 90.0f);
		float angle = angleDegrees * (Mathf.PI / 180.0f);
		var angleVector = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
		var offset = angleVector * Range;
		_lineRenderer.SetPosition (1, center + offset);

		RaycastHit2D hit = Physics2D.Raycast (transform.position, angleVector, Range, 1 << 8);

		if (hit.collider && hit.collider.gameObject.tag == "Asteroid") {
			print ("Hit!");
			var asteroid = hit.collider.gameObject.GetComponent<AsteroidComponent> ();
			var healthComp = hit.collider.gameObject.GetComponent<HealthComponent> ();
			healthComp.TakeDamage (1);
		} else if (hit.collider) {
			print (hit.transform.gameObject.name);
		}
	}
}
