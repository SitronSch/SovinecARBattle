using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTarget : MonoBehaviour
{    
    [SerializeField] List<string> tooltipValues = new List<string>();
    [SerializeField] List<int> bubbleScale = new List<int>();
    TextMeshProUGUI currentText;
    ToolTip tt;

    void Start()
    {
        tt = GameObject.Find("Tooltip(DO NOT CHANGE NAME)").GetComponent<ToolTip>();
        currentText = GetComponent<TextMeshProUGUI>();
        TapToPlace.anyInputDetected += ElementTouched;
    } 
    public void ElementTouched(List<RaycastResult>result, Vector2 position)
    {       
        foreach (RaycastResult i in result)
        {
            if(i.gameObject==gameObject)
            {
                int link = TMP_TextUtilities.FindIntersectingLink(currentText, position, null);
                if (link != -1)
                {
                    string actualLinkId = currentText.textInfo.linkInfo[link].GetLinkID();      //Every link needs to have unique ID set in textmesh pro, because of changing subtitles
                    
                    tt.ShowTooltip(tooltipValues[int.Parse(actualLinkId)], position, bubbleScale[int.Parse(actualLinkId)]);
                    return;
                }
            }
        }      
    }
}
