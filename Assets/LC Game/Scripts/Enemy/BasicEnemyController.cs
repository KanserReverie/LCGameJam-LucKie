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

            if(pos1.position.x > pos2.position.x)
            {
                goingRight = true;
                gameObject.transform.localScale= new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
            }
            else
            {
                goingRight = false;
            }
        }

    #region Enemy Movement
        [SerializeField] private Transform pos1;
        [SerializeField] private Transform pos2;
        [SerializeField] private float speed = 1.0f;
        private Vector3 lastPosition;

        void FixedUpdate()
        {
            lastPosition = transform.position;
            transform.position = Vector3.Lerp(pos1.position, pos2.position, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

            if(transform.position.x < lastPosition.x && goingRight)
            {
                gameObject.transform.localScale= new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
                goingRight = false;
            }
            else if(transform.position.x > lastPosition.x && !goingRight)
            {
                gameObject.transform.localScale= new Vector3(-gameObject.transform.localScale.x,gameObject.transform.localScale.y,gameObject.transform.localScale.z);
                goingRight = true;
            }
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