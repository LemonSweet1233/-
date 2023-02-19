using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {
        protected Vector2 position;
        public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

            SetVelocity();

            if (!isExitingState && player.IsTouchingWall)
            {
                if (player.PlayerController.ControlButtenDown && InputX())
                {
                    stateMachine.ChangeState(player.WallClimbState);
                }
                else if (!player.PlayerController.ControlButtenDown || !(InputX()))
                {
                    stateMachine.ChangeState(player.WallSlideState);
                }
            }
        }

        private void SetVelocity()
        {
            player.SetVelocityY(0);
            player.SetVelocityX(0);
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
