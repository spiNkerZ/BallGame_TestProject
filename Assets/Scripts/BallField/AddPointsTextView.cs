using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddPointsTextView : MonoBehaviour
{
    [SerializeField] TextMeshPro textMesh;

    public void CreateAndPlayAnimation(int _count, Vector3 _position)
    {
        AddPointsTextView addPointsInstObject = Instantiate(this, _position, Quaternion.identity);

        addPointsInstObject.transform.localScale = Vector3.zero;

        addPointsInstObject.textMesh.text = $"+{_count}";

        addPointsInstObject.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            addPointsInstObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.Flash);
        });

        Destroy(addPointsInstObject.gameObject,3);
    }
}
