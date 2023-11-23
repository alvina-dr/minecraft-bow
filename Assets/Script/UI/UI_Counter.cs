using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Counter : MonoBehaviour
{
    public int count = 0;
    public TextMeshProUGUI text;

    private void Start()
    {
        SetText(count.ToString());
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void Increment()
    {
        count++;
        SetText(count.ToString());
    }
}
