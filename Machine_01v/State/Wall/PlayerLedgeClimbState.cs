using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerLedgeClimbState : PlayerState
    {
        private Vector2 cornerPosition;
        private Vector2 startPos;
        private Vector2 stopPos;

        private bool isHanging;
        private bool isClimbing;
        public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName){}

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();

            player.Animator.SetBool("ledgeClimb", false);
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();

            isHanging = true;
        }

        public override void Enter()
        {
            base.Enter();

            player.SetVelocityZero();

            cornerPosition = player.DetermineCornerPosition();

            startPos.Set(cornerPosition.x - player.FacingDirection * playerData.startOffset.x, cornerPosition.y - playerData.startOffset.y);
            stopPos.Set(cornerPosition.x + player.FacingDirection * playerData.stopOffset.x, cornerPosition.y + playerData.stopOffset.y);

            player.transform.position = startPos;
        }

        public override void Exit()
        {
            base.Exit();

            if (isClimbing)
            {
                player.transform.position = stopPos;

                isClimbing = false;
            }

            isHanging = false;
        }


        public override void LogicUpdate()
        {
            player.SetVelocityZero();

            player.transform.position = startPos;

            if(isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                if (player.PlayerController.NormInputY == 1 && isHanging && !isClimbing)
                {
                    isClimbing = true;
                    player.Animator.SetBool("ledgeClimb", true);
                }
                else if (player.PlayerController.NormInputY == -1 && !isClimbing && isHanging)
                {
                    stateMachine.ChangeState(player.InAirState);
                }
                else if(!isClimbing && player.PlayerController.SpaceButtenDown && player.PlayerController.NormInputX == -player.FacingDirection)
                {
                    stateMachine.ChangeState(player.JumpState);
                }
            }
        }

        public override void OnChecks()
        {
            base.OnChecks();
            player.SetVelocityZero();
        }
    }
}
