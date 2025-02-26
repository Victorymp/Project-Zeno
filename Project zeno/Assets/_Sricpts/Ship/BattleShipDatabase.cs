using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BattleShipDatabase : ScriptableObject
{
    public BattleShipTemplate[] battleShips;

    public int BattleShipCount
    {
        get
        {
            return battleShips.Length;
        }
    }

    public BattleShipTemplate GetBattleShip(int index)
    {
        return battleShips[index];
    }
}
