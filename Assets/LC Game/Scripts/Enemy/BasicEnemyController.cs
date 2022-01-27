using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace LCGame.Enemy
{
    public class BasicEnemyController : MonoBehaviour
    {
        private enum EnemyState { Walking, Death }
        private EnemyState enemyState;
        
        [SerializeField] private float movementSpeed = 3.0f;
        [SerializeField] private bool goingRight = true;

        private Animator animator;
        private static readonly int death = Animator.StringToHash("Death");

        private void Start()
        {
            transform.position = pos1.position;
            enemyState = EnemyState.Walking;
            animator = GetComponentInChildren<Animator>();
        }

    #region Enemy Movement
        [SerializeField] private Transform pos1;
        [SerializeField] private Transform pos2;
        public float speed = 1.0f;
 
        void Update()
        {
            transform.position += new Vector3(1, 0, 0);
            //= Vector3.Lerp (pos1.position, pos2.position, Mathf.PingPong(Time.time*speed, 1.0f));
        }
    #endregion
        
        public void EnemyDeath()
        {
            animator.SetTrigger(death);
        }

        public void DestroyThisEnemy()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}