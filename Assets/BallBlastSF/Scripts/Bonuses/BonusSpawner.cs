using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private BonusObject[] _bonuses;
    [SerializeField] private StonesManager _stonesManager;

    public BonusObject SpawnRandomBonus(Vector2 parentPosition)
    {
        int index = Random.Range(0, _bonuses.Length);
        BonusObject bonusObject = Instantiate(_bonuses[index], new Vector3(parentPosition.x, parentPosition.y, 0), Quaternion.identity);
        bonusObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));

        return bonusObject;
    }

}
