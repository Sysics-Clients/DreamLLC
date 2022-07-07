using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseat : MonoBehaviour
{
    public ListItems listitemsPrefab;
    int i = 0;
    int j = 0;
    int k=0;
    int l = 0;
    public GameObject SingletonObj;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("verifshop")==0)
        {
            foreach (var item in listitemsPrefab.items)
            {
                if (item.price == 0)
                {
                    switch (item.type)
                    {
                        case ItemTypes.AK:
                            item.price = 0;
                            item.state = StateItem.current;
                            item.nbVideo = 0;
                            break;
                        case ItemTypes.knife:
                            item.price = 0;
                            item.state = StateItem.current;
                            item.nbVideo = 0;
                            break;
                        case ItemTypes.Pistol:
                            item.price = 0;
                            item.state = StateItem.current;
                            item.nbVideo = 0;
                            break;

                        case ItemTypes.legs:
                            item.price = 0;
                            l++;
                            item.state = StateItem.current;
                            item.nbVideo = 0;


                            break;
                        case ItemTypes.Boots:
                            item.price = 0;
                            k++;
                            item.state = StateItem.current;
                            item.nbVideo = 0;


                            break;
                        case ItemTypes.Chest:
                            item.price = 0;
                            i++;
                            item.state = StateItem.current;
                            item.nbVideo = 0;


                            break;
                        case ItemTypes.Casque:
                            item.price = 0;
                            j++;
                            item.state = StateItem.current;
                            item.nbVideo = 0;


                            break;

                        default:
                            break;
                    }
                }
                if (item.price != 0)
                {

                    //item.nbVideo = item.price / 50;
                    switch (item.type)
                    {
                        case ItemTypes.AK:
                            if (item.price >= 50)
                            {
                                item.price = item.price / 3;
                                item.state = StateItem.toBuy;
                                item.nbVideo = item.price / 30;
                                if (item.nbVideo < 1)
                                {
                                    item.nbVideo = 1;
                                }
                                else if (item.nbVideo > 3)
                                {
                                    item.nbVideo = 3;
                                }

                            }

                            break;
                        case ItemTypes.knife:
                            if (item.price >= 50)
                            {
                                item.price = item.price / 3;
                                item.state = StateItem.toBuy;
                                item.nbVideo = item.price / 30;
                                if (item.nbVideo < 1)
                                {
                                    item.nbVideo = 1;
                                }
                                else if (item.nbVideo > 3)
                                {
                                    item.nbVideo = 3;
                                }

                            }
                            break;
                        case ItemTypes.Pistol:
                            if (item.price >= 50)
                            {
                                item.price = item.price / 3;
                                item.state = StateItem.toBuy;
                                item.nbVideo = item.price / 30;
                                if (item.nbVideo < 1)
                                {
                                    item.nbVideo = 1;
                                }
                                else if (item.nbVideo > 3)
                                {
                                    item.nbVideo = 3;
                                }

                            }
                            break;
                        case ItemTypes.legs:
                            item.price = (item.price / 10) + (l * Random.Range(1, 5));
                            l++;
                            item.state = StateItem.toBuy;
                            item.nbVideo = item.price / 3;
                            if (item.nbVideo < 1)
                            {
                                item.nbVideo = 1;
                            }
                            else if (item.nbVideo > 3)
                            {
                                item.nbVideo = 3;
                            }
                            break;
                        case ItemTypes.Boots:
                            item.price = (item.price / 10) + (k * Random.Range(1, 5));
                            k++;
                            item.state = StateItem.toBuy;
                            item.nbVideo = item.price / 3;
                            if (item.nbVideo < 1)
                            {
                                item.nbVideo = 1;
                            }
                            else if (item.nbVideo > 3)
                            {
                                item.nbVideo = 3;
                            }
                            break;
                        case ItemTypes.Chest:
                            item.price = (item.price / 10) + (i * Random.Range(1, 5));
                            i++;
                            item.state = StateItem.toBuy;
                            item.nbVideo = item.price / 3;
                            if (item.nbVideo < 1)
                            {
                                item.nbVideo = 1;
                            }
                            else if (item.nbVideo > 3)
                            {
                                item.nbVideo = 3;
                            }
                            break;
                        case ItemTypes.Casque:
                            item.price = (item.price / 10) + (j * Random.Range(1, 5));
                            j++;
                            item.state = StateItem.toBuy;
                            item.nbVideo = item.price / 3;
                            if (item.nbVideo < 1)
                            {
                                item.nbVideo = 1;
                            }
                            else if (item.nbVideo > 3)
                            {
                                item.nbVideo = 3;
                            }
                            break;

                        default:
                            break;
                    }


                }
            }
            PlayerPrefs.SetInt("verifshop", 1);
            SingletonObj.SetActive(true);
            Canvas.SetActive(true);
        }
        else
        {
            SingletonObj.SetActive(true);
            Canvas.SetActive(true);
        }
        //Singleton._instance.save();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
