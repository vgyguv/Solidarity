using UnityEngine;

public class Assassin : MonoBehaviour
{
    private int _damage = -1500; 

    private float _speed = 3.5f;
    public string _localTravelAxis;
    public int _localTravelDirection;
    public bool _siege = false;
    private float[] _xJunctionPoints = { -9f, -6f, -3f, 0f, 3f, 6f, 9f };
    private float[] _yJunctionPoints = { -4f, -1f, 2f, 5f };
    private bool _passingJunction = true; // spawning on junction
    private GameManager _gameManager;
    [SerializeField] 
    private Player _player;
    private int _positionRounded;
    private float _playerX;
    private float _playerY;
    private int _newDirection = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._gameOver)
        {
            Destroy(gameObject);
        }
        else if (!_siege)
        {
            CalculateMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            Player _playerScript = other.GetComponent<Player>();
            if (_playerScript != null)
            {
                _playerScript.UpdateEnergyLevel(_damage);
            }

            _gameManager.AddScore(5000); // award points for defeating enemy
            _gameManager.OnAssassinDestroyed(); // respawn Enemy
        }
    }

    private void CalculateMovement()
    {
        // Move Enemy along Y axis at a constant speed
        //transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //float _travelSpeed = _speed * _gameManager._travelDirection;
        float _travelSpeed = _speed * _localTravelDirection;
        float _min = 0f;
        float _max = 0f;
        float _clampedX = 0f;
        float _clampedY = 0f;

        DeterminePlayerPosition();

        if (_localTravelAxis == "X-Axis" && transform.position.y == _playerY)
        {
            // Assassin currently travelling on X axis and is on same Y axis as Player
            // Just check that X axis travel is correct in relation to Player.

            //Debug.Log("travelX-AXIS ????????????????????????????????????? " + transform.position.x + " " + _playerX + " " + _localTravelDirection +
            //          " -- speed:" + _speed + " travel_speed:" + _travelSpeed);

            if ((transform.position.x < _playerX && _localTravelDirection < 0) || (transform.position.x > _playerX && _localTravelDirection > 0))
            {
                _localTravelDirection = -1;
                _travelSpeed = _speed * _localTravelDirection;
            }

            if (transform.position.x > _playerX)
            {
                transform.Translate(Vector3.right * _travelSpeed * Time.deltaTime);                    
            }
            else
            {
                transform.Translate(Vector3.left * _travelSpeed * Time.deltaTime);                    
            }
        }
        else if (_localTravelAxis == "Y-Axis" && transform.position.x == _playerX)
        {
            // Assassin currently travelling on Y axis and is on same X axis as Player
            // Just check that Y axis travel is correct in relation to Player.

            if (transform.position.y < _playerY && _localTravelDirection < 0)
            {
                _localTravelDirection = 1;
                _travelSpeed = _speed * _localTravelDirection;
            }

            transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);
        }
        else if (_localTravelAxis == "Y-Axis" && !_passingJunction)
        {
            // TRAVELLING ON Y-AXIS
            // Check if crossing a path junction with the x axis
            _newDirection = 0;

            if (_travelSpeed < 0) 
            {
                _min = transform.position.y + -0.02f;
                _max = transform.position.y;
            }
            else 
            {
                _min = transform.position.y;
                _max = transform.position.y + 0.02f;
            }


            for (int i = 0; i < _yJunctionPoints.Length; i++)
            {
                _clampedY = Mathf.Clamp(_yJunctionPoints[i], _min, _max);
                //Debug.Log("_min" + _min + " _max " + _max + " _clampedY " + _clampedY);

                if (_clampedY == _yJunctionPoints[i])
                {
                    if (_clampedY == _playerY)
                    {
                        // Player position is matching on y axis
                        ChasePlayerOnXAxis();
                    }
                    else
                    {
                        RandomXAxisDecision();
                    }
                    break;                    
                }  
            }

            if (_newDirection != 0)
            {
                // direction has changed - update _travelDirection
                SetLocalTravelDirection(_newDirection);
                // Set Y axis value to Y junction value to be exactly on the Y path
                Vector3 _position = transform.position;
                _position.y = _clampedY;
                transform.position = _position;
                // prevent evaluation of junction on next iteration as value may still be in range
                _passingJunction = true; 
            }
            else
            {
                // direction has not changed - continue of y axis.
                transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);

                if ((transform.position.y < -4 && _localTravelDirection < 0) || (transform.position.y > 6 && _localTravelDirection > 0))                {
                    // Reverse direction
                    SwitchLocalTravelDirection();
                }  
            }
        }
        else if (_localTravelAxis == "X-Axis" && !_passingJunction)
        {
            // TRAVELLING ON X-AXIS
            // Check if crossing a path junction with the y axis
            _newDirection = 0;

            if (_travelSpeed < 0) 
            {
                _min = transform.position.x + -0.02f;
                _max = transform.position.x;
            }
            else 
            {
                _min = transform.position.x;
                _max = transform.position.x + 0.02f;
            }

            for (int i = 0; i < _xJunctionPoints.Length; i++)
            {
                _clampedX = Mathf.Clamp(_xJunctionPoints[i], _min, _max);
                Debug.Log("_min" + _min + " _max " + _max + " _clampedX " + _clampedX);
            
                if (_clampedX == _xJunctionPoints[i])
                {
                    if (_clampedX == _playerX)
                    {
                        // Player position is matching on x axis
                        ChasePlayerOnYAxis();
                    }
                    else
                    {
                        RandomYAxisDecision();
                    }     
                    break;               
                }    
            }

            if (_newDirection != 0)
            {
                // direction has changed - update _travelDirection
                SetLocalTravelDirection(_newDirection);

                // Set X axis value to X junction value to be exactly on the X path 
                Vector3 _position = transform.position;
                _position.x = _clampedX;
                transform.position = _position;
                //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Setting psotition: X:" + _position.x + " / Y:"  + _position.y + " DIR:" + _newDirection);
                
                // prevent evaluation of junction on next iteration as value may still be in range
                _passingJunction = true; 
            }
            else
            {
                // direction has not changed - continue on x axis.
                transform.Translate(Vector3.left * _travelSpeed * Time.deltaTime);
        
                if ((transform.position.x < -9 && _localTravelDirection > 0) || (transform.position.x > 9 && _localTravelDirection < 0))
                {
                    // Reverse direction
                    SwitchLocalTravelDirection();
                }
            }
        } 
        else if (_localTravelAxis == "Y-Axis" && _passingJunction)
        {
            _passingJunction = false;
            // direction has not changed - continue of y axis.
            transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);

            if ((transform.position.y < -4 && _localTravelDirection < 0) || (transform.position.y > 6 && _localTravelDirection > 0))
            {
                // Reverse direction
                SwitchLocalTravelDirection();
            }                
        } 
        else if (_localTravelAxis == "X-Axis" && _passingJunction)
        {
            _passingJunction = false;
            // direction has not changed - continue on x axis.
            transform.Translate(Vector3.left * _travelSpeed * Time.deltaTime);
    
            if ((transform.position.x < -9 && _localTravelDirection > 0) || (transform.position.x > 9 && _localTravelDirection < 0))
            {
                // Reverse direction
                SwitchLocalTravelDirection();
            }
            _passingJunction = false;
        }
    }

    private void DeterminePlayerPosition()
    {
        //Debug.Log("DeterminePlayerPosition >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        // Round the Player x and y positions to integers and convert back to float values.
        _positionRounded = Mathf.RoundToInt(_player.transform.position.x);
        _playerX = _positionRounded;
        _positionRounded = Mathf.RoundToInt(_player.transform.position.y);
        _playerY = _positionRounded;
    }

    private void ChasePlayerOnXAxis()
    {
        //Debug.Log("ChasePlayerOnXAxis >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        // Encountered a junction on y-Axis and switching to travel on same x-Axis as Player
        SwitchLocalTravelAxis();

        if (transform.position.x < _playerX)
        {
            _newDirection = -1; // travel right
        }
        else 
        {
            _newDirection = 1; // travel left
        }
    }

    private void ChasePlayerOnYAxis()
    {
        //Debug.Log("ChasePlayerOnYAxis >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        // Encountered a junction on x-Axis and switching to travel on same y-Axis as Player
        SwitchLocalTravelAxis();

        if (transform.position.y < _playerY)
        {
            _newDirection = 1; // travel up
        }
        else 
        {
            _newDirection = -1; // travel down
        }
    }

    private void RandomXAxisDecision()
    {
        //Debug.Log("RandomXAxisDecision >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        
        if (Random.Range(0, 2) == 1)                   
        {
            // Encountered a junction on y-Axis and switching to travel on x-Axis
            //_gameManager.SwitchTravelAxis();
            SwitchLocalTravelAxis();
            // Check current X path and decide on travel direction - left/right
            if (transform.position.x == -9f)
            {
                _newDirection = 1; // travel right
            }
            else if (transform.position.x == 9f)
            {
                _newDirection = -1; // travel left
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    _newDirection = 1; // travel right
                }
                else
                {
                    _newDirection = -1; // travel left
                }
            }
        }
    }    

    private void RandomYAxisDecision()
    {
        //Debug.Log("RandomYAxisDecision >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        
        if (Random.Range(0, 2) == 1)
        {
            // Encountered a junction on x-Axis and switching to travel on y-Axis
            //_gameManager.SwitchTravelAxis();
            SwitchLocalTravelAxis();
            // Check current Y path and decide on travel direction - up/down
            if (transform.position.y == -4)
            {
                _newDirection = 1; // travel up
            }
            else if (transform.position.y == 5)
            {
                _newDirection = -1; // travel down
            }
            else
            {
                if (Random.Range(0, 2) == 0)
                {
                    _newDirection = 1; // travel up
                }
                else
                {
                    _newDirection = -1; // travel down
                }
            }
        }
    }


    public void UpdateTravelInfo(string travelAxis, int travelDirection)
    {
        _localTravelAxis = travelAxis;
        _localTravelDirection = travelDirection;
    }

    public void SwitchLocalTravelAxis()
    {
        //Debug.Log("SwitchLocalTravelAxis");
        // Respawn after 2 - 5 seconds
        if(_localTravelAxis == "X-Axis")
        {
            _localTravelAxis = "Y-Axis";
        }
        else
        {
            _localTravelAxis = "X-Axis";
        }
    }

    public void SetLocalTravelDirection(int newDirection)
    {
        //Debug.Log("SetlocalTravelDirection");
        _localTravelDirection = newDirection;
    }
    public void SwitchLocalTravelDirection()
    {
        //Debug.Log("SwitchlocalTravelDirection");
        _localTravelDirection = _localTravelDirection * -1;
    }
}
