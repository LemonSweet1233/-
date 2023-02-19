using NewGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGame
{
    public class Weapon : MonoBehaviour
    {
        public Action OnExit;

        [SerializeField] private int numberOfAttacks;
        [SerializeField] private float attackCounterRestCooldown;
        private int curentAttackConter;
        public int CurrentAttackCounter { get => curentAttackConter; private set => curentAttackConter = value >= numberOfAttacks ? 0 : value;  }

        private Animator animator;
        private GameObject baseGameObject;
        private AnimationEventHandler eventHandler;
        private Timer attackResetTimer;

        private void Awake()
        {
            baseGameObject = transform.GetChild(0).gameObject;
            animator = baseGameObject.GetComponent<Animator>();
            eventHandler = baseGameObject.GetComponent<AnimationEventHandler>();

            attackResetTimer = new Timer(attackCounterRestCooldown);
        }
        private void OnEnable()
        {
            eventHandler.OnFinish += Exit;
            attackResetTimer.OnTimerDone += ResetAttackCounter;
        }
        private void OnDisable()
        {
            eventHandler.OnFinish -= Exit;
            attackResetTimer.OnTimerDone -= ResetAttackCounter;
        }
        private void Update()
        {
            attackResetTimer.Tick();
        }
        private void Enter()
        {
            print(message: $"{transform.name} enter");

            animator.SetBool("active", true);

            animator.SetInteger("attackCounter", curentAttackConter);

            attackResetTimer.SetStopTime();

        }
        private void Exit()
        {
            animator.SetBool("active", false);

            CurrentAttackCounter++;
            attackResetTimer.GetStartTime();
            
            OnExit?.Invoke();
        }
        public void GetEnter() => Enter();
        public void GetExit() => Exit();

        private void ResetAttackCounter() => CurrentAttackCounter = 0;
    }
}

