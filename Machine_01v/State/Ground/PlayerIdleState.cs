using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NewGame
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            OnChecks();
            player.Animator.SetFloat("VelocityX", 0);
            startTime = Time.time;
            Debug.Log("IdleState");
        }

        public override void Exit()
        {
            player.Animator.SetFloat("VelocityX", 0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExitingState)
            {
                if (player.PlayerController.NormInputX != 0f)
                {
                    if (!player.PlayerController.LiftShiftButtenDown)
                    {
                        stateMachine.ChangeState(player.MoveState);
                    }
                    else
                    {
                        stateMachine.ChangeState(player.RunState);
                    }
                }
                else if(player.PlayerController.NormInputY == -1)
                {
                    stateMachine.ChangeState(player.CrouchState);
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

            SetVelocityX(0);
        }
    }
}

