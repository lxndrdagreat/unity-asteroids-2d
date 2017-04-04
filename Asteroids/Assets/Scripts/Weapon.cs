using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string objectName = "New Weapon";
    public float RateOfFire = 5.0f;   
    private float _fireTimer = 0.0f;
    [SerializeField]
    private GameObject _BulletPrefab;

    public void Update()
    {
        _fireTimer -= Time.deltaTime;
    }

    public bool CanFire()
    {
        return _fireTimer <= 0.0f;
    }

    public void Fire(Vector2 position, Quaternion rotation)
    {
        if (!CanFire())
        {
            return;
        }
        _fireTimer = 1.0f / RateOfFire;
        var bullet = (GameObject)Instantiate(_BulletPrefab);
        bullet.transform.position = position;
        bullet.GetComponent<BulletComponent>().Init(position, rotation);
    }
}