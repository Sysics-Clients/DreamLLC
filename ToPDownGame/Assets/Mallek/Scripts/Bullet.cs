using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=20;

    private void OnEnable()
    {
        StartCoroutine(stop());
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "enemy")
        {
            collision.gameObject.GetComponent<EnemyBehavior>().takeDamage(10);
        }
        this.gameObject.SetActive(false);
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
