#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public struct ST_S_unit_invenRec
{
    public int idx;
    public string unit_code;
    public int total_exp;
    public int slot_no;
    public int class_no;

}

public class SqlSavedata_unit_inven {

	private SQLiteDB _db = null;

    private string _querySelect_total_exp = "SELECT total_exp FROM savedata_unit_inven where idx = {0} ;";
    private string _queryUpdate_total_exp = "Update savedata_unit_inven set total_exp = ? where idx = {0} ;";
    private string _querySelect_all_from_slot_no = "SELECT * FROM savedata_unit_inven where slot_no = {0} ;";

    public SqlSavedata_unit_inven()
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

    public int Get_total_exp(string idx)
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
            string strsql = string.Format(_querySelect_total_exp, idx); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                try
                {
                    rtn = qr.GetInteger("total_exp");
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
    public ST_S_unit_invenRec Get_All_From_slot_no(int a_slot_no)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        ST_S_unit_invenRec rtn = new ST_S_unit_invenRec();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_from_slot_no, a_slot_no); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                try
                {
                    rtn.idx = qr.GetInteger("idx");
                    rtn.slot_no = qr.GetInteger("slot_no");
                    rtn.total_exp = qr.GetInteger("total_exp");
                    rtn.unit_code = qr.GetString("unit_code");
                    rtn.class_no = qr.GetInteger("class_no");
                }
                catch
                {
                    
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
    public void Update_total_exp(int a_val, string idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_total_exp, idx); //

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
}
