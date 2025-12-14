using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _damage = -300; 
    [SerializeField]
    private float _speed = 3.5f;
    public string _localTravelAxis;
    public int _localTravelDirection;
    public bool _siege = false;
    private float[] _xJunctionPoints = { -9f, -6f, -3f, 0f, 3f, 6f, 9f };
    private float[] _yJunctionPoints = { -4f, -1f, 2f, 5f };
    private bool _passingJunction = true; // spawning on junction
    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameManager = FindObjectOfType<GameManager>();
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
            _gameManager.UpdateEnemyCount(-1);
            Player _playerScript = other.GetComponent<Player>();
            if (_playerScript != null)
            {
                _playerScript.UpdateEnergyLevel(_damage);
            }

            _gameManager.AddScore(1000); // award points for defeating enemy
            _gameManager.OnEnemyDestroyed(); // respawn Enemy
        }
    }

    private void CalculateMovement()
    {
        // Move Enemy along Y axis at a constant speed
        float _travelSpeed = _speed * _localTravelDirection;
        int _newDirection = 0;
        float _min = 0f;
        float _max = 0f;
        float _clampedX = 0f;
        float _clampedY = 0f;

        if (_localTravelAxis == "Y-Axis" && !_passingJunction)
        {
            //Debug.Log("travelY-AXIS ----------------------------------------------------------------- Y-AXIS");
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
                    //Debug.Log("Got MATCH !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Y " + _clampedY);
                    if (Random.Range(0, 2) == 1)                   
                    {
                        // Encountered a junction on y-Axis and switching to travel on x-Axis
                        SwitchLocalTravelAxis();
                        // Check current X path and decide on travel direction - left/right
                        if (transform.position.x <= -9f)
                        {
                            _newDirection = 1; // travel right
                        }
                        else if (transform.position.x >= 9f)
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

                        break;
                    }
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

                //if ((transform.position.y < -4 && _gameManager._travelDirection < 0) || (transform.position.y > 6 && _gameManager._travelDirection > 0))
                if ((transform.position.y < -4 && _localTravelDirection < 0) || (transform.position.y > 6 && _localTravelDirection > 0))                {
                    // Reverse direction
                    SwitchLocalTravelDirection();
                }                
            }
        }
        else if (_localTravelAxis == "X-Axis" && !_passingJunction)
        {
            //Debug.Log("travelX-AXIS ----------------------------------------------------------------- X-AXIS");
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
                //Debug.Log("_min" + _min + " _max " + _max + " _clampedX " + _clampedX);
            
                if (_clampedX == _xJunctionPoints[i])
                {
                    //Debug.Log("Got MATCH !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! X " + _clampedX);
                    if (Random.Range(0, 2) == 1)
                    {
                        // Encountered a junction on x-Axis and switching to travel on y-Axis
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

                        break;
                    }
                }
            }

            if (_newDirection != 0)
            {
                // direction has changed - update _travelDirection
                //_gameManager.SetTravelDirection(_newDirection);
                SetLocalTravelDirection(_newDirection);

                // Set X axis value to X junction value to be exactly on the X path 
                Vector3 _position = transform.position;
                _position.x = _clampedX;
                transform.position = _position;
                //Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Setting psotition: X:" + _position.x + " / Y:"  + _position.y);
                
                // prevent evaluation of junction on next iteration as value may still be in range
                _passingJunction = true; 
            }
            else
            {
                // direction has not changed - continue on x axis.
                transform.Translate(Vector3.left * _travelSpeed * Time.deltaTime);
        
                //if ((transform.position.x < -9 && _gameManager._travelDirection > 0) || (transform.position.x > 9 && _gameManager._travelDirection < 0))
                if ((transform.position.x < -9 && _localTravelDirection > 0) || (transform.position.x > 9 && _localTravelDirection < 0))
                {
                    // Reverse direction
                    SwitchLocalTravelDirection();
                }
            }
        } else if (_localTravelAxis == "Y-Axis" && _passingJunction)
        {
            _passingJunction = false;
            // direction has not changed - continue of y axis.
            transform.Translate(Vector3.up * _travelSpeed * Time.deltaTime);

            if ((transform.position.y < -4 && _localTravelDirection < 0) || (transform.position.y > 6 && _localTravelDirection > 0))
            {
                // Reverse direction
                SwitchLocalTravelDirection();
            }                
        } else if (_localTravelAxis == "X-Axis" && _passingJunction)
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

    public void StartSiege()
    {
        _siege = true;
    }
    public void EndSiege()
    {
        _siege = false;
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
