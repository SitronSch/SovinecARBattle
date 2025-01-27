using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeepButtonHighlighted : MonoBehaviour
{
    // Start is called before the first frame update
    private EventSystem eventS;
    private GameObject lastSelected = null;
    private bool locked = false;
    [SerializeField] UiCollapse uiCollapseLogic;

    private void Start()
    {
        eventS = GetComponent<EventSystem>();
    }
    private void Update()           //EventSystem bohuzel neobsahuje delegata onSelectionChange nebo neco podobneho, proto takto hnusne
    {
        if(lastSelected!=null)
        {
            if(!locked)
            {
                eventS.SetSelectedGameObject(lastSelected);
            }
        }    
    }
    public void MakeSelectionChange(GameObject i)
    {
        locked = true;
        lastSelected = i;
        eventS.SetSelectedGameObject(lastSelected);
        locked = false;
    }
}
