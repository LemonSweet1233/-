using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            OnChecks();
            player.Animator.SetFloat("VelocityX", 1);
            startTime = Time.time;
            Debug.Log("MoveState");
        }

        public override void Exit()
        {
            player.Animator.SetFloat("VelocityX", 0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (player.IsGround)
            {
                if (player.PlayerController.NormInputX == 0f)
                {
                    stateMachine.ChangeState(player.IdleState);

                }
                else if (player.PlayerController.NormInputX != 0f && player.PlayerController.LiftShiftButtenDown)
                {
                    stateMachine.ChangeState(player.RunState);
                }
                else if (player.PlayerController.NormInputY == -1)
                {
                    stateMachine.ChangeState(player.CrouchMoveState);
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

            SetVelocityX(playerData.walkSpeed * player.PlayerController.NormInputX);
        }
    }
}

