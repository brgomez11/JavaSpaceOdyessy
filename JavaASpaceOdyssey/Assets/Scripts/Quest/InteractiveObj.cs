using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class enables simple animation and permits player interaction
public class InteractiveObj : MonoBehaviour {
    // Variables that allow interacted object to rotate along its axis
    public Vector3 rotAxis;
    public float rotSpeed;

    //reference to the object so there is no need to look up the object at runtime
    private CustomGameObject gameObjectInfo;

    //A reference to class that specifies what will happen when object interacts with player
    public ObjectInteraction OnCloseEnough;


    // Use this for initialization
    void Start () {
        //Searches for the CustomGameObject Script attached to an object
        gameObjectInfo = this.gameObject.GetComponent<CustomGameObject>();

        //If the object isn't null validate the object has a name
        if (gameObjectInfo)
        {
            gameObjectInfo.Validate();
        }
    }
	
	// Update is called once per frame
	void Update () {
        //rotates the object on its axis
        transform.Rotate(rotAxis, rotSpeed * Time.deltaTime);
    }

    //Method for when an collider of an object collides with another other
    public void OnTriggerEnter(Collider other)
    {
        //checks to see if the object being collided with is tagged player.
        if(other.gameObject.tag =="Player")
        {
            //check if the object isn't null
            if (OnCloseEnough != null)
            {
                //Invoke the method that handles putting the object in the inventory.
                OnCloseEnough.HandleInteraction();
            }
        }
        
    }
}
