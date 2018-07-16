using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMgr : MonoBehaviour {

    public List<InventoryItem> inventoryObjects = new List<InventoryItem>();
    public int numCells;
    public float height;
    public float width;
    public float yPosition;
    private MissionMgr missionMgr;
    // Use this for initialization
    void Start () {
        GameObject go = GameObject.Find("Game");
        if (go)
        {
            missionMgr = go.GetComponent<MissionMgr>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add(InteractiveObj iObj)
    {
        ObjectInteraction oi = iObj.OnCloseEnough;

        switch (oi.interactionType)
        {
            case (ObjectInteraction.InteractionType.Unique):
                {
                    // slot into first available spot
                    Insert(iObj);
                }
                break;

            case (ObjectInteraction.InteractionType.Accumulate):
                {
                    bool inserted = false;
                    // find object of same type, and increase
                    CustomGameObject cgo = iObj.gameObject.GetComponent
                    <CustomGameObject>();
                    CustomGameObject.CustomObjectType ot = CustomGameObject.
                    CustomObjectType.Invalid;
                    if (cgo != null)
                    {
                        ot = cgo.objectType;
                    }

                    for (int i = 0; i < inventoryObjects.Count; i++)
                    {
                        CustomGameObject cgoi = inventoryObjects[i].item.GetComponent<CustomGameObject>();
                        CustomGameObject.CustomObjectType io = CustomGameObject.CustomObjectType.Invalid;

                        if (cgoi != null)
                        {
                            io = cgoi.objectType;
                        }

                        if (ot == io)
                        {
                            inventoryObjects[i].quantity++;
                            // add token from this object to missionMgr
                            // to track, if this obj as a token
                            MissionToken mt = iObj.gameObject.GetComponent<MissionToken>();
                            if (mt != null)
                                missionMgr.Add(mt);
                            iObj.gameObject.SetActive(false);
                            inserted = true;
                        }

                    }
                }
                break;

            
        }
    }

    public void Insert(InteractiveObj iObj)
    {
        InventoryItem ii = new InventoryItem();
        ii.item = iObj.gameObject;
        ii.quantity = 1;
        ii.item.SetActive(false);
        inventoryObjects.Add(ii);

        MissionToken mt = ii.item.GetComponent<MissionToken>();
        if (mt != null)
        {
            missionMgr.Add(mt);
        }
    }

    public void DisplayInventory()
    {
        float sw = Screen.width;
        float sh = Screen.height;

        Texture buttonTexture = null;

        int totalCellsToDisplay = inventoryObjects.Count;
        for (int i = 0; i < totalCellsToDisplay; i++)
        {
            InventoryItem ii = inventoryObjects[i];
            buttonTexture = ii.displayTexture;
            int quantity = ii.quantity;

            float totalCellLength = sw - (numCells * width);

            float xcoord = totalCellLength - 0.5f * (totalCellLength) + (width * i);
            Rect r = new Rect(totalCellLength - 0.5f * (totalCellLength) +(width * i), yPosition * sh, width, height);

            if (GUI.Button(r, buttonTexture))
            {
                // to do – handle clicks there
            }
            Rect r2 = new Rect(totalCellLength - 0.5f * (totalCellLength) + (width * i), yPosition * sh, 0.5f * width, 0.5f * height);
            string s = quantity.ToString();
            GUI.Label(r2, s);

        }
    }

    public void OnGUI()
    {
        DisplayInventory();
    }
}
