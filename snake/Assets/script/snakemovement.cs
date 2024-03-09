using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class snakemovement : MonoBehaviour
{
    //variables 

    //direction variable 

    private Vector2 direction;

    bool goingUp;
    bool goingDown;
    bool goingLeft;
    bool goingRight;
   
    public Transform bodyPrefab; //place to store the body
    List<Transform> segments; //list of body parts

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();       //create a new list

        segments.Add(transform);                //add the head of the snake to the list

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && goingDown != true)
        {
            direction = Vector2.up;
            //mark as going up 
            goingUp = true;
            goingDown = false;
            goingLeft = false;
            goingRight = false;
        }

        else if (Input.GetKeyDown(KeyCode.A) && goingRight == false)
        {
            direction = Vector2.left;
            //mark as going left
            goingUp = false;
            goingLeft = true;
            goingDown = false;
            goingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && goingUp != true)
        {
            direction = Vector2.down;
            //mark as going down
            goingUp = false;
            goingDown = true;
            goingLeft = false;
            goingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && goingLeft == false)
        {
            direction = Vector2.right;
            //mark as going right
            goingUp = false;
            goingDown = false;
            goingLeft = false;
            goingRight = true;
        }
    }

    void FixedUpdate(){

        for (int i = segments.Count -1; i > 0; i--)
        {
            segments[i].position = segments [i - 1].position;
        }

        transform.position = new Vector2
        (Mathf.Round(transform.position.x) + direction.x,
        Mathf.Round(transform.position.y) + direction.y);
    }
    void Grow(){
        Transform segment = Instantiate(bodyPrefab);

        segment.position = segments[segments.Count -1].position;
        segments.Add(segment);
    }

    

    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "Food")
        {
            Debug.Log("hit");
            Grow();

            Time.fixedDeltaTime -= 0.001f;
        }

        else if (other.tag == "Obstacle"){
            SceneManager.LoadScene("endscene");
        }
    }


}
