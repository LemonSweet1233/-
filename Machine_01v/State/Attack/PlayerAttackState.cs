using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon weapon;

        public PlayerAttackState(
            Player player, 
            PlayerStateMachine stateMachine, 
            PlayerData playerData, 
            string animBoolName,
            Weapon weapon
            ) : base(player, stateMachine, playerData, animBoolName)
        {
            this.weapon = weapon;

            weapon.OnExit += ExitEvent;
        }

        public override void Enter()
        {
            base.Enter();

            weapon.GetEnter();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            player.SetPlayerFacing(false);
        }
        private void ExitEvent()
        {
            AnimationFinishTrigger();
            isAbleilityDone = true;
        }
    }
}

