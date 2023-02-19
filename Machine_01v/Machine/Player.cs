using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        #region PlayerState
        [Tooltip("PlayerState")]
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerRunState RunState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallClimbState WallClimbState { get; private set; }
        public PlayerWallGrabState WallGrabState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerLedgeClimbState LedgeClimbState { get; private set; }
        public PlayerRollState RollState { get; private set; }
        public PlayerCrouchState CrouchState { get; private set; }
        public PlayerCrouchMoveState CrouchMoveState { get; private set; }
        public PlayerSlidingTackeState SlidingTackeState { get; private set; }
        public PlayerAttackState PrimaryAttackState { get; private set; }
        public PlayerAttackState SecondaryAttackState { get; private set; }
        public PlayerAttackUpState AttackUpState { get; private set; }
        public PlayerInAirAttackState InAirAttackState { get; private set; }
        public PlayerParryState ParryStanceState { get; private set; }
        public PlayerParryState ParryState { get; private set; }
        public PlayerInAirAttackDonw InAirAttackDonw { get; private set; }

        #endregion

        #region Components

        public Animator Animator { get; private set; }

        [SerializeField] private PlayerData playerData;
        public PlayerController PlayerController { get; private set; }

        public Rigidbody2D RigidDody2D { get; private set; }
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        public Transform rollDirection { get; private set; }
        [SerializeField] public bool IsGround { get; private set; }
        [SerializeField] public bool IsTouchingWall { get; private set; }
        [SerializeField] public bool IsLedgeWall { get; private set; }
        [SerializeField] public bool IsHanding { get; private set; }

        private int facingDirection = 1;
        public bool IsFacing { get; private set; }
        public int FacingDirection { get { return facingDirection; } set { } }

        //Combate
        private Weapon primaryWeapon;
        private Weapon secondaryWeapon;

        #endregion

        private void Awake()
        {
            Animator = GetComponent<Animator>();

            RigidDody2D = GetComponent<Rigidbody2D>();

            PlayerController = GetComponent<PlayerController>();

            rollDirection = GameObject.FindGameObjectWithTag("Roll").transform;

            primaryWeapon = transform.GetChild(0).GetComponent<Weapon>();

            secondaryWeapon = transform.GetChild(1).GetComponent<Weapon>();

            rollDirection.gameObject.SetActive(false);
        }

        private void Start()
        {
            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");

            MoveState = new PlayerMoveState(this, StateMachine, playerData, "Move");

            RunState = new PlayerRunState(this, StateMachine, playerData, "Run");

            JumpState = new PlayerJumpState(this, StateMachine, playerData, "Jump");

            InAirState = new PlayerInAirState(this, StateMachine, playerData, "IsInAir");

            LandState = new PlayerLandState(this, StateMachine, playerData, "IsGround");

            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");

            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");

            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");

            LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");

            CrouchState = new PlayerCrouchState(this, StateMachine, playerData, "crouchIdle");

            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");

            RollState = new PlayerRollState(this, StateMachine, playerData, "IsInAir");

            SlidingTackeState = new PlayerSlidingTackeState(this, StateMachine, playerData, "slidingTackle");

            PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack", primaryWeapon);

            SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack" , secondaryWeapon);

            AttackUpState = new PlayerAttackUpState(this, StateMachine, playerData, "UpAttack");

            InAirAttackState = new PlayerInAirAttackState(this, StateMachine, playerData, "InAirAttack");

            ParryStanceState = new PlayerParryState(this, StateMachine, playerData, "ParryStance");

            //ParryState = new PlayerParryState(this, StateMachine, playerData, "ParryStance");

            InAirAttackDonw = new PlayerInAirAttackDonw(this, StateMachine, playerData, "InAirAttackDown");

            StateMachine.Initialize(IdleState);
        }
        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();

            SetSpriteFlipX(1);

            GetCheckGround();
            GetCheckForward();
            GetCheckLedgeWall();
            GetCheckHand();

            
        }
        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }

        #region Tools
        public void SetSpriteFlipX(int x)
        {
            if (PlayerController.NormInputX != 0 && PlayerController.NormInputX != facingDirection && IsFacing)
            {
                facingDirection *= -1 * x;
                transform.Rotate(0, 180, 0);
            }
        }
        public void SetVelocityX(float velocityX)
        {
            float velocityY = RigidDody2D.velocity.y;

            RigidDody2D.velocity = new Vector2(velocityX, 0f) + new Vector2(0f, velocityY);
        }
        public void SetVelocityY(float velocityY)
        {
            float velocityX = RigidDody2D.velocity.x;

            RigidDody2D.velocity = new Vector2(velocityX, 0f) + new Vector2(0f, velocityY);
        }
        public void SetVelocity(float velocity,Vector2 direction)
        {
            Debug.Log(direction);

            //Vector2 workspace = velocity * direction;
            //RigidDody2D.velocity = workspace;

            RigidDody2D.MovePosition(RigidDody2D.position + velocity * direction * Time.fixedDeltaTime);

        }
        public void SetVelocityZero()
        {
            RigidDody2D.velocity = Vector2.zero;
        }
        public void GetCheckGround()
        {
            IsGround = Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRaius, playerData.isGround);
        }
        private void GetCheckForward()
        {
            IsTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right, playerData.wallCheckDistance, playerData.isGround);

            Debug.DrawRay(wallCheck.position, Vector2.right * playerData.wallCheckDistance, Color.yellow);
        }

        private void GetCheckLedgeWall()
        {
            IsLedgeWall = Physics2D.Raycast((Vector2)wallCheck.position + Vector2.up, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.isGround);

            Debug.DrawRay((Vector2)wallCheck.position + Vector2.up, Vector2.right * playerData.wallCheckDistance, Color.red);
        }

        private void GetCheckHand()
        {
            IsHanding = Physics2D.Raycast((Vector2)transform.position, Vector2.up, playerData.wallCheckDistance, playerData.isGround);

            Debug.DrawRay((Vector2)transform.position, Vector2.up * playerData.wallCheckDistance, Color.red);
        }
        public Vector2 DetermineCornerPosition()
        {
            RaycastHit2D hitX = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.isGround);

            Vector2 workspace = new Vector2(hitX.distance * FacingDirection, 0);

            RaycastHit2D hixY = Physics2D.Raycast((Vector2)wallCheck.position + workspace + Vector2.up, Vector2.down, Vector2.up.y, playerData.isGround);

            workspace.Set(wallCheck.position.x + hitX.distance * FacingDirection, wallCheck.position.y + Vector2.up.y - hixY.distance);

            return workspace;
        }

        public void AnimationTrigger()
        {
            LedgeClimbState.AnimationTrigger();
            SlidingTackeState.AnimationTrigger();
        }
        public void AnimationFinishTrigger()
        {
            LedgeClimbState.AnimationFinishTrigger();
            SlidingTackeState.AnimationFinishTrigger();
        }

        public void SetPlayerFacing(bool canFacing)
        {
            if (canFacing)
            {
                IsFacing = true;
            }
            else
            {
                IsFacing = false;
            }
        }

        public Vector2 GetRollDirection()
        {
            return Vector2Int.RoundToInt(PlayerController.WordPosition - (Vector2)rollDirection.position);
        }

        #endregion
    }
}

