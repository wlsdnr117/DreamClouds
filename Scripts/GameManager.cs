using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int cloudnum = 0;
    private int randx, randy, randz;
    private int randcloud;
    public GameObject player;
    private float timecount = 0;
    private GameObject[] cloud= new GameObject[100];
    private int moveSpeed = 10;
    private Vector3 dirToTarget;
    

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        cloudMove();
        
    }

    void cloudMove()
    {
        timecount += Time.deltaTime;
        if(timecount>3&&cloudnum<99)
        {
            timecount = 0;
            randx = Random.Range(-100, 100);
            randy = Random.Range(50, 100);
            randz = Random.Range(-100, 100);
            randcloud = Random.Range(1,19);
            cloud[cloudnum]=Instantiate(Resources.Load("Prefabs/" + "cloud" + randcloud.ToString()), new Vector3(randx, randy, randz), Quaternion.identity)as GameObject;
            dirToTarget = player.transform.position - cloud[cloudnum].transform.position;
            cloud[cloudnum].transform.forward = dirToTarget.normalized;
            cloudnum++;
        }

        for(int i=0;i<cloudnum;i++)
        {
            cloud[i].gameObject.transform.Translate(cloud[i].transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
