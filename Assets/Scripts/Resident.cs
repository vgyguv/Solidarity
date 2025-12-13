using UnityEngine;

public class Resident : MonoBehaviour
{
    [SerializeField]
    private float _bounceSpeed = 0.05f;

    private float _sinkSpeed = 0.01f;
    private string _mood = "Wary";
    private float _initialPositionX = 0f;
    private float _initialPositionY = 0f;
    private float _initialPositionZ = 0f;
    private float _lowerNegativeZ;
    private float _upperNegativeZ;
    private float _bounceOffsetZ = 0.5f;
    //private float _shiverOffsetX = 0.05f;
    private float _shiverOffsetX = 0.05f;
    private float _restingPlaceZ = 1.0f;
    private GameManager _gameManager;
    public Gate _gate;
    public SiegePoint _siegePoint;
    public GameObject _energyTokenPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _initialPositionX = transform.position.x;
        _initialPositionY = transform.position.y;
        _initialPositionZ = transform.position.z;
        // The Z axis is negative
        _lowerNegativeZ = _initialPositionZ;
        _upperNegativeZ = _initialPositionZ - _bounceOffsetZ;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        //Debug.Log("MOOD:" + _mood + " X:" + _initialPositionX);
        // Move Enemy along Y axis at a constant speed
 
        if (_mood == "Afraid" && transform.position.z <= _initialPositionZ)
        {
            transform.position = new Vector3(_initialPositionX + _shiverOffsetX, _initialPositionY, _initialPositionZ);
            _shiverOffsetX = _shiverOffsetX * -1;   
        }
        else if (_mood == "Happy" && transform.position.x == _initialPositionX)
        {            
            //transform.Translate(Vector3.forward * _bounceSpeed * Time.deltaTime);
            float _currentPositionZ = transform.position.z - _bounceSpeed;
            transform.position = new Vector3(_initialPositionX, _initialPositionY, _currentPositionZ);
    
            // Z Axis is negative
            if (transform.position.z > _lowerNegativeZ || transform.position.z < _upperNegativeZ)
            {
                // Reverse direction
                _bounceSpeed = _bounceSpeed * -1;
            }
        }
        else if (_mood == "Destroyed")
        {            
            if (transform.position.z < _restingPlaceZ)
            {
                float _currentPositionZ = transform.position.z + _sinkSpeed;
                
                transform.position = new Vector3(_initialPositionX, _initialPositionY, _currentPositionZ);
            }
        }
        //Case where _mood is no longer Afraid - return shiver to resting position
        else if (_mood != "Afraid" && transform.position.x != _initialPositionX)
        {
            Vector3 _position = transform.position;
            _position.x = _initialPositionX;
            transform.position = _position;                
        }
        //Case where _mood is no longer Happy - return bounce to resting position
        else if (_mood != "Happy" && _mood != "Destroyed" && transform.position.z != _initialPositionZ)
        {
            if (transform.position.z < transform.position.z - _initialPositionZ)
            {
                Vector3 _position = transform.position;
                _position.z = _initialPositionZ;
                transform.position = _position;                
            }
            else if (transform.position.z > transform.position.z + _initialPositionZ){
                // Reverse direction
                _bounceSpeed = _bounceSpeed * -1;
            }            
            else if (transform.position.z > _initialPositionZ)
            {
                transform.Translate(Vector3.forward * _bounceSpeed * Time.deltaTime);
            }
        }

        // Ensure _bounceSpeed is positive when not in use.
        if (_mood != "Happy" && _bounceSpeed < 0)
        {
            _bounceSpeed = _bounceSpeed * -1;
        }
    }

    public void SetMoodAfraid()
    {
        _mood = "Afraid";        
    }

    public void SetMoodWary()
    {
        _mood = "Wary";        
    }

    public void SetMoodRelaxed()
    {
        _mood = "Relaxed";        
    }

    public void SetMoodHappy()
    {
        _mood = "Happy";        
         Invoke(nameof(SpawnEnergyToken), 30f);
    }

    public void SetMoodDestroyed()
    {
        _mood = "Destroyed";        
    }

    void SpawnEnergyToken()
    {
        if (_mood == "Happy")
        {
            _gate.GetComponent<Renderer>().material.color = Color.white;
            float _offset = -0.2f;
            //Vector3 _gatePosition = _gate.transform.position;
            Vector3 _siegePointPosition = _siegePoint.transform.position;
            float _energyTokenPositionZ = _siegePointPosition.z + _offset; 
            Vector3 _spawnPos = new Vector3(_siegePointPosition.x, _siegePointPosition.y, _energyTokenPositionZ);

            GameObject _localEnergyToken = Instantiate(_energyTokenPrefab, _spawnPos, Quaternion.identity);
            
            // Create a link between the EnergyToken and the currect Resident
            _localEnergyToken.GetComponent<EnergyToken>().SetLocalResidentLink(this.gameObject);
        }
    }
}
