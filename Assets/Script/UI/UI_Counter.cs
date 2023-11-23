using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class UI_Counter : MonoBehaviour
{
    public int count = 0;
    public TextMeshProUGUI text;

    public void SetText(string _text)
    {
        transform.DOScale(1.1f, .1f).OnComplete(() =>
        {
            text.text = _text;
            transform.DOScale(1f, .1f);
        });
    }

    public void Increment(int _score)
    {
        count += _score;
        SetText("SCORE : " + count.ToString());

    }
}
