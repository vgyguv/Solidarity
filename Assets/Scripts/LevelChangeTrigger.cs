using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    private GameManager _gameManager;
    public Color _normalColor = Color.white;
    public Color _activatedColor;
    private float _timeOnLevelChangeTrigger = 0f;
    private float _levelChangeTime = 1f;
    private Renderer _levelChangeTriggerRenderer;
    private Color32 _customBaseGreen = new Color32(107, 142, 35, 255); // R, G, B, A

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _levelChangeTriggerRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("OnTriggerStay+++++++++++++++++++++++++++++" + other.gameObject.name);

        if (other.CompareTag("Player") && _gameManager.GetClosedForestPathCount() == 12)
        {
            Player _playerScript = other.GetComponent<Player>();

            _timeOnLevelChangeTrigger += Time.deltaTime;

            float _pulse = Mathf.PingPong(Time.time * 2f, 1f); 
            _levelChangeTriggerRenderer.material.color = Color.Lerp(Color.white, Color.yellow, _pulse);

            if (_timeOnLevelChangeTrigger >= _levelChangeTime)
            {
                //Destroy(gameObject);
                _gameManager.EndGame();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit");

        if (other.CompareTag("Player"))
        {
            _timeOnLevelChangeTrigger = 0f;

            if (_levelChangeTriggerRenderer.material.color != _customBaseGreen)
            {
                _levelChangeTriggerRenderer.material.color = _customBaseGreen;
            }
        }
    }
}
