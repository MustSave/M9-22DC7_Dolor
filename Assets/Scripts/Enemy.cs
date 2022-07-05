using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyStates
{
    Idle,
    Walk,
    Run,
    Attack,
    Shout
}

public class Enemy : BaseGameEntity
{
    //���� Hp�� 0�� �Ǹ� ��
    public int maxHp;
    private int hp;
    private int fatigue;
    private int damage;

    //enemy �� ������ �ִ� ��� ���� ,�������
    private State[] states;
    private State currentState;

    public int Fatigue
    {
        set => this.fatigue = Mathf.Max(0, value);
        get => this.fatigue;
    }

    public int HP
    {
        set
        {
            if (value >= this.maxHp)
            {
                value = this.maxHp;
            }
            this.hp = value;
        }
        get
        {
            if (this.hp <= 0)
            {
                return 0;
            }
            return this.hp;
        }

    }

    public override void SetUp(string name)
    {
        //��� Ŭ������ Setup �޼��� ȣ��(ID, �̸�, ���� ����)
        base.SetUp(name);

        this.gameObject.name = $"{ID:D2}_Enemy_{name}";
        this.maxHp = 100;
        this.damage = 10;

        //Enemy�� ���� �� �ִ� ���� ���� ��ŭ �޸� �Ҵ�, �� ���¿� Ŭ���� �޸� �Ҵ� 
        states = new State[5];
        states[(int)eEnemyStates.Idle] = new EnemyOwnedStates.Idle();
        states[(int)eEnemyStates.Walk] = new EnemyOwnedStates.Walk();
        currentState = states[(int)eEnemyStates.Idle];

        ChangeState(eEnemyStates.Idle);

    }

    public override void Updated()
    {
       if(currentState != null)
        {
            currentState.Execute(this);
        }
    }

    public void ChangeState(eEnemyStates newState)
    {
        if (states[(int)newState] == null) return;

        if(currentState != null)
        {
            this.currentState.Exit(this);
        }
        currentState = states[(int)newState];
        currentState.Enter(this);
    }
}
