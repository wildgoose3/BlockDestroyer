using UnityEngine;
using System.Collections;

using Parameter;

public class BrickGenerator : MonoBehaviour {

    public int Stage=1;
    public GameObject Brick1Prefab;
    public GameObject Brick2Prefab;
    public GameObject Brick3Prefab;
    public GameObject Brick4Prefab;
    public GameObject Brick5Prefab;
    public GameObject HardBlockPrefab;

    // Use this for initialization
    void Start () {
	if (Stage == 1)
        {
            for (float j = 2; j <= 3.5; j+=0.5f)
            {
                for (int i = -3; i <= 3; i++)
                {
                    GameObject Brick1 = Instantiate(Brick1Prefab) as GameObject;
                    Brick1.transform.position = new Vector3(i, j, 0);
                    Param.BlockRest += 1;

                    GameObject Brick2 = Instantiate(Brick2Prefab) as GameObject;
                    Brick2.transform.position = new Vector3(i, j+0.25f, 0);
                    Param.BlockRest += 1;

                }
            }
            
                /*for (int i = -3; i <= -1; i++)
                {
                    GameObject HardBlock = Instantiate(HardBlockPrefab) as GameObject;
                    HardBlock.transform.position = new Vector3(i, 1.75f, 0);
                }
            */
        }
	}
	
	// Update is called once per frame
	void Update ()
{
	
	}
}
