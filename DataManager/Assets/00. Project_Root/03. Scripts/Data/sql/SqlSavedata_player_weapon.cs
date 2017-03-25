#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;


public struct ST_S_PlayerWeaponRec{
	public int idx;
	public int hero_ix;
	public int slot_no;
	public string weapon_datacode;
	public int up_level;
	public int up_dur;
	public int up_attack;
	public float up_critical_ratio;
    public int upgrade_limit;
    public float up_attack_speed;
    public int class_no;
    public int count;
}

public class SqlSavedata_player_weapon {

	private SQLiteDB _db = null;

	private string _querySelect_weapon_datacode = "SELECT weapon_datacode FROM savedata_player_weapon where hero_ix = 0 ;";
    private string _queryUpdate_weapon_datacode_where_idx = "Update savedata_player_weapon set weapon_datacode = ? where idx = {0};";
    private string _queryUpdate_up_attack = "Update savedata_player_weapon set up_attack = ? where hero_ix = 0 and weapon_datacode = '{0}' ;";
	private string _queryUpdate_up_critical_ratio = "Update savedata_player_weapon set up_critical_ratio = ? where hero_ix = 0 and weapon_datacode = '{0}' ;";
    private string _queryUpdate_slot_no = "Update savedata_player_weapon set slot_no = ? where idx = {0} ;";
    private string _queryUpdate_atk_cri_spd = "Update savedata_player_weapon set up_attack = ?, up_critical_ratio = ?, up_attack_speed = ? where idx = {0} ;";
    private string _queryUpdate_slot_no_clear = "Update savedata_player_weapon set slot_no = 0 where slot_no = {0} ;";
    private string _queryUpdate_upgrade_limit_down = "Update savedata_player_weapon set upgrade_limit = upgrade_limit-1 where idx = {0} ;";
    private string _queryDelete_idx = "Delete From savedata_player_weapon where idx = {0} ;";
    private string _querySelect_weapon_datacode_where_slot_no = "SELECT weapon_datacode FROM savedata_player_weapon where slot_no = {0} ;";
	private string _querySelect_idx_where_slot_no = "SELECT idx FROM savedata_player_weapon where slot_no = {0} ;";
    private string _querySelect_all_where_slot_no = "SELECT * FROM savedata_player_weapon where slot_no = {0} ;";
    private string _querySelect_all_where_weapon_datacode = "SELECT * FROM savedata_player_weapon where weapon_datacode = '{0}';";
    private string _querySelect_all_where_idx = "SELECT * FROM savedata_player_weapon where idx = {0} ;";
    private string _queryUpdate_all_where_idx = "Update savedata_player_weapon set slot_no=?, weapon_datacode = ?, up_level=?,up_dur=?,up_attack=?,up_critical_ratio=?,upgrade_limit=?,up_attack_speed=?,class_no=?,count=? where idx = {0};";
    private string _queryInsert = "INSERT INTO savedata_player_weapon (hero_ix,slot_no,weapon_datacode,up_level,up_dur,up_attack,up_critical_ratio,upgrade_limit,up_attack_speed,class_no,count) VALUES(0,?,?,?,?,?,?,?,?,?,?);";
	
	public SqlSavedata_player_weapon()
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

	public string Get_weapon_datacode()
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		string rec = "";

		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_weapon_datacode); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rec = qr.GetString("weapon_datacode");
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
		
		return rec;
	}
	public string Get_weapon_datacode_where_slot_no(int a_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		string rec = "";
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_weapon_datacode_where_slot_no, a_slot_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rec = qr.GetString("weapon_datacode");
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
		
		return rec;
	}
	public int Get_weapon_idx_where_slot_no(int a_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int rec = 0;
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_idx_where_slot_no, a_slot_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rec = qr.GetInteger("idx");
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
		
		return rec;
	}

	public int[] GetList_weapon_idx_where_slot_no(int a_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		int rec = 0;
		List<int> rtnlist = new List<int>();

		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_idx_where_slot_no, a_slot_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				rec = qr.GetInteger("idx");
				rtnlist.Add(rec);
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
		
		return rtnlist.ToArray();
	}
	public ST_S_PlayerWeaponRec[] GetList_weapon_all_where_slot_no(int a_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
		
		// ST_S_PlayerWeaponRec rec;
		List<ST_S_PlayerWeaponRec> rtnlist = new List<ST_S_PlayerWeaponRec>();
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
			string strsql = string.Format(_querySelect_all_where_slot_no, a_slot_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
				ST_S_PlayerWeaponRec rec = new ST_S_PlayerWeaponRec();
				// idx, hero_ix, slot_no, weapon_datacode, up_level, up_dur, up_attack, up_critical_ratio

				rec.idx = qr.GetInteger("idx");
				rec.hero_ix = qr.GetInteger("hero_ix");
				rec.slot_no = qr.GetInteger("slot_no");
				rec.weapon_datacode = qr.GetString("weapon_datacode");
				rec.up_level = qr.GetInteger("up_level");
				rec.up_dur = qr.GetInteger("up_dur");
				rec.up_attack = qr.GetInteger("up_attack");
				rec.up_critical_ratio = (float) qr.GetDouble("up_critical_ratio");
                rec.upgrade_limit = qr.GetInteger("upgrade_limit");
                rec.up_attack_speed = (float)qr.GetDouble("up_attack_speed");
                rec.class_no = qr.GetInteger("class_no");
                rec.count = qr.GetInteger("count");

				rtnlist.Add(rec);
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
		
		return rtnlist.ToArray();
	}
    public ST_S_PlayerWeaponRec[] GetList_weapon_all_where_datacode(string a_datacode)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        // ST_S_PlayerWeaponRec rec;
        List<ST_S_PlayerWeaponRec> rtnlist = new List<ST_S_PlayerWeaponRec>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_where_weapon_datacode, a_datacode); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_S_PlayerWeaponRec rec = new ST_S_PlayerWeaponRec();
                // idx, hero_ix, slot_no, weapon_datacode, up_level, up_dur, up_attack, up_critical_ratio

                rec.idx = qr.GetInteger("idx");
                rec.hero_ix = qr.GetInteger("hero_ix");
                rec.slot_no = qr.GetInteger("slot_no");
                rec.weapon_datacode = qr.GetString("weapon_datacode");
                rec.up_level = qr.GetInteger("up_level");
                rec.up_dur = qr.GetInteger("up_dur");
                rec.up_attack = qr.GetInteger("up_attack");
                rec.up_critical_ratio = (float)qr.GetDouble("up_critical_ratio");
                rec.upgrade_limit = qr.GetInteger("upgrade_limit");
                rec.up_attack_speed = (float)qr.GetDouble("up_attack_speed");
                rec.class_no = qr.GetInteger("class_no");
                rec.count = qr.GetInteger("count");

                rtnlist.Add(rec);
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

        return rtnlist.ToArray();
    }
    public ST_S_PlayerWeaponRec[] GetList_weapon_all_where_idx(int a_idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        // ST_S_PlayerWeaponRec rec;
        List<ST_S_PlayerWeaponRec> rtnlist = new List<ST_S_PlayerWeaponRec>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_where_idx, a_idx); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_S_PlayerWeaponRec rec = new ST_S_PlayerWeaponRec();
                // idx, hero_ix, slot_no, weapon_datacode, up_level, up_dur, up_attack, up_critical_ratio

                rec.idx = qr.GetInteger("idx");
                rec.hero_ix = qr.GetInteger("hero_ix");
                rec.slot_no = qr.GetInteger("slot_no");
                rec.weapon_datacode = qr.GetString("weapon_datacode");
                rec.up_level = qr.GetInteger("up_level");
                rec.up_dur = qr.GetInteger("up_dur");
                rec.up_attack = qr.GetInteger("up_attack");
                rec.up_critical_ratio = (float)qr.GetDouble("up_critical_ratio");
                rec.upgrade_limit = qr.GetInteger("upgrade_limit");
                rec.up_attack_speed = (float)qr.GetDouble("up_attack_speed");
                rec.class_no = qr.GetInteger("class_no");
                rec.count = qr.GetInteger("count");

                rtnlist.Add(rec);
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

        return rtnlist.ToArray();
    }


	public void Update_up_attack(int a_attack, string a_datacode)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_DB());
			
			SQLiteQuery qr;		
			string strsql = string.Format(_queryUpdate_up_attack, a_datacode); // 

			qr = new SQLiteQuery(_db, strsql); 
			qr.Bind(a_attack);			
			qr.Step();
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}
		
		return;
	}
	public void Update_up_critical_ratio(int a_critical_ratio, string a_datacode)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_DB());
			
			SQLiteQuery qr;		
			string strsql = string.Format(_queryUpdate_up_critical_ratio, a_datacode); // 
			
			qr = new SQLiteQuery(_db, strsql); 
			qr.Bind(a_critical_ratio);			
			qr.Step();
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}
		
		return;
	}
	public void Update_Crear_slot_no(int a_slot_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			_db.Open(GetFileName_DB());
			
			SQLiteQuery qr;		
			// a_slot_no clear
			string strsql = string.Format(_queryUpdate_slot_no_clear, a_slot_no); // 
			qr = new SQLiteQuery(_db, strsql);
			qr.Step();
			qr.Release();   
			

			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}
		
		return;
	}

	public void Update_slot_no(int a_slot_no, int a_idx)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}		
		try{
			Update_Crear_slot_no(a_slot_no);

			_db.Open(GetFileName_DB());
			
			SQLiteQuery qr;		
			string strsql = string.Format(_queryUpdate_slot_no, a_idx); // 
			qr = new SQLiteQuery(_db, strsql); 
			qr.Bind(a_slot_no);			
			qr.Step();
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}
		
		return;
	}
    public void Update_weapon_datacode(string a_datacode, int a_idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_weapon_datacode_where_idx, a_idx); // 
            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_datacode);
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
    public void Update_atk_cri_spd(int a_atk, float a_cri, float a_spd, int a_idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_atk_cri_spd, a_idx); // 
            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_atk);
            qr.Bind(a_cri);
            qr.Bind(a_spd);
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
    public void Update_upgrade_limit_down(int a_idx)
    {
        if (_db == null)
        {  _db = new SQLiteDB();  }
        try
        {
            _db.Open(GetFileName_DB());
            SQLiteQuery qr;
            // 
            string strsql = string.Format(_queryUpdate_upgrade_limit_down, a_idx); // 변경점.
            qr = new SQLiteQuery(_db, strsql);
            qr.Step();
            qr.Release();
            _db.Close();
        }
        catch (Exception e)
        {
            if (_db != null)
            {         _db.Close();         }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return;
    }

    public void DeleteFrom_idx(int a_idx)
    {
        if (_db == null)
        { _db = new SQLiteDB(); }
        try
        {
            _db.Open(GetFileName_DB());
            SQLiteQuery qr;
            // 
            string strsql = string.Format(_queryDelete_idx, a_idx); // 변경점.
            qr = new SQLiteQuery(_db, strsql);
            qr.Step();
            qr.Release();
            _db.Close();
        }
        catch (Exception e)
        {
            if (_db != null)
            { _db.Close(); }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return;
    }
    public void Update_all_where_idx(
        int a_idx,
        ST_S_PlayerWeaponRec rec
        )
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_all_where_idx, a_idx); // 
            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(rec.slot_no);
            qr.Bind(rec.weapon_datacode);
            qr.Bind(rec.up_level);
            qr.Bind(rec.up_dur);
            qr.Bind(rec.up_attack);
            qr.Bind(rec.up_critical_ratio);
            qr.Bind(rec.upgrade_limit);
            qr.Bind(rec.up_attack_speed);
            qr.Bind(rec.class_no);
            qr.Bind(rec.count);
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
    
    public void InsertData( int slot_no,
	     string weapon_datacode,
	     int up_level,
	     int up_dur,
	     int up_attack,
	     float up_critical_ratio,
         int upgrade_limit,
         float up_attack_speed,
         int class_no,
         int count
        )
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            // string strsql = string.Format(_queryUpdate_atk_cri_spd, a_idx); // 
            qr = new SQLiteQuery(_db, _queryInsert);
            qr.Bind(slot_no);
            qr.Bind(weapon_datacode);
            qr.Bind(up_level);
            qr.Bind(up_dur);
            qr.Bind(up_attack);
            qr.Bind(up_critical_ratio);
            qr.Bind(upgrade_limit);
            qr.Bind(up_attack_speed);
            qr.Bind(class_no);
            qr.Bind(count);
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
