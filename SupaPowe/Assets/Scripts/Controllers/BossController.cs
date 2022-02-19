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
                _isAttackWaiting = true;
                break;

            case BossState.Attack:
                _isAttackWaiting = false;
                break;

            case BossState.PrepareDefend:
                _isAttackWaiting = true;
                break;

            case BossState.Defend:
                _isDefendWaiting = false;
                break;
        }

    }


    public void GetDamage(float damage)
    {
        health -= damage;
        ChangeBossState(BossState.PrepareDefend);
    }


    public void Attack()
    {
        int swordPlace = (int)(Random.Range(0, 3));
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
        

    }
}
