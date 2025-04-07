using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OptimizeScroll : MonoBehaviour
{
    [SerializeField] private RectTransform viewPort;
    [SerializeField] private ScrollRect scrollRect;
    
    private const float ItemHeight = 110f; // includes spacing of 10
    private const float ItemSpacing = 10f;
    
    [SerializeField] private InventoryManager inventoryManager;
    
    private void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(HandleScroll);
    }
    
    private void HandleScroll(Vector2 value)
    {
        UpdateVisibleItems();
    }

#region TESTAREA

    bool ifOnce = false;


    
        // how many rows are there 
    int _rows,_rowsOnScreen;
    float _normalizedRowHeight;
    private void SetUpRowsData()
    {
        ifOnce = true;
        _rows = inventoryManager.inventoryRows.Length;
        _rowsOnScreen = (int)(viewPort.rect.height / ItemHeight);
        _normalizedRowHeight = 1/(float)(_rows - _rowsOnScreen);
    }

    private void UpdateVisibleItems()
    {
        // Implement your solution here
        // Access the array of inventory rows as needed: inventoryManager.inventoryRows
        if(!ifOnce) SetUpRowsData();


        var rowsArray = inventoryManager.inventoryRows;

        int whatRowIAmAt = _rows - _rowsOnScreen - (int)(scrollRect.verticalNormalizedPosition/_normalizedRowHeight);
        int whatToHideBefore = whatRowIAmAt -1;
        int WhatToHideAfter = whatRowIAmAt + 5; 
        /*
        // debug to get the proper outcome 
        string debugtext = "";
        for (int i = whatToHideBefore ; i > 0 ; i --)
        {
            debugtext += i +" / ";
        }
        Debug.Log(whatToHideBefore+" hide BEFORE "+ debugtext);


        debugtext = "";
        for (int o = WhatToHideAfter ; o <= rows ; o ++)
        {
            debugtext += o +" / ";
        }
        Debug.Log( WhatToHideAfter+" hide AFTER "+ debugtext);
        */
        foreach(var r in rowsArray)
        {
            r.SetActive(true);
        }
        for (int i = whatToHideBefore ; i > 0 ; i --)
        {
            rowsArray[i-1].SetActive(false);
        }
        for (int o = WhatToHideAfter ; o <= _rows ; o ++)
        {
            rowsArray[o-1].SetActive(false);
        }
    }

#endregion

}
