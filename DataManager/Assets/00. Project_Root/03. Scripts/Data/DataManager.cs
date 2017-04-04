using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public sealed class DataManager {

    public static string _DataBaseFileName_StartupBalanceDB = "demo_data_balance.db";
    public static string _DataBaseFileName_BalanceDB = "Balance_data_0001.db";

	static DataManager instance=null;
    static readonly object padlock = new object();
	DataManager()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
    }
	public static DataManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance==null)
                {
                    instance = new DataManager();
				}
                return instance;
            }
        }
    }
	
	public MonoDataManager _SetupDataManager = null;
	
    //=======================================================
    private SQLiteDB _db = null;
    
    public JSONObject Get_JsonDataRec_BalanceDB(string v_query)
    {
        var obj_rtn = new JSONObject();

        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        string filename = DataManager.Instance.GetDataFilePath_BalanceDB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            
            // UnityEngine.Debug.Log("~~~~~~~~~~ Get_JsonDataRec_BalanceDB()  v_query:" + v_query);
            qr = new SQLiteQuery(_db, v_query);
            // List<string> l_lst_row = new List<string>();
            JSONArray list_value = new JSONArray();
            while (qr.Step())
            {
                JSONObject obj_tmp = qr.GetRowJson();
                // l_lst_row.Add(strtmp);
                list_value.Add(obj_tmp);
            }
            obj_rtn.Add("rows", list_value );
            qr.Release();
            _db.Close();
        }
        catch(System.Exception e)
        {
            if (_db != null)
            {
                _db.Close();
                _db = null;
            }
            UnityEngine.Debug.LogError(e.ToString());
        }
        // Debug.Log("~~~~~~~~~~~~~~~~~~~~~~  obj_rtn:" + obj_rtn.ToString());
        return obj_rtn;
    }
    

//================================
	public string GetDataFilePath_StartupBalanceDB()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_StartupBalanceDB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_StartupBalanceDB;	           
#endif
		return rtn;
		
	}
	
    
	public string GetDataFilePath_BalanceDB()
	{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_BalanceDB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_BalanceDB;	           
#endif
		return rtn;
		
	}
	
}
