using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticleComponent : MonoBehaviour {

    private ParticleSystem _particle;

    void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if (_particle.IsAlive() == false)
        {
            Destroy(gameObject);
        }
	}
}
