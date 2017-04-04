using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour {

	private LineRenderer _lineRenderer;

	public float Lifetime = 0.1f;

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
		print (direction.eulerAngles.z);
		float angle = direction.eulerAngles.z * (Mathf.PI / 180.0f);
		var offset = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
		_lineRenderer.SetPosition (1, center + offset);
	}
}
