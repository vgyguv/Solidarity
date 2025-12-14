using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _gameOver = false;
    public int _score = 0;
    public int _enemyCount = 0;
    public string _travelAxis;
    public int _travelDirection = -1;
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField] 
    private GameObject _assassinPrefab;
    [SerializeField] 
    private GameObject _neighbourPrefab;
    [SerializeField] 
    private GameObject _mountainPrefab;
    [SerializeField] 
    private GameObject _forestTriggerPrefab;
    private GameObject _currentOpponent;
    private UIManager _uiManager;
    private string _opponentType;

    // Northern border
    private GameObject _pathNorth_E9_N7;
    //private bool _spawnPointBlocked_E9_N7 = false;
    private GameObject _pathNorth_E6_N7;
    //private bool _spawnPointBlocked_E6_N7 = false;
    private GameObject _pathNorth_E3_N7;
    //private bool _spawnPointBlocked_E3_N7 = false;
    private GameObject _pathNorth_W3_N7;
    //private bool _spawnPointBlocked_W3_N7 = false;
    private GameObject _pathNorth_W6_N7;
    //private bool _spawnPointBlocked_W6_N7 = false;
    private GameObject _pathNorth_W9_N7;
    //private bool _spawnPointBlocked_W9_N7 = false;
    // Eastern border
    private GameObject _pathEast_E9_N2;        
    //private bool _spawnPointBlocked_E9_N2 = false;
    private GameObject _pathEast_E9_S1;        
    //private bool _spawnPointBlocked_E9_S1 = false;
    private GameObject _pathEast_E9_S4;        
    //private bool _spawnPointBlocked_E9_S4 = false;
    // Western border
    private GameObject _pathWest_W9_N2;
    //private bool _spawnPointBlocked_W9_N2 = false;
    private GameObject _pathWest_W9_S1;
    //private bool _spawnPointBlocked_W9_S1 = false;
    private GameObject _pathWest_W9_S4;
    //private bool _spawnPointBlocked_W9_S4 = false;

    void Start()
    {
        //Debug.Log("GameManager.Start >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        Vector3 _spawnPos;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // LEVEL LAYOUT - NEIGHBOUR AND MOUNTAIN OBJECTS
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // T O P   B O R D E R

        _spawnPos = new Vector3(10.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(7.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(4.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(1.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-1.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-4.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-7.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-10.5f, 6.3f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // T O P   R I G H T   B L O C K E R

        _spawnPos = new Vector3(10.5f, 5.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // X 6.9244 / Y -0.66478
        // T O P   R O W 

        _spawnPos = new Vector3(10.5f, 3.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 6)
        _spawnPos = new Vector3(6.8f, 5.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 5)
        _spawnPos = new Vector3(3.8f, 5.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 4)
        _spawnPos = new Vector3(0.8f, 5.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 3)
        _spawnPos = new Vector3(-1.5f, 3.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        ////_spawnPos = new Vector3(-2.2f, 5.3f, 0f);
        ////Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 2)
        _spawnPos = new Vector3(-5.2f, 5.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 1, col 1)
        _spawnPos = new Vector3(-8.2f, 5.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
                                   
        _spawnPos = new Vector3(-10.5f, 3.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // T O P   L E F T   B L O C K E R

        _spawnPos = new Vector3(-10.5f, 5.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // M I D D L E   R O W

        _spawnPos = new Vector3(10.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 6)
        _spawnPos = new Vector3(6.8f, 2.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 5)
        _spawnPos = new Vector3(4.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        ////_spawnPos = new Vector3(3.8f, 2.3f, 0f);
        ////Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 4)
        _spawnPos = new Vector3(1.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        ////_spawnPos = new Vector3(0.8f, 2.3f, 0f);
        ////Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 3)
        _spawnPos = new Vector3(-1.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        ////_spawnPos = new Vector3(-2.2f, 2.3f, 0f);
        ////Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 2)
        _spawnPos = new Vector3(-5.2f, 2.3f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 2, col 1)
        //_spawnPos = new Vector3(-8.2f, 2.3f, 0f);
        //Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        _spawnPos = new Vector3(-7.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-10.5f, 0.45f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // B O T T O M   R O W

        _spawnPos = new Vector3(10.5f, -2.55f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 6)
        _spawnPos = new Vector3(6.8f, -0.7f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 5)
        _spawnPos = new Vector3(3.8f, -0.7f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 4)
        _spawnPos = new Vector3(0.8f, -0.7f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 3)
        _spawnPos = new Vector3(-1.5f, -2.55f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        ////_spawnPos = new Vector3(-2.2f, -0.7f, 0f);
        ////Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 2)
        _spawnPos = new Vector3(-5.2f, -0.7f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);
        // Grid (row 3, col 1)
        _spawnPos = new Vector3(-8.2f, -0.7f, 0f);
        Instantiate(_neighbourPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-10.5f, -2.55f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        // B O T T O M   B O R D E R

        _spawnPos = new Vector3(10.5f, -5.35f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        _spawnPos = new Vector3(-10.5f, -5.35f, 0f);
        Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);

        ////////////////////////////////////////////////////////////////////////////////////
        // F O R E S T   T R I G G E R S
        ////////////////////////////////////////////////////////////////////////////////////
        // Northern border

        _spawnPos = new Vector3(-9f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(-6f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(-3f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(3f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(6f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(9f, 5f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        // Eastern border (Positive X)

        _spawnPos = new Vector3(-9f, 2f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(-9f, -1f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(-9f, -4f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        // Western border (Negative X)

        _spawnPos = new Vector3(9f, 2f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(9f, -1f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        _spawnPos = new Vector3(9f, -4f, 0.07f);
        Instantiate(_forestTriggerPrefab, _spawnPos, Quaternion.Euler(90f, 0f, 0f));

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // F O R E S T   P A T H   C L O S U R E  O B J E C T S
        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Northern border

        _spawnPos = new Vector3(9.0f, 6.5f, 0f);
        _pathNorth_E9_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_E9_N7.SetActive(false);

        _spawnPos = new Vector3(6.0f, 6.5f, 0f);
        _pathNorth_E6_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_E6_N7.SetActive(false);

        _spawnPos = new Vector3(3.0f, 6.5f, 0f);
        _pathNorth_E3_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_E3_N7.SetActive(false);

        // Center path north cannot be closed

        _spawnPos = new Vector3(-3.0f, 6.5f, 0f);
        _pathNorth_W3_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_W3_N7.SetActive(false);

        _spawnPos = new Vector3(-6.0f, 6.5f, 0f);
        _pathNorth_W6_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_W6_N7.SetActive(false);

        _spawnPos = new Vector3(-9f, 6.5f, 0f);
        _pathNorth_W9_N7 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathNorth_W9_N7.SetActive(false);

        // Eastern border (Positive X)

        _spawnPos = new Vector3(10.7f, 2.0f, 0f);
        _pathEast_E9_N2 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathEast_E9_N2.SetActive(false);
        
        _spawnPos = new Vector3(10.7f, -1.0f, 0f);
        _pathEast_E9_S1 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathEast_E9_S1.SetActive(false);

        _spawnPos = new Vector3(10.7f, -4.0f, 0f);
        _pathEast_E9_S4 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathEast_E9_S4.SetActive(false);

        // Western border (Negative X)

        _spawnPos = new Vector3(-10.7f, 2.0f, 0f);
        _pathWest_W9_N2 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathWest_W9_N2.SetActive(false);

        _spawnPos = new Vector3(-10.7f, -1.0f, 0f);
        _pathWest_W9_S1 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathWest_W9_S1.SetActive(false);

        _spawnPos = new Vector3(-10.7f, -4.0f, 0f);
        _pathWest_W9_S4 = Instantiate(_mountainPrefab, _spawnPos, Quaternion.identity);
        _pathWest_W9_S4.SetActive(false);

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // S P A W N   O P P O N E N T S
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // Spawn first enemy after 5 - 10 seconds 
        Invoke(nameof(SpawnEnemy), Random.Range(5f, 10f));
        // Spawn secoond enemy after 150 - 200 seconds 
        Invoke(nameof(SpawnEnemy), Random.Range(150f, 200f));
        // Spawn secoond enemy after 600 - 720 seconds 
        Invoke(nameof(SpawnEnemy), Random.Range(420f, 450f));

        // Spawn assassin after 210 seconds
        Invoke(nameof(SpawnAssassin), 210f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver)
        {
            //SceneManager.LoadScene("Game");            
            SceneManager.LoadScene("MainMenu");            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Quit application if Escape key is pressed            
            Application.Quit();            
        }
    }

    void SpawnEnemy()
    {
        _opponentType = "Enemy";
        //Debug.Log("SpawnEnemy >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + _opponentType);
        SpawnOpponent();
    }

    void SpawnAssassin()
    {
        _opponentType = "Assassin";
        //Debug.Log("SpawnAssassin >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> " + _opponentType);
        SpawnOpponent();
    }

    void SpawnOpponent()
    {
        if (!_gameOver)
        {
            int _x;
            int _y;
            
            int _randomDecision = Random.Range(0, 2);
            // Decision: 0 - fixed x axis and random y axis 
            // Decision: 1 - random x axis, and fixed y axis, 

            if (_randomDecision == 1 || _opponentType == "Assassin")
            {

                _travelAxis = "Y-Axis"; // move along y axis
                _travelDirection = -1; // Enemy travels down
                _x = Random.Range(-3, 3) * 3;
                _y = 7;
                bool _blockedSpawnPoint = CheckSpawnPoint(_x, _y);

                if (_blockedSpawnPoint)
                {
                    // Default to central path on northern border
                    _x = 0;
                }
            } 
            else
            {

                _travelAxis = "X-Axis"; // move along x axis

                _y = Random.Range(-1, 1) * 3 -1;

                _randomDecision = Random.Range(0, 2);
                // Decision: 0 - x position on right
                // Decision: 1 - x position on left,  

                if (_randomDecision == 1)
                {
                    _x = -10;
                    _travelDirection = 1; // Enemy travels to the right
                }
                else
                {
                    _x = 10;
                    _travelDirection = -1; // Enemy travels to the left
                }
                bool _blockedSpawnPoint = CheckSpawnPoint(_x, _y);

                if (_blockedSpawnPoint)
                {
                    // Default to central path on northern border
                    _travelAxis = "Y-Axis"; // move along y axis
                    _travelDirection = -1; // Enemy travels down
                    _x = 0;
                    _y = 7;
                }
            }
        
            Vector3 spawnPos = new Vector3(_x, _y, 0);
            if (_opponentType == "Enemy")
            {
                //Debug.Log("E N E M Y !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1");
                // Spawn enemy
                _currentOpponent = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
                UpdateEnemyCount(1);               
                // Set the _travelAxis and _travelDirection locally in the Enemy gameObject
                _currentOpponent.GetComponent<Enemy>().UpdateTravelInfo(_travelAxis, _travelDirection);
            }
            else
            {
                //Debug.Log("A S S A S S I N !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1!");
                // Spawn assassin
                _currentOpponent = Instantiate(_assassinPrefab, spawnPos, Quaternion.identity);
                // Set the _travelAxis and _travelDirection locally in the assassin gameObject
                _currentOpponent.GetComponent<Assassin>().UpdateTravelInfo(_travelAxis, _travelDirection);                
            }
                        
        }
    }
    
    public void OnEnemyDestroyed()
    {
        //Debug.Log("OnEnemyDestroyed fired");
        // Respawn after 2 - 5 seconds
        Invoke(nameof(SpawnEnemy), Random.Range(2f, 5f));
    }

    public void OnAssassinDestroyed()
    {
        //Debug.Log("OnAssassinDestroyed fired");
        // Respawn after 2 minutes
        Invoke(nameof(SpawnAssassin), 10f);
    }

    public void AddScore(int amount)
    {
        _score += amount;
        _uiManager.UpdateScore(_score);
    }

    public void UpdateEnemyCount(int amount)
    {
        _enemyCount += amount;
    }

    public bool CheckSpawnPoint(float positionX, float positionY)
    {
        //Debug.Log("gameManager::CheckSpawnPoint(" + positionX + ", " + positionY + ") HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        // N O R T H E R N   B O R D E R
        if (positionX == 9.0f && positionY == 7.0f)
        {
            return _pathNorth_E9_N7.activeInHierarchy;
        } 
        else if (positionX == 6.0f && positionY == 7.0f)
        {
            return _pathNorth_E6_N7.activeInHierarchy;
        }
        else if (positionX == 3.0f && positionY == 7.0f)
        {
            return _pathNorth_E3_N7.activeInHierarchy;
        }
        if (positionX == -3.0f && positionY == 7.0f)
        {
            return _pathNorth_W3_N7.activeInHierarchy;
        } 
        else if (positionX == -6.0f && positionY == 7.0f)
        {
            return _pathNorth_W6_N7.activeInHierarchy;
        }
        else if (positionX == -9.0f && positionY == 7.0f)
        {
            return _pathNorth_W9_N7.activeInHierarchy;
        }
        // EASTERN BORDER (Positive X)
        else if (positionX >= 9.0f && positionY == 2.0f)
        {
            return _pathEast_E9_N2.activeInHierarchy;
        }
        else if (positionX >= 9.0f && positionY == -1.0f)
        {
            return _pathEast_E9_S1.activeInHierarchy;
        }
        else if (positionX >= 9.0f && positionY == -4.0f)
        {
            return _pathEast_E9_S4.activeInHierarchy;
        }
        // WESTERN BORDER (Negative X)
        else if (positionX <= -9.0f && positionY == 2.0f)
        {
            return _pathWest_W9_N2.activeInHierarchy;
        }
        else if (positionX <= -9.0f && positionY == -1.0f)
        {
            return _pathWest_W9_S1.activeInHierarchy;
        }
        else if (positionX <= -9.0f && positionY == -4.0f)
        {
            return _pathWest_W9_S4.activeInHierarchy;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForestPathClosed(float positionX, float positionY)
    {
        //Debug.Log("gameManager::CheckForestPathOpen(" + positionX + ", " + positionY + ") HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
        // N O R T H E R N   B O R D E R
        if (positionX == 9.0f && positionY >= 5.0f)
        {
            return _pathNorth_E9_N7.activeInHierarchy;
        } 
        else if (positionX == 6.0f && positionY >= 5.0f)
        {
            return _pathNorth_E6_N7.activeInHierarchy;
        }
        else if (positionX == 3.0f && positionY >= 5.0f)
        {
            return _pathNorth_E3_N7.activeInHierarchy;
        }
        if (positionX == -3.0f && positionY >= 5.0f)
        {
            return _pathNorth_W3_N7.activeInHierarchy;
        } 
        else if (positionX == -6.0f && positionY >= 5.0f)
        {
            return _pathNorth_W6_N7.activeInHierarchy;
        }
        else if (positionX == -9.0f && positionY >= 5.0f)
        {
            return _pathNorth_W9_N7.activeInHierarchy;
        }
        // EASTERN BORDER (Positive X)
        else if (positionX >= 9.0f && positionY == 2.0f)
        {
            return _pathEast_E9_N2.activeInHierarchy;
        }
        else if (positionX >= 9.0f && positionY == -1.0f)
        {
            return _pathEast_E9_S1.activeInHierarchy;
        }
        else if (positionX >= 9.0f && positionY == -4.0f)
        {
            return _pathEast_E9_S4.activeInHierarchy;
        }
        // WESTERN BORDER (Negative X)
        else if (positionX <= -9.0f && positionY == 2.0f)
        {
            return _pathWest_W9_N2.activeInHierarchy;
        }
        else if (positionX <= -9.0f && positionY == -1.0f)
        {
            return _pathWest_W9_S1.activeInHierarchy;
        }
        else if (positionX <= -9.0f && positionY == -4.0f)
        {
            return _pathWest_W9_S4.activeInHierarchy;
        }
        else
        {
            return false;
        }
    }

    public void CloseForestPath(float positionX, float positionY)
    {
        //Debug.Log("CloseForestPath()");
        // N O R T H E R N   B O R D E R
        if (positionX == 9.0f && positionY >= 5.0f)
        {
            _pathNorth_E9_N7.SetActive(true);
            //_spawnPointBlocked_E9_N7 = true;
        } 
        else if (positionX == 6.0f && positionY >= 5.0f)
        {
            _pathNorth_E6_N7.SetActive(true);
            //_spawnPointBlocked_E6_N7 = true;
        }
        else if (positionX == 3.0f && positionY >= 5.0f)
        {
            _pathNorth_E3_N7.SetActive(true);
            //_spawnPointBlocked_E3_N7 = true;
        }
        if (positionX == -3.0f && positionY >= 5.0f)
        {
            _pathNorth_W3_N7.SetActive(true);
            //_spawnPointBlocked_W3_N7 = true;
        } 
        else if (positionX == -6.0f && positionY >= 5.0f)
        {
            _pathNorth_W6_N7.SetActive(true);
            //_spawnPointBlocked_W6_N7 = true;
        }
        else if (positionX == -9.0f && positionY >= 5.0f)
        {
            _pathNorth_W9_N7.SetActive(true);
            //_spawnPointBlocked_W9_N7 = true;
        }
        // EASTERN BORDER (Positive X)
        else if (positionX >= 9.0f && positionY == 2.0f)
        {
            _pathEast_E9_N2.SetActive(true);
            //_spawnPointBlocked_E9_N2 = true;
        }
        else if (positionX >= 9.0f && positionY == -1.0f)
        {
            _pathEast_E9_S1.SetActive(true);
            //_spawnPointBlocked_E9_S1 = true;
        }
        else if (positionX >= 9.0f && positionY == -4.0f)
        {
            _pathEast_E9_S4.SetActive(true);
            //_spawnPointBlocked_E9_S4 = true;
        }
        // WESTERN BORDER (Negative X)
        else if (positionX <= -9.0f && positionY == 2.0f)
        {
            _pathWest_W9_N2.SetActive(true);
            //_spawnPointBlocked_W9_N2 = true;
        }
        else if (positionX <= -9.0f && positionY == -1.0f)
        {
            _pathWest_W9_S1.SetActive(true);
            //_spawnPointBlocked_W9_S1 = true;        
        }
        else if (positionX <= -9.0f && positionY == -4.0f)
        {
            _pathWest_W9_S4.SetActive(true);    
            //_spawnPointBlocked_W9_S4 = true;
        }
    }

    public void EndGame()
    {
        //Debug.Log("GameManager.EndGame()");
        _gameOver = true;
        _uiManager.ShowGameOver();
        //Debug.Log("Game Over!");
        // Stop spawning opponents
        CancelInvoke();
    }
}
