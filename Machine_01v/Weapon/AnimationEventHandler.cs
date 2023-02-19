using System;
using UnityEngine;

namespace NewGame
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private AudioManager audioManager;
        public Action OnFinish;

        private void Awake()
        {
            audioManager = AudioManager.instance;
        }

        private void AnimationFinishedTrigger() => OnFinish?.Invoke();
        
        private void AE_SwordAttack()
        {
            audioManager.PlaySound("SwordAttack");
        }
    }
}