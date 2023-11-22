using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private RectMask2D mask;
    [SerializeField] private RectTransform rectTransform;

    public void SetBarValue(float _value, float _maxValue)
    {
        mask.padding = new Vector4(mask.padding.x, mask.padding.y, (_maxValue - _value) / _maxValue * rectTransform.rect.width, mask.padding.w);
    }
}
