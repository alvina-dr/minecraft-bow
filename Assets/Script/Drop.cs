using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject lootObject;
    [SerializeField] private ItemData itemData;
    [SerializeField] private int num;

    public void SetupDrop(ItemData _data, int _num)
    {
        itemData = _data;
        num = _num;
        lootObject = Instantiate(itemData.lootPrefab, transform);
        lootObject.transform.localPosition = Vector3.zero;
        lootObject.transform.DOLocalRotate(Vector3.up * 360f, 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        lootObject.transform.DOLocalMoveY(.2f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehavior _player = other.GetComponent<PlayerBehavior>();
        if (_player != null)
        {
            _player.inventory.AddItem(itemData, num);
            _player.pickUpSound.Play();
            lootObject.transform.DOKill();
            Destroy(this.gameObject);
        }
    }
}
