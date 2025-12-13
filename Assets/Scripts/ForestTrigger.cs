using UnityEngine;

public class ForestTrigger : MonoBehaviour
{
    private GameManager _gameManager;
    public Color _normalColor = Color.white;
    public Color _activatedColor;
    private float _timeOnForestTrigger = 0f;
    private float _initialActivationTime = 1f;
    private float _forestClosureTime = 5f;
    private int _energyCost = -1000;
    private Renderer _forestTriggerRenderer;
    private Color32 _customBaseGreen = new Color32(107, 142, 35, 255); // R, G, B, A

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _forestTriggerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay+++++++++++++++++++++++++++++" + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Player _playerScript = other.GetComponent<Player>();

            _timeOnForestTrigger += Time.deltaTime;
            bool _forestPathClosed = _gameManager.CheckForestPathClosed(transform.position.x, transform.position.y);    
            if (_timeOnForestTrigger >= _initialActivationTime && !_forestPathClosed)
            {
                Debug.Log("IIIIIIIIIIIIIIII Inside first condition");
                float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
                _forestTriggerRenderer.material.color = Color.Lerp(Color.white, Color.green, _pulse);

                if (_timeOnForestTrigger >= _forestClosureTime)
                {
                    Debug.Log("IIIIIIIIIIIIIIII Inside second condition");
                    _gameManager.CloseForestPath(transform.position.x, transform.position.y);
                    _playerScript.UpdateEnergyLevel(_energyCost);            
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");

        if (other.CompareTag("Player"))
        {
            _timeOnForestTrigger = 0f;

            if (_forestTriggerRenderer.material.color != _customBaseGreen)
            {
                _forestTriggerRenderer.material.color = _customBaseGreen;
            }
        }
    }
}
