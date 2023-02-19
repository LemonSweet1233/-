using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewGame
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.JumpState.ResetJumpNumber();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.SetVelocityY(-playerData.wallSlideVelocityY);

            if (player.IsTouchingWall)
            {
                if (player.PlayerController.ControlButtenDown && InputX() && !isExitingState)
                {
                    stateMachine.ChangeState(player.WallGrabState);
                }
                else if (player.PlayerController.SpaceButtenDown)
                {
                    stateMachine.ChangeState(player.JumpState);
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
    }
}