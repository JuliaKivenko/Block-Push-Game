using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wobbler : MonoBehaviour
{
    [SerializeField] float wobbleHeight = 1f;
    [SerializeField] private float wobbleDuration = 1f;

    void Start()
    {
        transform.DOMoveY(transform.position.y + wobbleHeight, wobbleDuration).SetLoops(-1, LoopType.Yoyo);
    }

}
