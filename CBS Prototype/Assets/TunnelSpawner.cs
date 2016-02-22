using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum LevelType
{
    Dungeon, 
    Egypt,
    Park
}

public class TunnelSpawner : MonoBehaviour {

    static public LevelType levelType;

    public const int Max_Tunnel = 100;
    const int NUMBER_OF_TUNNELS = 4;        //1 MORE THAN THE NUMBER OF TUNNELS IN RESOURCES FOLDER
    public float tunnelSpeed = 0.1f;
    public bool tunnelStarted = false;
    public bool snakeSpawned = false;
    public bool tunnelMode = false;
    int tunnelName = 0;


    GameObject tunnelPlayer;

    List<GameObject> tunnel;
    Quaternion nullQuart;
    GameObject tempOBJ;
    public GameObject tempSnake;

	// Use this for initialization
	void Start () {
        if (!tunnelStarted)
        {
            if (!snakeSpawned)
            {
                tempSnake.transform.position = gameObject.transform.position;
                tempSnake.transform.Translate(-5, 0, 0);
                Instantiate(tempSnake);
                snakeSpawned = true;
            }
            tunnel = new List<GameObject>();

            spawnTunnelPiece();



          //  print("Started tunnel 0");
            //tempOBJ = (Resources.Load("TunnelSections/" + Random.Range(1, NUMBER_OF_TUNNELS)) as GameObject);
          //  tempOBJ.transform.position = transform.position;
         //   tunnel.Add(Instantiate(tempOBJ));
           // tunnel[0].name = ("tunnel " + 0);
            //tunnel[0].transform.position.Set()
          //  print("created tunnel 0");

            for (int i = 1; i < Max_Tunnel; i++)
            {
                spawnTunnelPiece();
              //  print("Started tunnel " + i);
             //   tempOBJ = (Resources.Load("TunnelSections/" + Random.Range(1, NUMBER_OF_TUNNELS)) as GameObject);
             //   tunnel.Add(Instantiate(tempOBJ));
             //   tunnel[i].name = ("tunnel " + i);
             ////  print("created tunnel" + i);
             //   tunnel[i].transform.position = tunnel[i - 1].transform.position;
             //   tunnel[i].gameObject.transform.Translate(((tunnel[i].gameObject.GetComponent<Renderer>().bounds.extents.x + tunnel[i - 1].gameObject.GetComponent<Renderer>().bounds.extents.x)), 0, 0);
             // //  print("Max Tunnel = " + Max_Tunnel + " tunnel count = " + tunnel.Count);

            }
        }

	}
	
	// Update is called once per frame
    void Update()
    {
        if (tunnelMode)
        {
            /*
            foreach(GameObject t in tunnel)
            {
                t.transform.Translate(Vector3.left);
            }
             * */
            for (int i = 0; i < tunnel.Count; i++)
            {
                tunnel[i].transform.Translate(Vector3.left * tunnelSpeed * Time.timeScale);
                if (tunnel[i].transform.position.x < transform.position.x - 100)
                {
                    Destroy(tunnel[0]);
                    tunnel.RemoveAt(0);
                    i--;
                    //print(tunnel.Count);

                }
            }
            //print("tunnel count/Max Tunnel " + tunnel.Count + " " + Max_Tunnel);
            print("Max tunnel: " + Max_Tunnel + "   tunnel count " + tunnel.Count);
            if (tunnel.Count < Max_Tunnel)
            {
                spawnTunnelPiece();

            }


        }
    }

    void spawnTunnelPiece()
    {
        print("WE GOT THERE");

        

        int tempInt = Random.Range(1, NUMBER_OF_TUNNELS);

        // For initial tunnel piece placement
        //if (tunnel.Count == 0)
        //    tempInt = 0;

        print(tempInt);
        tempOBJ = (Resources.Load("TunnelSections/" + levelType + "/" + tempInt) as GameObject);
        float offset = 0;
        if (tunnel.Count == 0)
        {
            tempOBJ.transform.position = transform.position;
        }
        else
        {
            tempOBJ.transform.position = tunnel[tunnel.Count - 1].transform.position;
            offset = tempOBJ.gameObject.GetComponent<Renderer>().bounds.extents.x + tunnel[tunnel.Count - 1].gameObject.GetComponent<Renderer>().bounds.extents.x;
        }
        tempOBJ.name = ("tunnel " + (tunnelName));
        tunnelName++;
        

            
        tempOBJ.gameObject.transform.Translate(offset, 0, 0);
        tunnel.Add(Instantiate(tempOBJ));

        //foreach (Transform trans in transform)
        //{
        //    Debug.Log("LOLOLOLOL");
        //    trans.gameObject.AddComponent<TunnelObstacleCollide>();
        //}

        //foreach (TunnelObstacleCollide obstacle in tempOBJ.transform.GetComponentsInChildren<TunnelObstacleCollide>())
        //{
        //    obstacle.SnakeChase = tempSnake;
        //}
    }
}
