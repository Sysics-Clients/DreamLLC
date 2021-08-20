using UnityEngine;

public class Destruction : MonoBehaviour
{
    public GameObject destructableBox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(destructableBox, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
