using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinShine : MonoBehaviour
{
    public Transform shine;
    public float offset;
    public float speed;
    public float minDelay;
    public float maxDelay;



    // Start is called before the first frame update
    void Start()
    {
        animate();
    }

  
    void animate()
    {
        shine.DOLocalMoveX(offset, speed).SetEase(Ease.Linear).SetDelay(Random.Range(minDelay, maxDelay)).OnComplete(() =>
        {
            shine.DOLocalMoveX(-offset, 0);
            animate();
        });
    }
}
