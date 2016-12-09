using UnityEngine;
using System.Collections;
using Parameter;

public class ItemGenerator : MonoBehaviour
{
    public GameObject extendPrefab;
    public GameObject shrinkPrefab;
   // public GameObject splitPrefab;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
        //アイテムを出現
        if (Param.Moving)
        {
            int num = Random.Range(0, 300);
            if (num == 0)
            {
                GameObject Extend = Instantiate(extendPrefab) as GameObject;
                Extend.transform.position = new Vector3(Random.Range(-3, 3), 4, 0);
            }
            else if (num == 1)
            {
                GameObject Shrink = Instantiate(shrinkPrefab) as GameObject;
                Shrink.transform.position = new Vector3(Random.Range(-3, 3), 4, 0);
            }
            /*else if (num == 2)
            {
                GameObject Split = Instantiate(splitPrefab) as GameObject;
                Split.transform.position = new Vector3(Random.Range(-3, 3), 4, 0);
            }*/

        }
    }
}
