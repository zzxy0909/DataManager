#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public class SqlSavedata_player_stage {

	private SQLiteDB _db = null;

    private string _querySelect_score = "SELECT score FROM savedata_player_stage where hero_ix = 0 and stage_no = {0} and round_no = {1} ;";
    private string _querySelect_StartLockNumber = "SELECT round_no FROM savedata_player_stage where hero_ix = 0 and stage_no = {0} and score < 0 ORDER BY round_no LIMIT 1 ;";
    private string _queryUpdate_score = "Update savedata_player_stage set score = ? where hero_ix = 0 and stage_no = {0} and round_no = {1} ;";
//    private string queryInsert = "INSERT INTO savedata_player_stage (hero_ix, stage_no, round_no, blob_field) VALUES(0, ,?);";
	
    public SqlSavedata_player_stage()
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
    public int Get_StartLockNumber(int a_stage_no)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int rtn = -1;

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_StartLockNumber, a_stage_no); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn = qr.GetInteger("round_no");
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


    public int Get_score(int a_stage_no, int a_round_no )
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int rtn = -1;

		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = string.Format(_querySelect_score, a_stage_no, a_round_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                rtn = qr.GetInteger("score");
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
    public void Update_score(int a_score, int a_stage_no, int a_round_no)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_score, a_stage_no, a_round_no); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_score);
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
