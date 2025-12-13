using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _energyText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    public TextMeshProUGUI _restartText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _energyText = GameObject.Find("EnergyText").GetComponent<TextMeshProUGUI>();
        //_scoreText = GetComponent<TextMeshPro>();
        if (_restartText == null)
        {
            Debug.Log("_restartText IS NULLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
        }

        if (_energyText == null)
        {
            Debug.Log("_energyText IS NULL");
        }
        else
        {
            Debug.Log("_energyText IS NOT NULL");
        }
        _energyText.text = "Energy:" + 0;
    }

    // Update is called once per frame
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore.ToString();        
    }
    public void UpdateEnergy(int energyLevel)
    {
        _energyText.text = "Energy:" + energyLevel.ToString();        
    }
    public void ShowGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
    }

}
