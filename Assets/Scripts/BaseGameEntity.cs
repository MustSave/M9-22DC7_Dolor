using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEntity : MonoBehaviour
{
    //����(���, reality)�� �ν� ��ü�κ��� ������ ���������� �����Ѵٰ� �������� ���� ���Ѵ�
    //������ȿ�ѾƵ�
    private static int nextValidID = 0;

    //BaseGameEntity�� ��ӹ޴� ��� ���ӿ�����Ʈ�� ID��ȣ�� �ο��޴µ�
    //�� ��ȣ�� 0���� �����ؼ� 1�� ���� (������ �ֹε�Ϲ�ȣó�� ���)
    private int id;

    public int ID
    {
        set
        {
            id = value;
            nextValidID++;
        }
        get
        {
            return id;
        }
    }

    private string entityName; // ������Ʈ �̸� 

    /// <summary>
    /// �Ļ� Ŭ�������� base.SetUp()���� ȣ�� 
    /// </summary>
    /// <param name="agent �̸�"></param>
    public virtual void SetUp(string name)
    {
        ID = nextValidID;

        entityName = name;

        PrintText(ID, name);
    }

    //GameController Ŭ�������� ��� ������Ʈ Updated()�� ȣ���� ������Ʈ�� �����Ѵ�.
    public abstract void Updated();

    public void PrintText(int id, string name)
    {
        Debug.Log(ID + " : " + entityName);
    }

    public void PrintText(string text)
    {
        Debug.Log(text);
    }
}
