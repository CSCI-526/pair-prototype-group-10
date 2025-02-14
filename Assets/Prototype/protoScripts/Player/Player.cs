using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public bool isBusy;
    public GameObject attackHitBox;
    public Vector2 attackHitBoxCenterOffset;
    public Vector2 attackHitBoxSize;
    public bool showAttackHitBox;
    [HideInInspector] public float lastTimeAttacked;
    [HideInInspector] public int comboCounter;

    [Header("Parry Details")]

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    public float airForce;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsEnemy;

    public int facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; } 
    public PlayerParryState parryState { get; private set; }
    public PlayerDefenseState defenseState { get; private set; }
    public PlayerAirParryState airParryState { get; private set; }
    public PlayerSecondaryAttackState secondaryAttackState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        parryState = new PlayerParryState(this, stateMachine, "Parry");
        defenseState = new PlayerDefenseState(this, stateMachine, "Defense");
        airParryState = new PlayerAirParryState(this, stateMachine, "AirParry");
        secondaryAttackState = new PlayerSecondaryAttackState(this, stateMachine, "FollowUpAttack");

    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        
    }

    public IEnumerator Busyfor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }


    #region Trigger
    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    public void JumpTrigger()
    {
        rb.velocity = new Vector2(0f, airForce);
    }

    public void AttackTrigger()
    {
        stateMachine.currentState.attackEnableTrigger();
    }

    #endregion

    #region Velocity
    public void setVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public void ZeroVelocity() => rb.velocity = new Vector2(0,0);
    #endregion

    #region Collision
    public bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        //Ground Check
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        //attack box check
        float attackBoxCenterX;
        float attackBoxCenterY;
        if (showAttackHitBox)
        {
            if (facingRight)
            {
                attackBoxCenterX = transform.position.x + attackHitBoxCenterOffset.x;
                attackBoxCenterY = transform.position.y + attackHitBoxCenterOffset.y;

            }
            else
            {
                attackBoxCenterX = transform.position.x - attackHitBoxCenterOffset.x;
                attackBoxCenterY = transform.position.y + attackHitBoxCenterOffset.y;

            }
            Vector2 attackBoxCenter = new Vector2(attackBoxCenterX, attackBoxCenterY);
            Gizmos.DrawWireCube((Vector2)attackBoxCenter, attackHitBoxSize);

        }


    }
    #endregion

    #region Filp
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void FlipController(float _x)
    {
        if (facingRight && _x < 0)
        {
            Flip();
        }
        else if (!facingRight && _x > 0)
        {
            Flip();
        }
    }
    #endregion



    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;
        string currentStateName =  stateMachine.currentState.AnimBoolNameGUI;
        GUI.Box(new Rect(10, 10, 200, 100), "Player State");
        GUI.Label(new Rect(20, 35, 200, 40), currentStateName, style);
        if (stateMachine.currentState == primaryAttackState)
        {
            GUI.Label(new Rect(20, 60, 200, 40), "Combo Counter: " + comboCounter, style);
        }
        if (stateMachine.currentState == secondaryAttackState)
        {
            GUI.Label(new Rect(20, 60, 200, 40), "Follow Up Counter: " + comboCounter, style);
        }
    }
}
