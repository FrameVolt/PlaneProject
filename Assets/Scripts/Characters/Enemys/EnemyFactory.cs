using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {
    public enum EnemyType { normal, tank, boss}
    private static EnemyBase normalEnemy;
    private static EnemyBase tank;
    private static EnemyBase boss;

    private void Awake () {
        normalEnemy = Resources.Load<EnemyBase>("Prefabs/Enemys/NormalEnemy");
        tank = Resources.Load<EnemyBase>("Prefabs/Enemys/Tank");
        boss = Resources.Load<EnemyBase>("Prefabs/Enemys/Boss");
    }


    public static EnemyBase CreateEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.normal:
                return normalEnemy;
            case EnemyType.tank:
                return tank;
            case EnemyType.boss:
                return boss;
        }
        return normalEnemy;
    }
}
