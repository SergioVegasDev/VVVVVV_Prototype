using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MoveBehavior))]
[RequireComponent(typeof(ChangeSpriteDirectionBehavior))]
[RequireComponent(typeof(ChangeGravityBehavior))]
public class Player : MonoBehaviour, InputSystem_Actions.IPlayerActions, IDammageable
{
    protected ChangeSpriteDirectionBehavior _csdb;
    private Rigidbody2D _rb;
    protected ChangeGravityBehavior _cg;
    protected MoveBehavior _mb;
    protected float speed = 5;
    private bool gravityFlipped = false;
    protected Animator _animator;
    private float horizontalVelocity;
    private InputSystem_Actions _actions;
    public static event Action UsePauseMenu = delegate { };
    public static event Action UseWinMenu = delegate { };
    public static event Action UseGameOverMenu = delegate { };
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _csdb = GetComponent<ChangeSpriteDirectionBehavior>();
        _mb = GetComponent<MoveBehavior>();
        _cg = GetComponent<ChangeGravityBehavior>();
        _rb = GetComponent<Rigidbody2D>();
        _actions =  new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
    }

    public void Update()
    {
        _mb.MoveCharacterHorizontal(new Vector2(horizontalVelocity, 0), speed);
        _animator.SetFloat("Velocity", Mathf.Abs(_rb.linearVelocityX));
        _csdb.ChangeSpriteDirectionWithChangeGravity(_rb.linearVelocityX, gravityFlipped);
        _animator.SetBool("Grounded", true);
        if (_cg.IsGrounded())
        {
            _animator.SetBool("Grounded", true);
        }
        else
            _animator.SetBool("Grounded", false);
    } 
    private void ControlInputs(bool enable)
    {
        if (enable) 
            _actions.Disable();
        else _actions.Enable();
    }
    public void Die()
    {
        _animator.SetTrigger("Death");
        _actions.Disable();
        GetComponent<CapsuleCollider2D>().enabled = false;
        UseAudioManger(AudioClips.DeathSound);
        Invoke(nameof(ExecuteGameOver), 2.5f);
        _rb.bodyType = RigidbodyType2D.Static;
    }
    public void Respawn()
    {
        _actions.Enable();
        GetComponent<CapsuleCollider2D>().enabled = true;
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void ExecuteGameOver()
    {
        UseGameOverMenu.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalVelocity = context.ReadValue<Vector2>().x;
        if (_cg.IsGrounded())
        {
            if (context.ReadValue<Vector2>().y!=0)
            {
                UseAudioManger(AudioClips.ChangeGravity);
                _animator.SetTrigger("Jump");
                _cg.ChangeGravity(ref gravityFlipped);
            }
            _animator.SetBool("Grounded", true);
        }
        else
            _animator.SetBool("Grounded", false);
    }
    public void OnEscape(InputAction.CallbackContext context)
    {
        UsePauseMenu.Invoke();
    }

    public void OnEnable()
    {
        _actions.Enable();
        Dialogue.PausePlayer += ControlInputs;
        PauseMenu.PausePlayer += ControlInputs;
        WinMenu.PausePlayer += ControlInputs;
    }
    public void OnDisable()
    {
        Dialogue.PausePlayer -= ControlInputs;
        PauseMenu.PausePlayer -= ControlInputs;
        WinMenu.PausePlayer -= ControlInputs;
        _actions.Disable();
    }
    private void OnDestroy()
    {
        Dialogue.PausePlayer -= ControlInputs;
        PauseMenu.PausePlayer -= ControlInputs;
        WinMenu.PausePlayer -= ControlInputs;
        _actions.Disable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            UseAudioManger(AudioClips.FinalEncounter);
            Invoke(nameof(TriggerWinMenu), 0.1f);
        }
    }
    private void TriggerWinMenu()
    {
        UseWinMenu.Invoke();
    }
    private void UseAudioManger(AudioClips audioName)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (AudioManager.Instance.clipList.TryGetValue(audioName, out AudioClip clip))
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
