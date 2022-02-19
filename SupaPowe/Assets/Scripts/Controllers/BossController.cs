using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{

    public float health;
    public LayerMask pwLayerMask;
    public Transform[] swordPlaces;

    public float prepareAttackDuration;
    public float prepareDefendDuration;
    private float _timer;

    private bool _isDefendWaiting;
    private bool _isAttackWaiting;

    int defendPosition = 0;
    int attackPosition = 0;
    public BossState BState { get; private set; }

    

    private void Start()
    {
        ChangeBossState(BossState.PrepareAttack);
    }




    private void Update()
    {

        if (_isDefendWaiting)
        {
            _timer += Time.deltaTime;
            if(_timer >= prepareDefendDuration)
            {
                ChangeBossState(BossState.Defend);
                _timer = 0;
            }
        }

        if (_isAttackWaiting)
        {
            _timer += Time.deltaTime;
            if(_timer >= prepareAttackDuration)
            {
                ChangeBossState(BossState.Attack);
                Attack(3);
                _timer = 0;
            }
        }

    }





    public enum BossState
    {
        PrepareAttack = 0,
        Attack = 1,
        PrepareDefend = 2,
        Defend = 3,

    }

    public void ChangeBossState(BossState newState)
    {
        BState = newState;

        switch (newState)
        {
            case BossState.PrepareAttack:
                AttackPrepare();
                GameManager.Instance.ChangeState(GameManager.GameState.Defending);
                _isAttackWaiting = true;
                
                break;

            case BossState.Attack:
                
                _isAttackWaiting = false;
                ChangeBossState(BossState.PrepareDefend);
                break;

            case BossState.PrepareDefend:
                Defend();
                GameManager.Instance.ChangeState(GameManager.GameState.Attacking);
                _isDefendWaiting = true;
                break;

            case BossState.Defend:
                _isDefendWaiting = false;
                ChangeBossState(BossState.PrepareAttack);
                break;
        }

    }

    public void Defend()
    {
        //Defend animation is gonna play , player will see where the boss will be blocking
        defendPosition = (int)Random.Range(0, 3);
        
    }

    public void Block(int pos)
    {
        if(pos == defendPosition)
        {
            //Boss blocked the player
            ChangeBossState(BossState.Defend);
        }
        else
        {
            //Boss get damage
            GetDamage(25);
            ChangeBossState(BossState.Defend);
        }
    }
    

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        ChangeBossState(BossState.Defend);
    }

    public void Die()
    {
        //Death
        Debug.Log("Boss is dead");
    }


    public void AttackPrepare()
    {
        attackPosition = (int)(Random.Range(0, 3));

        //Attack animation is gonna play , player will see where the boss will be attacking
        
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.left,Mathf.Infinity,pwLayerMask);
        if (hit)
        {
            if(hit.transform.gameObject.tag == "Sword")
            {
                //Player dodges
                ChangeBossState(BossState.PrepareDefend);
            }
            else if(hit.transform.gameObject.tag == "Player")
            {
                //Player gets hit, 2 levels back

            }
        }
        */
    }

    public void Attack(int pos)
    {
        if(attackPosition == pos)
        {
            //Player Blocked The Boss Attack
            ChangeBossState(BossState.Attack);
        }
        else
        {
            //Player Couldn't Block The Boss Attack, Level Restart
        }
    }



}
