using UnityEngine;

public class EnergyToken : MonoBehaviour
{
    private Resident _localResident;
    int _energyReward = 1000;
    // Speed of rotation
    public float rotationSpeed = 500f;
    private Vector3 diagonalAxis = new Vector3(1f, 1f, 0f).normalized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Energy::Start()");   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Energy::Update()");   
        // Rotate around the diagonal axis
        transform.Rotate(diagonalAxis, rotationSpeed * Time.deltaTime);        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnergyToken::OnTriggerEnter() ??????????????????????????????????????????????????????????" + other.gameObject.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("EnergyToken::OnTriggerEnter() - GOT PLAYER");
            Player _playerScript = other.GetComponent<Player>();
            _playerScript.UpdateEnergyLevel(_energyReward);            
            Destroy(gameObject);
            
            // Trigger the respawn of the EnergyToken after 45 seconds
            if (_localResident != null)
            {
                _localResident.Invoke("SpawnEnergyToken", 45f);
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            // Trigger the respawn of the EnergyToken after 60 seconds
            if (_localResident != null)
            {
                _localResident.Invoke("SpawnEnergyToken", 60f);
            }
        }

    }

    public void SetLocalResidentLink(GameObject resident)
    {
        _localResident = resident.GetComponent<Resident>();
    }
}
