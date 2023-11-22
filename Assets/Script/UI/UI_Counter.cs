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
        SetCount(count.ToString());
    }

    public void SetCount(string _text)
    {
        text.text = _text;
    }

    public void Increment()
    {
        count++;
        SetCount(count.ToString());
    }
}
