using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float _speed = 10;
    public float _shaffleSpeed = 10;
    public float _jumpForce = 10;
    public float _gravityScale = 10;

    private bool _isGround = true;

    private Vector3 _movement = new Vector3(0, 0, 0);

    public Rigidbody _rb;

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal") * _shaffleSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGround)
            {
                _isGround = false;
                _movement.y = _jumpForce;
            }
        }
    }

    private void FixedUpdate()
    {
        _movement.z = _speed;
        _rb.MovePosition(transform.position + _movement * Time.fixedDeltaTime);

        if (_isGround) 
        {
            _movement.y = 0;
        }
        else
        {
            _movement.y -= _gravityScale * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }

        if (collision.gameObject.tag == "Kill")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
