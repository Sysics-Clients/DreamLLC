using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Canvs : MonoBehaviour
{
    public Text text;
    public Text EnemyName;
    public Ease simpleEase;
    // Start is called before the first frame update

    private void OnEnable()
    {
        GeneralEvents.addCounter += AddcounterText;
        GeneralEvents.ennemyDown += enmyDown;

    }

    private void OnDisable()
    {
        GeneralEvents.addCounter -= AddcounterText;
        GeneralEvents.ennemyDown -= enmyDown;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddcounterText()
    {
        int i = int.Parse(text.text);
        i++;
        text.text = i.ToString();
    }

    void enmyDown(string name)
    {
        EnemyName.gameObject.SetActive(true);
        
        EnemyName.text = name;
        RectTransform rect = EnemyName.rectTransform;
        EnemyName.rectTransform.DOAnchorPosY(20, 1).SetEase(simpleEase).OnComplete(() =>
        {
            EnemyName.rectTransform.position = new Vector3(78,0,0);
            EnemyName.gameObject.SetActive(false);
        });
    }
}
