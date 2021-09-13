
using UnityEngine;
using DG.Tweening;
public class PadController : MonoBehaviour
{
    public Transform spriteTransform;
    public Ease ease;
    public GameObject PadObject;
    public Collider thisColloder;
    public GameObject Canvas;
    public RectTransform ImageText;
    public Ease ImageEase;
    public GameObject ChatSreen;
    void Start()
    {
        float y = spriteTransform.localPosition.y;
       //spriteTransform.DOShakeRotation(1, 10, 3).SetEase(ease).SetLoops(100);
        Sequence mySequence = DOTween.Sequence();
        /*mySequence.Append(transform.DOMoveX(45, 1))
          .Append(transform.DORotate(new Vector3(0, 180, 0), 1))
          .PrependInterval(1)
          .Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));*/
        /*mySequence.Append(spriteTransform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1))
                  .Append(spriteTransform.DOShakeRotation(1, 10, 3))
                  .PrependInterval(1)
                  .Append(spriteTransform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1))
                  .SetEase(ease)
                  .SetLoops(-1,LoopType.Yoyo);*/
        spriteTransform.DOMoveY(y + 1.5f, 1.5f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            spriteTransform.DOTogglePause();
            spriteTransform.gameObject.SetActive(false);
             PadObject.SetActive(false);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(ImageText.DOScaleY(1, 0.3f)).SetEase(ImageEase);
            mySequence.OnComplete(() =>
            {
                Time.timeScale = 0;
                //PadObject.SetActive(false);
            });

            
            thisColloder.enabled = false;
            Canvas.SetActive(true);
            GameObject canvasInput = GameObject.FindObjectOfType<InputSystem>().gameObject;
            canvasInput.SetActive(false);
        }
    }
    public void SkipButton()
    {
        Time.timeScale = 1;
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(ImageText.DOScaleY(0, 0.3f)).SetEase(Ease.InBack);
        mySequence.OnComplete(() =>
        {
            if (ChatSreen!=null)
            {
                ChatSreen.SetActive(true);
            }
            else
            {
                GameObject canvasInput = GameObject.FindObjectOfType<InputSystem>().gameObject;
                canvasInput.SetActive(true);
            }
            Destroy(gameObject);
            //PadObject.SetActive(false);
        });

    }
}
