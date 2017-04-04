using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private int m_Level = 1;
	public int Level { get { return m_Level; } }

	private bool m_GameActive = true;
	public bool GameActive { get { return m_GameActive; } }

    [Header("Asteroids")]
	public GameObject[] AsteroidPrefabs;

    public GameObject HitParticlePrefab;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
			return;
		}			

		SpawnAsteroids ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnAsteroids() {
		for (var i = 0; i < 5 + m_Level; ++i) {			
			var center = Camera.main.rect.center;
			float angle = Random.Range (0.0f, 360.0f);
			var offset = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)) * 5.0f;
			SpawnAsteroid (0, center + offset);
		}
	}

	public void SpawnAsteroid(int size, Vector2 position, int count = 1){
		if (size >= AsteroidPrefabs.Length) {
			return;
		}
		var asteroid = (GameObject)Instantiate (AsteroidPrefabs[size]);
		asteroid.transform.position = position;
	}

    public void SpawnHitParticle(Vector3 position)
    {
        var effect = (GameObject)Instantiate(HitParticlePrefab);
        effect.transform.position = position;
    }
}
