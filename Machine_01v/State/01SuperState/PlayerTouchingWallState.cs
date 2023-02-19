using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerTouchingWallState : PlayerState
    {
        public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.IsGround && !player.PlayerController.ControlButtenDown)
            {
                stateMachine.ChangeState(player.IdleState);
            } 
            else if (!player.IsGround && !player.IsTouchingWall && !player.PlayerController.ControlButtenDown)
            {
                stateMachine.ChangeState(player.InAirState);
            }
            else if(player.IsTouchingWall && !player.IsLedgeWall)
            {
                stateMachine.ChangeState(player.LedgeClimbState);
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

        protected bool InputX()
        {
            return Mathf.Abs(player.PlayerController.NormInputX) == 1;
        }
    }
}

