#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public class SqlSavePlayerData {

	private SQLiteDB _db = null;

    private string _querySelect_max_inven = "SELECT max_inven FROM savedata_player where hero_ix = 0 ;";
    private string _querySelect_tutorial_play = "SELECT tutorial_play FROM savedata_player where hero_ix = 0 ;";
    private string _querySelectHP = "SELECT hp FROM savedata_player where hero_ix = 0 ;";
    private string _querySelectAttack = "SELECT attack FROM savedata_player where hero_ix = 0 ;";
    private string _querySelectplay_slot_no = "SELECT play_slot_no FROM savedata_player where hero_ix = 0 ;";
	private string _querySelectGold = "SELECT gold FROM savedata_player where hero_ix = 0 ;";
	private string _querySelect_exp = "SELECT exp FROM savedata_player where hero_ix = 0 ;";
	private string _querySelect_level = "SELECT level FROM savedata_player where hero_ix = 0 ;";
    private string _queryUpdate_max_inven = "Update savedata_player set max_inven = ? where hero_ix = 0 ;";
    private string _queryUpdate_gold = "Update savedata_player set gold = ? where hero_ix = 0 ;";
    private string _queryUpdate_exp = "Update savedata_player set exp = ? where hero_ix = 0 ;";
    private string _queryUpdate_exp_total = "Update savedata_player set exp_total = ? where hero_ix = 0 ;";
	private string _queryUpdate_level = "Update savedata_player set level = ? where hero_ix = 0 ;";
    private string _queryUpdate_play_slot_no = "Update savedata_player set play_slot_no = ? where hero_ix = 0 ;";
    private string _queryUpdate_tutorial_play = "Update savedata_player set tutorial_play = ? where hero_ix = 0 ;";

	public SqlSavePlayerData()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_SaveDB()
	{
		return DataManager.Instance.GetDataFilePath_SaveDB();
		
	}
	public int Get_PlaySlot()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int rtn = 0;
		
		string filename = GetFileName_SaveDB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			qr = new SQLiteQuery(_db, _querySelectplay_slot_no); 
			while( qr.Step() )
			{
				rtn = qr.GetInteger("play_slot_no");
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
    public int Get_tutorial_play()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int tutorial_play = 0;

        string filename = GetFileName_SaveDB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            qr = new SQLiteQuery(_db, _querySelect_tutorial_play);
            while (qr.Step())
            {
                tutorial_play = qr.GetInteger("tutorial_play");
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

        return tutorial_play;
    }
	public int Get_max_inven()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}

        int max_inven = 0;
		
		string filename = GetFileName_SaveDB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            qr = new SQLiteQuery(_db, _querySelect_max_inven); 
			while( qr.Step() )
			{
                max_inven = qr.GetInteger("max_inven");
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

        return max_inven;
	}
    public int Get_HP()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int hp = 0;

        string filename = GetFileName_SaveDB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            qr = new SQLiteQuery(_db, _querySelectHP);
            while (qr.Step())
            {
                hp = qr.GetInteger("hp");
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

        return hp;
    }
    public int Get_attack()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        int atk = 0;

        string filename = GetFileName_SaveDB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            qr = new SQLiteQuery(_db, _querySelectAttack);
            while (qr.Step())
            {
                atk = qr.GetInteger("attack");
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

        return atk;
    }
	public int Get_Gold()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int gold = 0;
		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;
			qr = new SQLiteQuery(_db, _querySelectGold); 
			while( qr.Step() )
			{
				gold = qr.GetInteger("gold");
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
		
		return gold;
	}
	public int Get_level()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int level = 0;
		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;
			qr = new SQLiteQuery(_db, _querySelect_level); 
			while( qr.Step() )
			{
				level = qr.GetInteger("level");
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
		
		return level;
	}
	public int Get_exp()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int exp = 0;
				
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;
			qr = new SQLiteQuery(_db, _querySelect_exp); 
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
    public void Update_max_inven(int a_max_inven)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_SaveDB());

            SQLiteQuery qr;
            // string strsql = string.Format(_queryUpdate_exp

            qr = new SQLiteQuery(_db, _queryUpdate_max_inven);
            qr.Bind(a_max_inven);
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
	public void Update_exp(int a_exp)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;
			// string strsql = string.Format(_queryUpdate_exp
			
			qr = new SQLiteQuery(_db, _queryUpdate_exp); 
			qr.Bind(a_exp);			
			qr.Step();
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
		
		return;
	}
	public void Update_exp_total(int a_exp)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;
			// string strsql = string.Format(_queryUpdate_exp
			
			qr = new SQLiteQuery(_db, _queryUpdate_exp_total); 
			qr.Bind(a_exp);			
			qr.Step();
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
		
		return;
	}
	public void Update_level(int a_level)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;			
			qr = new SQLiteQuery(_db, _queryUpdate_level); 
			qr.Bind(a_level);			
			qr.Step();
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
		
		return;
	}
	public void Update_play_slot_no(int a_play_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_SaveDB());
			
			SQLiteQuery qr;			
			qr = new SQLiteQuery(_db, _queryUpdate_play_slot_no); 
			qr.Bind(a_play_slot_no);			
			qr.Step();
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
		
		return;
	}
    public void Update_gold(int a_gold)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_SaveDB());
            SQLiteQuery qr;
            // string strsql = string.Format(_queryUpdate_exp
            qr = new SQLiteQuery(_db, _queryUpdate_gold);
            qr.Bind(a_gold);                                 // 
            qr.Step();
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

        return;
    }
    public void Update_tutorial_play(int a_tutorial_play)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_SaveDB());
            SQLiteQuery qr;
            // string strsql = string.Format(_queryUpdate_exp
            qr = new SQLiteQuery(_db, _queryUpdate_tutorial_play);
            qr.Bind(a_tutorial_play);                                 // 
            qr.Step();
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

        return;
    }
}
