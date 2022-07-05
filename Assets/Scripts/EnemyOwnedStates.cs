using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ����
/// </summary>
namespace EnemyOwnedStates 
{
    public class Idle : State //Idle �϶� ����Ǵ� ���� 
    {
        public override void Enter(Enemy entity) // �����ൿ
        {
            entity.PrintText("����");
            entity.HP = entity.maxHp;
        }

        public override void Execute(Enemy entity) // �����ൿ
        {
            if(entity.HP > 0)
            {
                entity.HP -= 10;
                Debug.Log(entity.HP);
            }
            else
            {
                GameController.Stop(entity);
                return;
            }
        }

        public override void Exit(Enemy entity) // ���� �ൿ 
        {
            entity.PrintText("������");
        }
    }

    public class Walk : State
    {
        public override void Enter(Enemy entity)
        {
            Debug.Log("�ȴ´�");
        }

        public override void Execute(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(Enemy entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Run : State
    {
        public override void Enter(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(Enemy entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Attack : State
    {
        public override void Enter(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(Enemy entity)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Shout : State
    {
        public override void Enter(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Execute(Enemy entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Exit(Enemy entity)
        {
            throw new System.NotImplementedException();
        }
    }

}


