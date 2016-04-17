using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TitleTween : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("Expand", 0);
    }

    void Expand()
    {
        transform.DOScale(1.1f, 3).OnComplete(Contract).SetEase(Ease.InOutSine);
    }

    void Contract()
    {
        transform.DOScale(1, 3).OnComplete(Expand).SetEase(Ease.InOutSine);
    }
}