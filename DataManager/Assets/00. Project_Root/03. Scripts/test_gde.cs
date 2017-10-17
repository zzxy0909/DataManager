using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using GameDataEditor;

public class test_gde : MonoBehaviour {

    public GDEuser_infoData m_GDEuser_infoData;

    protected void InitGDE()
    {
        if (m_GDEuser_infoData != null )
            return;

        if (!GDEDataManager.Init("gde_data"))
        {
            Debug.LogError(SetDataSceneStrings.ErrorInitializing);
        }
        else
        {
            LoadItems();
        }
    }
    void LoadItems()
    {
         m_GDEuser_infoData = new GDEuser_infoData(GDEItemKeys.user_info_1);
        
    }
    void Awake()
    {
        InitGDE();
    }

    // Use this for initialization
    void Start () {

        //m_GDEuser_infoData.nickname = "연습1";

        //m_GDEuser_infoData.SaveToDict();
    }
	
	// Update is called once per frame
	void Update () {
        // This is only here so I don't have to stop and start each time Unity recompiles
        InitGDE();

    }
}
