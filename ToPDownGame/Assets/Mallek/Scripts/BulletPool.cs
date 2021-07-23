using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	public static BulletPool Instance;
	public GameObject objectToPool, objectToPoolPistol;
	public int nombreBullet, nombreBulletPistol;
    public List<GameObject> pooledObjects, pooledObjectsPistol;

    private void Awake()
    {
		Instance = this;
	}
    public void start()
	{
		for (int i = 0; i < nombreBullet; i++)
		{
			GameObject obj = Instantiate(objectToPool);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}

		for (int i = 0; i < nombreBulletPistol; i++)
		{
			GameObject obj = Instantiate(objectToPoolPistol);
			obj.SetActive(false);
			pooledObjectsPistol.Add(obj);
		}
	}

	public GameObject spownBullet(Vector3 pos,Vector3 direction)
    {
		GameObject gameObject = pooledObjects[0];
		pooledObjects.RemoveAt(0);
		gameObject.transform.position = pos;
		gameObject.transform.forward = direction;
		gameObject.SetActive(true);
		pooledObjects.Add(gameObject);
		return gameObject;
    }
	public GameObject spownBulletPistol(Vector3 pos, Vector3 direction)
	{
		GameObject gameObject = pooledObjectsPistol[0];
		pooledObjectsPistol.RemoveAt(0);
		gameObject.transform.position = pos;
		gameObject.transform.forward = direction;
		gameObject.SetActive(true);
		pooledObjectsPistol.Add(gameObject);
		return gameObject;
	}
}
