#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_B_zoneobject_point
{
    public int idx;
    public string data_code;
    public string param;
    public int gold;
    public int point1;
}

public class SqlBalance_zoneobject_point {

	private SQLiteDB _db = null;

    private string _querySelect_all = "select * from balance_zoneobject_point where data_code = '{0}' and param = '{1}' ;";
    private string _querySelect_all_null_param = "select * from balance_zoneobject_point where data_code = '{0}' ;";

    public SqlBalance_zoneobject_point()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_BalanceDB();
		
	}

    public ST_B_zoneobject_point Get_DataRec(string a_data_code, string a_param)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}

        ST_B_zoneobject_point rtn = new ST_B_zoneobject_point();
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = "";
            if (string.IsNullOrEmpty(a_param))
            {
                strsql = string.Format(_querySelect_all_null_param, a_data_code); // _querySelect_exp
            }
            else
            {
                strsql = string.Format(_querySelect_all, a_data_code, a_param); // _querySelect_exp
            }
//            UnityEngine.Debug.Log("~~~~~~~~~~" + strsql);
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                rtn.gold = qr.GetInteger("gold");
                rtn.point1 = qr.GetInteger("point1");
                rtn.idx = qr.GetInteger("idx");
                rtn.data_code = a_data_code;
                rtn.param = a_param;
            }
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
				_db = null;
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}

//        UnityEngine.Debug.Log("~~~~~~~~~~" + rtn.idx);
        return rtn;
	}

}
