using System;
using System.Collections;
using System.Collections.Generic;
using TooLoo.AI;
using UnityEngine;
using YATE.UI;
using TooLoo;
using YATE.Audio;

namespace YATE
{
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        [SerializeField] private float startingHealth = 100f;

        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private BabyDiscomfort babyDiscomfort;
        [SerializeField] private FootSteps footSteps;

        public bool IsAlive { get; set; } = true;

        public bool IsSighted { get; set; } = false;

        public event Action<ECasonStatus> OnSighted;
        public event Action<ECasonStatus> OnUnsighted;

        [Tooltip("For Debugging Only")]
        [SerializeField, ReadOnly] private List<AIAgent> enemiesInPursuit = new();

        private void Start()
        {
            Init(new Vector3(-35f, 0, 0));
        }

        public void Init(Vector3 startingPosition)
        {
            characterMovement.Init();
            babyDiscomfort.Init();
            footSteps.Init();
        }

        public void AddEnemyInPursuit(AIAgent enemy)
        {
            enemiesInPursuit.Add(enemy);
            OnSighted?.Invoke(ECasonStatus.Sighted);
        }

        public void RemoveEnemyInPursuit(AIAgent enemy)
        {
            enemiesInPursuit.Remove(enemy);

            if (enemiesInPursuit.Count == 0)
            {
                OnUnsighted?.Invoke(ECasonStatus.Unsighted);
            }
        }
    }
}