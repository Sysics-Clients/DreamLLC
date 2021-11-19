using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollecteCard : MonoBehaviour
{
    public CardType cardType;
    public Transform SpriteTransform;
    public Ease ease;
    private void Start()
    {
        //float y = SpriteTransform.localPosition.y;
       // SpriteTransform.DOMoveY(y -3, 1.5f).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        StartCoroutine(waitToActive());
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        
    }
    IEnumerator waitToActive()
    {
        
        yield return new WaitForSeconds(2);

        GetComponent<MeshRenderer>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cardType == CardType.accessCard)
            {
                SpriteTransform.DOTogglePause();
                SpriteTransform.gameObject.SetActive(false);
                GeneralEvents.onTaskFinish(gameObject.GetComponent<MissionObjects>().missionName, gameObject.GetComponent<MissionObjects>().id);
                GeneralEvents.writeErrorMessage("Access Card Collected! Door is Opened now!", Color.green);
                GeneralEvents.hideErreurMessage(4);
            }
            else if(cardType == CardType.MedicalCard)
            {
                GeneralEvents.onTaskFinish(gameObject.GetComponent<MissionObjects>().missionName, gameObject.GetComponent<MissionObjects>().id);
                if (!GeneralEvents.testAllCompletion())
                {
                    GeneralEvents.writeErrorMessage("Medical box Collected!", Color.green);
                    GeneralEvents.hideErreurMessage(4);
                }
            }
            Destroy(gameObject);
        }
    }
}
public enum CardType
{
    accessCard,MedicalCard
}
