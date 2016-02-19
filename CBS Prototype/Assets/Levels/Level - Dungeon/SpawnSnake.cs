using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnSnake : MonoBehaviour {
     //>
    public int maxSnakesView = (int)UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES);
    public int snakeCounter = 0;
    public int numSnakes = 2;
    public bool isTriggered;
    //public bool isTrap;

    static private int snakeTrapsCounter = 0;
    //public GameObject guitext;
	// Use this for initialization
	void Start () {

        snakeTrapsCounter++;
    }
	
	// Update is called once per frame
    public Rigidbody BaseSnake; //>

    void OnLevelWasLoaded(int level)
    {
        snakeTrapsCounter = 0;
    }

	void Update () {
        if(isTriggered)
            spawnSnake();
	}
    void spawnSnake(int numSnakes)
    {
        if (UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES) <= 0)
            return;
        if (snakeCounter < numSnakes)
        {
            Rigidbody clone;

            clone = Instantiate(BaseSnake, transform.position, transform.rotation) as Rigidbody;
            clone.name = "snake" + snakeCounter;
            if (PlayerSpawner.playerInst)
                clone.GetComponent<MoveTo>().goal = PlayerSpawner.playerInst.transform;
            snakeCounter++;
            /* Text text = guitext.GetComponent<Text>();
            text.text = snakeCounter.ToString();*/
        }
    }

    void spawnSnake()
    {
        if (UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES) <= 0)
            return;
        if (snakeCounter <= UISlider.GetSliderValue(UISlider.SliderType.NUM_OF_SNAKES) / snakeTrapsCounter)
        {
            Rigidbody clone;

            clone = Instantiate(BaseSnake, transform.position, transform.rotation) as Rigidbody;
            clone.name = "snake" + snakeCounter;
            if (PlayerSpawner.playerInst)
                clone.GetComponent<MoveTo>().goal = PlayerSpawner.playerInst.transform;
            snakeCounter++;
            /* Text text = guitext.GetComponent<Text>();
            text.text = snakeCounter.ToString();*/
        }
    }
}
