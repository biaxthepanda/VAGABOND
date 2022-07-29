using DG.Tweening;
using TMPro;
using UnityEngine;

public class DialogueCanvas : Singleton<DialogueCanvas>
{
    [SerializeField]
    private TextMeshProUGUI _dialogue;
    [SerializeField]
    private GameObject _image;

    public void Dialogue(string s, float delay)
    {
        _image.SetActive(true);
        _dialogue.text = s;
        DOVirtual.DelayedCall(delay, () =>
        {
            _dialogue.text = "";
            
            _image.SetActive(false);
        });
    }
}
