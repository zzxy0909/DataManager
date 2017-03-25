#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public class SqlSavedata_upgrade {

	private SQLiteDB _db = null;

	private string _querySelect_upgrade_level = "SELECT upgrade_level FROM savedata_upgrade where data_code = '{0}' ;";
    private string _queryUpdate_upgrade_level = "Update savedata_upgrade set upgrade_level = ? where data_code = '{0}' ;";

    public SqlSavedata_upgrade()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_SaveDB();
		
	}

    public int Get_upgrade_level(string a_data_code )
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int rtn = 0;

		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = string.Format(_querySelect_upgrade_level, a_data_code); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                rtn = qr.GetInteger("upgrade_level");
			}
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}

        return rtn;
	}
    public void Update_upgrade_level(int a_level, string a_data_code)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_upgrade_level, a_data_code); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_level);
            qr.Step();
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

        return;
    }
}
