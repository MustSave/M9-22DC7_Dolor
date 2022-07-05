using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    /// <summary>
    /// �ش� ���¸� ���� �Ҷ� 1ȸ ȣ��
    /// </summary>
    public abstract void Enter(Enemy entity);
    /// <summary>
    /// �ش� ���¸� ������Ʈ �� �� ������ ȣ�� (�����ϴ�)
    /// </summary>
    public abstract void Execute(Enemy entity);
    /// <summary>
    /// �ش� ���¸� ������ �� 1ȸ ȣ�� 
    /// </summary>
    public abstract void Exit(Enemy entity);

}
