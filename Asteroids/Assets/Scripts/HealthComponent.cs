using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TookDamageEvent(int damageTaken);
public delegate void DiedEvent();

public class HealthComponent : MonoBehaviour {

	[SerializeField]
	private int m_MaxHealth;

	private int m_Health;
	private bool m_IsAlive = true;

	public event TookDamageEvent TookDamage;
	public event DiedEvent Died;

	void Awake() {
		m_Health = m_MaxHealth;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetMaxHealth(int h){
		m_Health = m_MaxHealth = h;
	}

	public void TakeDamage(int amount) {
		if (!m_IsAlive) {
			return;
		}

		m_Health -= amount;
		if (TookDamage != null) {
			TookDamage (amount);
		}

		if (m_Health <= 0) {
			m_IsAlive = false;
			if (Died != null) {
				Died ();
			}
		}
	}
}
