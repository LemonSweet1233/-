using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerInAirState : PlayerState
    {
        public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (player.IsTouchingWall && !player.IsLedgeWall)
            {
                player.SetVelocityZero();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            SetPlayerVelocityX();

            if(player.PlayerController.AttackInput[(int)EnumAttackInput.primary] && player.PlayerController.NormInputY == -1)
            {
                stateMachine.ChangeState(player.InAirAttackDonw);
            }
            else if (player.PlayerController.AttackInput[(int)EnumAttackInput.primary] && !player.IsTouchingWall)
            {
                stateMachine.ChangeState(player.InAirAttackState);
            }
            else if (player.IsGround && player.RigidDody2D.velocity.y < 0.01f)
            {
                stateMachine.ChangeState(player.LandState);
            }
            else if (player.IsTouchingWall && player.PlayerController.NormInputX == player.FacingDirection && player.RigidDody2D.velocity.y <= 0)
            {
                stateMachine.ChangeState(player.WallSlideState);
            }
            else if(player.IsTouchingWall && player.PlayerController.ControlButtenDown)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if(player.IsTouchingWall && !player.IsLedgeWall && !player.IsGround)
            {
                stateMachine.ChangeState(player.LedgeClimbState);
            }
            else if (player.PlayerController.SpaceButtenDown && player.JumpState.CanJump() && player.PlayerController.NormInputX != 0)
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (player.PlayerController.SpaceButtenDown && player.JumpState.CanJump() && player.PlayerController.NormInputX == 0)
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if(player.PlayerController.Roll && player.RollState.ChenkIfCanRoll())
            {
                stateMachine.ChangeState(player.RollState);
            }
        }

        private void SetPlayerVelocityX()
        {
            if (!player.IsTouchingWall && !player.IsGround)
            {
                player.SetVelocityX(playerData.jumpSpeed * player.PlayerController.NormInputX);
            }
            else if (!player.IsGround && player.IsTouchingWall)
            {
                player.SetVelocityX(0);
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
    }
}

