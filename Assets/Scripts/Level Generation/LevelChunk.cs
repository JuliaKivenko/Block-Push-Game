using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelChunk : MonoBehaviour
{
    [SerializeField] private Transform chunkEnd;

    private float scrollSpeed;
    private float despawnPositionZ;

    public void SetUp(float scrollSpeed, Transform despawnTransform)
    {
        this.scrollSpeed = scrollSpeed;
        despawnPositionZ = despawnTransform.position.z;
    }
    public Transform GetChunkEnd() => chunkEnd;

    public void StartMoving()
    {
        transform.DOMoveZ(despawnPositionZ, scrollSpeed).SetEase(Ease.Linear).OnComplete(() => { gameObject.SetActive(false); });
    }
}
