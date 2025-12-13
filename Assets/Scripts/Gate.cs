using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] 
    private GameManager _gameManager;
    public GameObject _settlementCube;
    public Resident _resident;
    public Color _normalColor = Color.white;
    public Color _activatedColor;
    private float _buildTime = 3f;
    private float _fortifyTime = 4f;
    private float _breachTime = 10f;
    private float _invasionTime = 5f;
    private float _destructionTime = 5f;
    private int _energyCost = -100;
    private Renderer _settlementRenderer;
    private float _timeOnGate = 0f;
    private bool _isDeveloped = false;
    private bool _isFortified = false;
    public bool _isDestroyed = false;
    private Color32 _customBaseGreen = new Color32(107, 142, 35, 255); // R, G, B, A
    private Renderer _gateRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gateRenderer = GetComponent<Renderer>();
        
        Transform parent = transform.parent;
        foreach (Transform child in parent)
        {
            if (child.name == "Settlement" && child != transform)
            {
                _settlementCube = child.gameObject;
                break;
            }
        }
        _settlementRenderer = _settlementCube.GetComponent<Renderer>();
        if (_settlementRenderer != null)
        {
            _settlementRenderer.material.color = _normalColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._enemyCount > 0 && !_isDeveloped && !_isFortified && !_isDestroyed)
        {
            _resident.SetMoodAfraid();            
        }
        else if (_gameManager._enemyCount == 0 && !_isDeveloped && !_isFortified && !_isDestroyed)
        {
            _resident.SetMoodWary();                        
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        //if (other.gameObject.name == "Enemy")
        if (other.CompareTag("Enemy"))
        {
            if (!_isDestroyed)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.StartSiege(); 
            }
        }        
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay+++++++++++++++++++++++++++++" + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Player _playerScript = other.GetComponent<Player>();

            if (!_isDestroyed && !_isDeveloped)
            {
                _timeOnGate += Time.deltaTime;
                
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                _gateRenderer.material.color = Color.Lerp(Color.white, Color.green, _pulse);

                if (_timeOnGate >= _buildTime)
                {
                    _activatedColor = Color.green;
                    AlterSettlementState();
                    _isDeveloped = true;     
                    _resident.SetMoodRelaxed();
                    _playerScript.UpdateEnergyLevel(_energyCost);            
                }
            }
            else if (_isDeveloped && !_isFortified)
            {
                _timeOnGate += Time.deltaTime;
                
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                _gateRenderer.material.color = Color.Lerp(Color.white, Color.yellow, _pulse);

                if (_timeOnGate >= _fortifyTime)
                {
                    _activatedColor = Color.yellow;
                    AlterSettlementState();
                    _isFortified = true;     
                    _resident.SetMoodHappy();
                    _playerScript.UpdateEnergyLevel(_energyCost);            
                }
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (!_isDestroyed && !_isDeveloped)
            {
                _timeOnGate += Time.deltaTime;
                
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                //_gateRenderer.material.color = Color.Lerp(Color.white, Color.grey, _pulse);
                _gateRenderer.material.color = Color.Lerp(_customBaseGreen, Color.grey, _pulse);

                if (_timeOnGate >= _destructionTime)
                {
                    //_activatedColor = Color.grey;
                    _activatedColor = _customBaseGreen;
                    AlterSettlementState();
                    _isDestroyed = true;     
                    _resident.SetMoodDestroyed();
        
                    Enemy enemy = other.gameObject.GetComponent<Enemy>();
                    enemy.EndSiege(); 
                }
            }
            else if (!_isDestroyed && _isDeveloped && !_isFortified)
            {
                _timeOnGate += Time.deltaTime;
                
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                _gateRenderer.material.color = Color.Lerp(Color.white, Color.green, _pulse);

                if (_timeOnGate >= _invasionTime)
                {
                    _activatedColor = Color.white;
                    AlterSettlementState();
                    _isDeveloped = false;     
                    _resident.SetMoodAfraid();
                }
            }
            else if (!_isDestroyed && _isFortified)
            {
                _timeOnGate += Time.deltaTime;
                
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                _gateRenderer.material.color = Color.Lerp(Color.white, Color.yellow, _pulse);

                if (_timeOnGate >= _breachTime)
                {
                    _activatedColor = Color.green;
                    AlterSettlementState();
                    _isFortified = false;   
                    _resident.SetMoodWary();  
                }
            }

        }
    }
        
    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.EndSiege(); 
        }        

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            _timeOnGate = 0f;
            if (_gateRenderer.material.color != Color.grey)
            {
                _gateRenderer.material.color = Color.grey;
            }
        }
    }
        
    void AlterSettlementState()
    {
        Debug.Log("AlterSettlementState");
        Vector3 _position = _settlementCube.transform.position;

        _settlementRenderer.material.color = _activatedColor;
        
        if (_gateRenderer != null)
        {
            _gateRenderer.material.color = Color.white;
        }

        // Set the hight of the Settlement based on the color

        if (_activatedColor == Color.grey)
        {
            _position.z = 0.09f;            
        }
        else if (_activatedColor == Color.white)
        {
            _position.z = 0.07f;            
        }
        else if (_activatedColor == Color.green)
        {
            _position.z = 0.05f;            
        }
        else if (_activatedColor == Color.yellow)
        {
            _position.z = 0.03f;            
        }
        _settlementCube.transform.position = _position;

        _timeOnGate = 0f;    
    }
}
