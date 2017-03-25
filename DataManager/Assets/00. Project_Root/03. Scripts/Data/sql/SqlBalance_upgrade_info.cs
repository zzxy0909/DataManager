#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


public class SqlBalance_upgrade_info {

	private SQLiteDB _db = null;

    private string _querySelect_value = "SELECT value FROM balance_upgrade_info where data_code = '{0}' and upgrade_level = {1} ;";
    private string _querySelect_up_gold = "SELECT up_gold FROM balance_upgrade_info where data_code = '{0}' and upgrade_level = {1} ;";

    public SqlBalance_upgrade_info()
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
	
	public float Get_upgrade_value(string a_data_code, int a_level)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		float rtn = 0f;
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = string.Format(_querySelect_value, a_data_code, a_level); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                rtn = (float) qr.GetDouble("value");
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

        return rtn;
	}
    public int Get_upgrade_up_gold(string a_data_code, int a_level)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int rtn = 0;

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_up_gold, a_data_code, a_level); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn = qr.GetInteger("up_gold");
            }
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
                _db = null;
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return rtn;
    }

}
