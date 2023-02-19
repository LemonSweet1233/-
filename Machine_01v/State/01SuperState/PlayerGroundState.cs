using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NewGame
{
    public class PlayerGroundState : PlayerState
    {
        public PlayerGroundState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.JumpState.ResetJumpNumber();
            player.RollState.RestCanRoll();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.PlayerController.AttackInput[(int)EnumAttackInput.primary] && player.PlayerController.NormInputY == 0)
            {
                stateMachine.ChangeState(player.PrimaryAttackState);
            }
            else if (player.PlayerController.AttackInput[(int)EnumAttackInput.secondary] && player.PlayerController.NormInputY == 0)
            {
                stateMachine.ChangeState(player.SecondaryAttackState);
            }
            else if (player.PlayerController.AttackInput[(int)EnumAttackInput.parryStance])
            {
                stateMachine.ChangeState(player.ParryStanceState);
            }
            else if (player.PlayerController.NormInputY == 1 && player.PlayerController.AttackInput[(int)EnumAttackInput.primary])
            {
                stateMachine.ChangeState(player.AttackUpState);
            }
            else if (player.PlayerController.SpaceButtenDown && player.JumpState.CanJump())
            {
                stateMachine.ChangeState(player.JumpState);
            }
            else if (!player.IsGround)
            {
                player.JumpState.ResetJumpNumber();
                stateMachine.ChangeState(player.InAirState);
            }
            else if(player.IsTouchingWall && player.PlayerController.ControlButtenDown && player.IsLedgeWall)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
            else if (player.PlayerController.Roll && player.RollState.ChenkIfCanRoll())
            {
                stateMachine.ChangeState(player.RollState);
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
        protected void SetVelocityX(float velocityX)
        {
            player.SetVelocityX(velocityX);
        }
    }
}

