#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public class SqlSavedata_info {

	private SQLiteDB _db = null;

	private string _querySelect_value = "SELECT value FROM savedata_info where data_code = '{0}' ;";
	private string _querySelect_str_value = "SELECT str_value FROM savedata_info where data_code = '{0}' ;";
    private string _queryUpdate_value = "Update savedata_info set value = ? where data_code = '{0}' ;";
    private string _queryUpdate_str_value = "Update savedata_info set str_value = ? where data_code = '{0}' ;";

    public SqlSavedata_info()
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

    public int Get_value(string a_data_code)
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
            string strsql = string.Format(_querySelect_value, a_data_code); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                try
                {
                    rtn = qr.GetInteger("value");
                }
                catch
                {
                    rtn = 0;
                }
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
    public string Get_str_value(string a_data_code)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        string rtn = "";

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_str_value, a_data_code); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                try
                {
                    rtn = qr.GetString("str_value");
                }
                catch
                {
                    rtn = "";
                }
            }
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return rtn;
    }
    public void Update_value(int a_val, string a_data_code)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_value, a_data_code); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_val);
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
    public void Update_str_value(string a_strval, string a_data_code)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_str_value, a_data_code); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_strval);
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
