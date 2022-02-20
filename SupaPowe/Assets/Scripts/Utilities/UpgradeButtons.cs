using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
   


    public void SlowTimeUpgrade()
    {
        LevelManager.Instance.SlowTimeMultiplier *= 1.1f;
        Destroy(gameObject);
    }
    public void LineLengthUpgrade()
    {
        LevelManager.Instance.LineLengthMultiplier *= 1.3f;
        Destroy(gameObject);
    }
    public void BonusLineUpgrade()
    {
        LevelManager.Instance.BonusLine += 1;
        Destroy(gameObject);
    }


}
