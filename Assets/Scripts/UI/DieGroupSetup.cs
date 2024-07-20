using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieGroupSetup : MonoBehaviour
{
    public DieGroup dieGroupToCreate = new DieGroup();
    public bool attackRoll;

    public void SetToHitType(int _toHitTypeIndex)
    {
        switch (_toHitTypeIndex)
        {
            case 0:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Standard;
                break;
            case 1:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Advantage;
                break;
            case 2:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Disadvantage;
                break;
        }

    }
    public void SetModifier(bool _toHit)
    {
        if (_toHit)
        {

        }
        else
        {

        }
    }
}
