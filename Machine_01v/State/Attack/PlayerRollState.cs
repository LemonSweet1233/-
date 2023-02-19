using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerRollState : PlayerAbilityState
    {
        private bool canRoll;
        private bool isHolding;
        private float rollCodeTime;
        private Vector2 rollDirection;
        public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();

            canRoll = true;

            isHolding = true;
            rollDirection = Vector2.right * player.FacingDirection;

            Time.timeScale = playerData.holdTimeScale;
            startTime = Time.unscaledTime;

            player.rollDirection.gameObject.SetActive(true);

            SetVelocityY(playerData.rollJumpVelocityY);

            Debug.Log("RollState");            
        }

        public override void Exit()
        {
            base.Exit();

            if(player.RigidDody2D.velocity.y > 0)
            {
                SetVelocityY(player.RigidDody2D.velocity.y * playerData.rollEndYMultiplier);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExitingState)
            {
                if (isHolding)
                {
                    if (player.PlayerController.WordPosition != Vector2.zero)
                    {
                        rollDirection = player.GetRollDirection();
                        rollDirection.Normalize();
                    }

                    float angle = Vector2.SignedAngle(Vector2.right, rollDirection);

                    player.rollDirection.rotation = Quaternion.Euler(0, 0, angle - 45f);

                    if (Time.unscaledTime >= startTime + playerData.maxHoldTime)
                    {
                        isHolding = false;
                        Time.timeScale = 1f;
                        startTime = Time.time;

                        player.SetSpriteFlipX(Mathf.RoundToInt(rollDirection.x));
                        player.RigidDody2D.drag = playerData.drag;
                        player.SetVelocity(playerData.rollVelocity * Time.time, rollDirection);
                        player.rollDirection.gameObject.SetActive(false);
                    }
                }
                else
                {
                    player.SetVelocity(playerData.rollVelocity * Time.time, rollDirection);

                    if(Time.unscaledTime >= startTime + playerData.maxHoldTime)
                    {
                        player.RigidDody2D.drag = 0;
                        isAbleilityDone = true;
                        rollCodeTime = Time.time;
                    }
                }
            }
        }

        public override void OnChecks()
        {
            base.OnChecks();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        public bool ChenkIfCanRoll()
        {
            return canRoll && Time.time >= rollCodeTime + playerData.rollCoolTime;
        }
        public void RestCanRoll() => canRoll = true;
    }

}
