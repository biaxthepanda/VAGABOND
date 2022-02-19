using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
   


    public void SlowTimeUpgrade()
    {
        LevelManager.Instance.slowTimeMultiplier *= 1.1f;
        Destroy(gameObject);
    }
    public void LineLengthUpgrade()
    {
        LevelManager.Instance.lineLengthMultiplier *= 1.3f;
        Destroy(gameObject);
    }
    public void BonusLineUpgrade()
    {
        LevelManager.Instance.bonusLine += 1;
        Destroy(gameObject);
    }


}
