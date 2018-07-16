using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class defines how an object will be manipulated once an interaction occurs
public class ObjectInteraction : MonoBehaviour {

    //Enum that specifies the action
    public enum InteractionAction
    {
        Invalid = -1,
        PutInInventory = 0,
        Use = 1,
    }

    //Enum that specifies what type of object 

    public enum InteractionType
    {
        Invalid = -1,
        Unique = 0,
        Accumulate = 1,
    }

    public InteractionAction interaction;
    public InteractionType interactionType;
    

    //A texture variable to store the icon of the object being stored
    public Texture tex;

    public void HandleInteraction()
    {
        InventoryMgr iMgr = null;
        GameObject player = GameObject.Find("Player");

        if(player)
        {
            iMgr = player.GetComponent<InventoryMgr>();
        }

        if (interaction == InteractionAction.PutInInventory)
        {
            if (iMgr)
                iMgr.Add(this.gameObject.GetComponent < InteractiveObj>());
        }

    }
   
}
