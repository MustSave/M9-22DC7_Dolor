using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string[] arrayEnemys; //���ʹ̵��� �̸� �迭 
    [SerializeField]
    private GameObject enemyPrefab; //���ʹ� Ÿ���� ������

    //��� ��� ���� ��� ������Ʈ ����Ʈ 
    private List<BaseGameEntity> entitys;

    public static bool IsGameStop { set; get; } = false;

    private void Awake()
    {
        entitys = new List<BaseGameEntity>();

        for(int i =0; i< arrayEnemys.Length; i++)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy entity = clone.GetComponent<Enemy>();
            entity.SetUp(arrayEnemys[i]);

            entitys.Add(entity);
        }
    }

    private void Update()
    {
        if (IsGameStop == true) return;

        for(int i = 0; i< entitys.Count; i++)
        {
            entitys[i].Updated();
        }
    }

    public static void Stop(BaseGameEntity entity)
    {
        IsGameStop = true;

        entity.PrintText("�����մϴ�");
    }
}
