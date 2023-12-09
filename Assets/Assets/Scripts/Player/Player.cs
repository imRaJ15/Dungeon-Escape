using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    Rigidbody2D _rb2d;
    float _playerSpeed = 3.0f;
    float _jumpForce = 7.0f;
    public bool _resetJumpNeeded = false;
    PlayerAnimation _playerAnimation;
    SpriteRenderer _spriteRenderer;
    SpriteRenderer _swordSR;
    bool _grounded = false;
    bool faceright = false;
    public int diamonds;
    int _health = 4;
    public bool _isPlayerDead = false;
    bool _moveLeft = false;
    bool _moveRight = false;
    float _horizontalMove;
    bool _isGamePause = false;

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    GameObject _gameoverImage;

    [SerializeField]
    GameObject _castleKeyImage;

    [SerializeField]
    GameObject _winnerImage;

    [SerializeField]
    GameObject _getCastleKeyImage;

    [SerializeField]
    GameObject _pauseMenu;

    [SerializeField]
    GameObject _actionButtons;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordSR = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDead == false)
        { 
            PlayerMovement(); 
            Movement();
        }

        if (GameManager.Instance.HasKeyToCastle == true)
        { _castleKeyImage.SetActive(true); }

        HideActionButtons();
    }

    void Movement()
    {
        float horizotalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() == true)
            {
                _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
                Debug.Log("Jump!");
                _playerAnimation.Jump(true);
                StartCoroutine(ResetJumpNeededRutione());
            }
        }
        _rb2d.velocity = new Vector2(horizotalInput * _playerSpeed, _rb2d.velocity.y);
    }

    public void PointerDownLeft()
    {
        _grounded = IsGrounded();
        _moveLeft = true;
        Flip(false);
    }

    public void PointerUpLeft()
    { _moveLeft = false; }

    public void PointerDownRight()
    {
        _grounded = IsGrounded();
        _moveRight = true;
        Flip(true);
    }

    public void PointerUpRight()
    { _moveRight = false;   }

    void PlayerMovement()
    {
        if (_moveLeft)
        { 
            _horizontalMove = -_playerSpeed;
        }
        else if (_moveRight)
        { 
            _horizontalMove = _playerSpeed;
        }

        else { _horizontalMove = 0; }
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(_horizontalMove, _rb2d.velocity.y);
        _playerAnimation.Move(_horizontalMove);
    }

    public void PointerDownJump()
    {
        if (IsGrounded() == true)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);
            ///Debug.Log("Jump!");
            ///_playerAnimation.Jump(true);
            StartCoroutine(ResetJumpNeededRutione());
        }
    }

    public void PointerUpJump()
    { IsGrounded(); }

    public void PointerDownAttack()
    {
        if (IsGrounded() == true)
        { _playerAnimation.Attack(); }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundLayer);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if (_resetJumpNeeded == false)
            {
                 Debug.Log("Grounded!");
                ///_playerAnimation.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Flip(bool faceright)
    {
        if (faceright == true)
        { 
            _spriteRenderer.flipX = false;
            _swordSR.flipX = false;
            _swordSR.flipY = false;

            Vector3 newPos = _swordSR.transform.localPosition;
            newPos.x = 1.01f;
            _swordSR.transform.localPosition = newPos;
        }
        else if (faceright == false)
        { 
            _spriteRenderer.flipX = true;
            _swordSR.flipX = true;
            _swordSR.flipY = true;

            Vector3 newPos = _swordSR.transform.localPosition;
            newPos.x = -1.01f;
            _swordSR.transform.localPosition = newPos;
        }
    }

    IEnumerator ResetJumpNeededRutione()
    {
        _resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false; 
    }

    public void Damage()
    {
        if (_health < 1)
        { return; }
        Debug.Log("Player taking Damage");
        _health--;
        UIManager.Instance.LifeUpdate(_health);

        if (_health < 1)
        {
            _isPlayerDead = true;
            _playerAnimation.Dead();
            StartCoroutine(GameOverRoutine());
        }
    }

    public void DiamondsCollection(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateDiamondsCount(diamonds);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spikes")
        {
            _health = 0;
            _isPlayerDead = true;
            _playerAnimation.Dead();
            StartCoroutine(GameOverRoutine());

        }

        if (other.tag == "CastleEntrance")
        {
            if (GameManager.Instance.HasKeyToCastle == true)
            { StartCoroutine(WinnerRoutine()); }
            else { _getCastleKeyImage.SetActive(true); }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "CastleEntrance")
        { _getCastleKeyImage.SetActive(false); }
        
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        _gameoverImage.SetActive(true);

    }

    IEnumerator WinnerRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        _winnerImage.SetActive(true);
    }

    public void ActivePauseMenu()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _isGamePause = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _isGamePause = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void HideActionButtons()
    {
        if (_isGamePause == true)
        { _actionButtons.SetActive(false); }

        else
        { _actionButtons.SetActive(true); }
    }
}
