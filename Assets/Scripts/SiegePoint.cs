using UnityEngine;

public class SiegePoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy _enemy = other.gameObject.GetComponent<Enemy>();
            if (_enemy != null)
            {
                Gate _gateScript = GetComponentInParent<Gate>();
                if (_gateScript != null)
                {
                    if (!_gateScript._isDestroyed)
                    {
                        _enemy.StartSiege();                 
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
