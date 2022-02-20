using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BossController : MonoBehaviour
{

    public float health;

    public float prepareAttackDuration;
    public float prepareDefendDuration;
    public float _timer;

    private bool _isDefendWaiting;
    private bool _isAttackWaiting;

    int defendPosition = 0;
    int attackPosition = 0;

    public bool isDead = false;

    public BossState BState { get; private set; }

    

    private void Start()
    {
        Transform _player = GameObject.FindWithTag("Player").transform;

        Vector3 dir = (transform.position - _player.position).normalized;;
        Vector3 slowStartPos = _player.position + dir * 4f;

        transform.DOMove(slowStartPos, 1).SetEase(Ease.Linear);

        DOVirtual.DelayedCall(1, () => ChangeBossState(BossState.PrepareAttack));
    }




    private void Update()
    {
        if (isDead) return;
        if (_isDefendWaiting)
        {
            _timer += Time.deltaTime;
            if(_timer >= prepareDefendDuration)
            {
                if(BState != BossState.Defend)
                {
                    ChangeBossState(BossState.Defend); 
                }
                _timer = 0;
            }
        }

        if (_isAttackWaiting)
        {
            _timer += Time.deltaTime;
            if(_timer >= prepareAttackDuration)
            {
                if(BState != BossState.Attack)
                {
                    ChangeBossState(BossState.Attack);
                    Attack(3);
                }
                
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
        Dead = 4,
    }

    public void ChangeBossState(BossState newState)
    {
        BState = newState;

        switch (newState)
        {
            case BossState.PrepareAttack:
                AttackPrepare();
                GameManager.Instance.ChangeState(GameManager.GameState.Attacking);
                _isAttackWaiting = true;
                
                break;

            case BossState.Attack:
                
                _isAttackWaiting = false;
                ChangeBossState(BossState.PrepareDefend);
                _timer = 0;

                break;

            case BossState.PrepareDefend:
                Defend();
                GameManager.Instance.ChangeState(GameManager.GameState.Attacking);
                _isDefendWaiting = true;
                break;

            case BossState.Defend:
                _isDefendWaiting = false;
                ChangeBossState(BossState.PrepareAttack);
                _timer = 0;

                break;
        }

    }

    public void Defend()
    {
        Debug.Log("Boss Defend'e hazýrlanýyor");

        //Defend animation is gonna play , player will see where the boss will be blocking
        defendPosition = (int)Random.Range(0, 3);
        Debug.Log("DefendPOS = " +defendPosition);
        _timer = 0;
    }

    public void Block(int pos)
    {
        if(pos == defendPosition)
        {
            Debug.Log("Boss Player'ý Blockladý");

            //Boss blocked the player
            _isDefendWaiting = false;
            ChangeBossState(BossState.Defend);
            _timer = 0;

        }
        else
        {
            //Boss get damage
            _isDefendWaiting = false;
            GetDamage(25);
            _timer = 0;

        }
    }
    

    public void GetDamage(float damage)
    {
        Debug.Log("Boss Damage Aldý");

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            ChangeBossState(BossState.Defend);
        }
        _timer = 0;

    }

    public void Die()
    {
        //Death
        Debug.Log("Boss is dead");
        isDead = true;
        ChangeBossState(BossState.Dead);
        _timer = 0;

    }


    public void AttackPrepare()
    {
        Debug.Log("Boss Saldýrmaya Hazýrlanýyor");

        attackPosition = (int)(Random.Range(0, 3));
        Debug.Log("AttackPOS = " + attackPosition);
        _timer = 0;

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
            Debug.Log("Player Boss'u Blockladý");
            //Player Blocked The Boss Attack
            _isAttackWaiting = false;
            ChangeBossState(BossState.Attack);
            _timer = 0;

        }
        else
        {
            _isAttackWaiting = false;
            Debug.Log("Player Öldü");
            _timer = 0;

            //Player Couldn't Block The Boss Attack, Level Restart
        }
    }



}
