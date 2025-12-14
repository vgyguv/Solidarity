using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField]
    private int _energy = 5000; // starting energy
    [SerializeField]
    private float _speed = 3.5f;
    private GameManager _gameManager;
    private UIManager _uiManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _uiManager.UpdateEnergy(_energy);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = new Vector3(0f,-4f,-0f);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    public int GetEnergyLevel()
    {
        //Debug.Log("Player.GetEnergyLevel()");
        return _energy;
    }

    public void UpdateEnergyLevel(int amount)
    {
        //Debug.Log("Player.ReduceEnergy()");
        _energy += amount;

        if (_energy <= 0)
        {
            _energy = 0;
            _uiManager.UpdateEnergy(_energy);
            Destroy(gameObject);
            //Debug.Log("Player.ReduceEnergy() - player destroyed");
            //GameManager gameManager = FindObjectOfType<GameManager>();
            _gameManager.EndGame();
        }
        else
        {
            _uiManager.UpdateEnergy(_energy);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput,0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // following statement fixes the y boundary - like the if statement below
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 6), 0f);        

        if (transform.position.x >= 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0f);
        }
        else if (transform.position.x <= -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0f);
        }
        
    }
}
