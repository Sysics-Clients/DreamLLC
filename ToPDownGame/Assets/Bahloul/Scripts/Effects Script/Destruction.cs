using UnityEngine;
using DG.Tweening;

public class Destruction : MonoBehaviour
{
    int SHakeNumber=0;
    public GameObject destructableBox;
    private GameObject audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            transform.DOShakeRotation(0.5f,5,1,5);
            SHakeNumber++;
            if (SHakeNumber == 3)
            {
                DOTween.CompleteAll();
                audioManager.GetComponent<AudioManager>().PlaySound(AudioManager.Sounds.BoxDestruction);
                Instantiate(destructableBox, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
