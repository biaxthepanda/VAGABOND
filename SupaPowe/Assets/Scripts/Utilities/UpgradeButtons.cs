using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{

    [SerializeField]
    private GameObject _upgrades;

    private void OnEnable() {
        GameManager.OnGameStateChanged += ActivateUpgrade;
    }

    private void OnDisable() {
        GameManager.OnGameStateChanged -= ActivateUpgrade;
    }

    private void ActivateUpgrade(GameManager.GameState state) {

        if (state != GameManager.GameState.Upgrade) return;

        //if random bla bla return;

        DOVirtual.DelayedCall(1f, () => _upgrades.SetActive(true));
    }


    public void SlowTimeUpgrade()
    {
        LevelManager.Instance.SlowTimeMultiplier *= 1.1f;
        NextLevel();
    }
    public void LineLengthUpgrade()
    {
        LevelManager.Instance.LineLengthMultiplier *= 1.1f;
        NextLevel();
    }
    public void BonusLineUpgrade()
    {
        LevelManager.Instance.BonusLine += 1;
        NextLevel();
    }

    private void NextLevel() {
        _upgrades.SetActive(false);
        GameManager.Instance.ChangeState(GameManager.GameState.SceneChange);
    }


}
