using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class allows us to specify how an interacted object will behave when placed in the inventory
public class CustomGameObject : MonoBehaviour {
    // Define a list of tokens
    //Enumeration was used because all possible values will be known at compile time
    public enum CustomObjectType
    {
        Invalid = -1,
        Unique = 0,
        Coin = 1,
        Ruby = 2,
        Emerald = 3,
        Diamond = 4
    }
    //This variable stores the current type of object
    public CustomObjectType objectType;

    public string displayName;
    // Assigns the name of object if its not defined
    public void Validate()
    {
        if (displayName == "")
            displayName = "unnamed_object";
    }

}
