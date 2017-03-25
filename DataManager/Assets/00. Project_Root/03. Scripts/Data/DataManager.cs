using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;

public sealed class DataManager {

    public static string _DataBaseFileName_StartupBalanceDB = "demo_data_balance.db";
    public static string _DataBaseFileName_BalanceDB = "Balance_data_0001.db";

    public static string _DataBaseFileName_StartupSaveDB = "demo_data_save.db";
    public static string _DataBaseFileName_SaveDB = "Save_data_0001.db";

    public static string _DataBaseFileName_StartupShare01DB = "data_share01.db";
    public static string _DataBaseFileName_Share01DB = "data_share01_0001.db";

    public static string _DataBaseFileName_StartupTestFile = "001.png";

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
	public SqlSavePlayerData _SqlSavePlayerData = new SqlSavePlayerData();
    public SqlSavedata_info _SqlSavedata_info = new SqlSavedata_info();
    public SqlBalance_level_info _SqlBalance_level_info = new SqlBalance_level_info();
    public SqlSavedata_zonedata _SqlSavedata_zonedata = new SqlSavedata_zonedata();
    public SqlDataShare01_active_zonedata _SqlDataShare01_active_zonedata = new SqlDataShare01_active_zonedata();
    public SqlBalance_zoneobject_point _SqlBalance_zoneobject_point = new SqlBalance_zoneobject_point();
    public Sql_bt_drink_info _Sql_bt_drink_info = new Sql_bt_drink_info();
    public Sql_bt_drink_material _Sql_bt_drink_material = new Sql_bt_drink_material();

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
            
            UnityEngine.Debug.Log("~~~~~~~~~~ Get_JsonDataRec_BalanceDB()  v_query:" + v_query);
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
        Debug.Log("~~~~~~~~~~~~~~~~~~~~~~  obj_rtn:" + obj_rtn.ToString());
        return obj_rtn;
    }

    public void Update_pos_slot(int n, float x, float y)
    {
        string data_code = "pos_slot" + n;
        string strdata = string.Format("{0:0.00}|{1:0.00}", x, y);

        _SqlSavedata_info.Update_str_value(strdata, data_code);
    }
    public Vector2 Get_pos_slot(int n)
    {
        Vector2 rtn = Vector2.zero;
        string data_code = "pos_slot" + n;
        string strdata = _SqlSavedata_info.Get_str_value(data_code);
        string[] arrdata = strdata.Split('|');
        if (arrdata.Length > 1)
        {
            try
            {
                rtn.x = System.Convert.ToSingle(arrdata[0]);
                rtn.y = System.Convert.ToSingle(arrdata[1]);
            }
            catch
            {
                Debug.LogError("~~~~~~~~~~~ System.Convert.ToSingle(arrdata[0])");
            }
        }

        return rtn;
    }

    int _DefaultClassNo = 1;
    public int GetLevel_Info_exp()
    {
        int val = GetInfo_exp();
        if (val <= 0)
        {
            NGUIDebug.Log("GetInfo_exp Error!");
            return 1;
        }
        return _SqlBalance_level_info.Get_level(val, _DefaultClassNo); // _DefaultClassNo : 1
    }
    public void AddPlayScore(int n_score)
    {
        string data_code = "total_play_score";
        int val = _SqlSavedata_info.Get_value(data_code);
        val += n_score;
        _SqlSavedata_info.Update_value(val, data_code);
    }
    public void UpdateTotalPlayScore(int n_score)
    {
        string data_code = "total_play_score";
        _SqlSavedata_info.Update_value(n_score, data_code);
    }
    public int GetTotalPlayScore()
    {
        string data_code = "total_play_score";
        int val = _SqlSavedata_info.Get_value(data_code);
        return val;
    }
    public int GetInfo_gold()
    {
        string data_code = "gold";
        int val = _SqlSavedata_info.Get_value(data_code);
        return val;
    }
    public void UpdateInfo_gold(int val)
    {
        string data_code = "gold";
        _SqlSavedata_info.Update_value(val, data_code);
    }
    public int GetInfo_exp()
    {
        string data_code = "exp";
        int val = _SqlSavedata_info.Get_value(data_code);
        return val;
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
	public string GetDataFilePath_StartupSaveDB()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_StartupSaveDB;
		#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_StartupSaveDB;	           
		#endif
		return rtn;
		
	}
    public string GetDataFilePath_StartupShare01DB()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_StartupShare01DB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_StartupShare01DB;	           
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
	public string GetDataFilePath_SaveDB()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_SaveDB;
		#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_SaveDB;	           
		#endif
		return rtn;
		
	}
    public string GetDataFilePath_Share01DB()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string rtn = Application.persistentDataPath + "/" + _DataBaseFileName_Share01DB;
#elif UNITY_ANDROID
		string rtn =  Application.persistentDataPath + "/" + _DataBaseFileName_Share01DB;	           
#endif
        return rtn;

    }
}
