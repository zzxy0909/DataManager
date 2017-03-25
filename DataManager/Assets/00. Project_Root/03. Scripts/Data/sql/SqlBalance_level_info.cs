#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


public class SqlBalance_level_info {

	private SQLiteDB _db = null;

    private string _querySelect_exp = "SELECT exp FROM balance_level_info where level = {0} and class_no = {1} ;";
    private string _querySelect_total_exp = "SELECT total_exp FROM balance_level_info where level = {0} and class_no = {1} ;";
    private string _querySelect_level = "SELECT level FROM balance_level_info where total_exp <= {0} and (total_exp + exp) > {0} and class_no = {1} ;";
	
	public SqlBalance_level_info()
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
    public int Get_total_exp(int a_level, int a_class)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int exp = 0;

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_total_exp, a_level, a_class); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                exp = qr.GetInteger("total_exp");
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

        return exp;
    }
    public int Get_Next_exp(int a_level, int a_class)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int exp = 0;
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_exp, a_level, a_class); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				exp = qr.GetInteger("exp");
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
		
		return exp;
	}
    public int Get_level(int a_exp, int a_class)
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
            string strsql = string.Format(_querySelect_level, a_exp, a_class); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn = qr.GetInteger("level");
            }
            qr.Release();
            _db.Close();

//            UnityEngine.Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~ " + strsql + " ~~~>" + rtn);
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
