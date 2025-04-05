using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    [Header("положение героя")]
    [SerializeField] private Transform _player;

    [Header("кнопочки")]
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _restartButton;

    private bool _isPause;
    private bool _isPlaying;

    private float _recordValue;

    [Header("текстики")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _recordText;

    private void Start()
    {
        _isPause = false;
        _isPlaying = false;
        _recordValue = PlayerPrefs.GetFloat("Record", 0);
        _recordText.text = Mathf.Round(_recordValue).ToString();

    }

    private void Update()
    {
        _scoreText.text = Mathf.Round(_player.position.z).ToString();
        if (_recordValue < _player.position.z)
        {
            _recordValue = _player.position.z;
            _recordText.text = Mathf.Round(_recordValue).ToString();
            PlayerPrefs.SetFloat("Record", _recordValue);
            PlayerPrefs.Save();
        }


        _restartButton.SetActive(_isPause);
        _exitButton.SetActive(_isPause);
       
        if (_isPlaying)
        {

            _playButton.SetActive(_isPause);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPause = !_isPause;
            }

            if (_isPause)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        else
        {
            _playButton.SetActive(true);
            _exitButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Play ()
    {
        if (_isPlaying)
        {
            _isPause = !_isPause;
        }
        else
        {
            _isPlaying = true;
        }
        
    }

    public void Pause()
    {
        if (_isPlaying)
        {
            _isPause = !_isPause;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

  }
