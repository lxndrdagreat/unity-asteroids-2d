using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private int m_Level = 1;
	public int Level { get { return m_Level; } }

	private bool m_GameActive = true;
	public bool GameActive { get { return m_GameActive; } }

    public GameObject PlayerPrefab;
    private GameObject _playerShip = null;

    public GameObject WarpfieldPrefab;
    private GameObject _warpField = null;

    [SerializeField]
    [Tooltip("How long to play Warpfield between levels.")]
    private float _WarpTime = 1.0f;
    private float _warpTimer = 0.0f;
    private bool _warping = false;

    [Header("Asteroids")]
	public GameObject[] AsteroidPrefabs;

    public GameObject HitParticlePrefab;

    private List<GameObject> _asteroids;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
			return;
		}

        _asteroids = new List<GameObject>();
		SpawnAsteroids ();        
	}

	// Use this for initialization
	void Start () {
        SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if (_warping)
        {
            _warpTimer -= Time.deltaTime;
            if (_warpTimer <= 0.0f)
            {
                _warping = false;
                Destroy(_warpField);
                SpawnAsteroids();
                SpawnPlayer();                
            }
        }
	}

    void SpawnPlayer()
    {
        if (_playerShip == null)
        {
            _playerShip = (GameObject)Instantiate(PlayerPrefab);            
        }
        _playerShip.SetActive(true);
        _playerShip.transform.position = Vector2.zero;
    }

    void DespawnPlayer()
    {
        if (_playerShip == null)
        {
            return;
        }
        _playerShip.SetActive(false);
    }

	void SpawnAsteroids() {
		for (var i = 0; i < 5 + m_Level; ++i) {			
			var center = Camera.main.rect.center;
			float angle = Random.Range (0.0f, 360.0f);
			var offset = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)) * 7.0f;
			SpawnAsteroid (0, center + offset);
		}
	}

	public void SpawnAsteroid(int size, Vector2 position, int count = 1){
		if (size >= AsteroidPrefabs.Length) {
			return;
		}
		var asteroid = (GameObject)Instantiate (AsteroidPrefabs[size]);
	    asteroid.GetComponent<AsteroidComponent>().Initialize(size);
		asteroid.transform.position = position;
        _asteroids.Add(asteroid);
	}

    public void DestroyAsteroid(AsteroidComponent asteroid)
    {
        SpawnAsteroid(asteroid.GetAsteroidSize()+1, asteroid.transform.position, 2);
        _asteroids.Remove(asteroid.gameObject);
        Destroy(asteroid.gameObject);

        if (_asteroids.Count == 0)
        {
            // finished the level
            print("Level complete. Starting next level.");
            DespawnPlayer();
            _warpField = (GameObject)Instantiate(WarpfieldPrefab);
            _warpTimer = _WarpTime;
            _warping = true;
            m_Level += 1;
        }
    }

    public void SpawnHitParticle(Vector3 position)
    {
        var effect = (GameObject)Instantiate(HitParticlePrefab);
        effect.transform.position = position;
    }
}
