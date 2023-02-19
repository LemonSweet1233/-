using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerSlidingTackeState : PlayerGroundState
    {
        public PlayerSlidingTackeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        public override void LogicUpdate()
        {
            if (isAnimationFinished)
            {
                if (player.IsHanding)
                {
                    stateMachine.ChangeState(player.CrouchState);
                }
                else
                {
                    stateMachine.ChangeState(player.IdleState);
                }  
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            SetVelocityX(playerData.slidingTackeVelocityX * player.FacingDirection);
        }
    }
}

