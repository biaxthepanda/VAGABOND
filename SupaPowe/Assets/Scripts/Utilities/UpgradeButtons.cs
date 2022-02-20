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

        DOVirtual.DelayedCall(0.5f, () => _upgrades.SetActive(true));
    }


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

    private void NextLevel() {
        GameManager.Instance.ChangeState(GameManager.GameState.SceneChange);
    }


}
