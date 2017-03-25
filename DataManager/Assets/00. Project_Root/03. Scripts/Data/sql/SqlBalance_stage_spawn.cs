#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public struct ST_B_stage_spawnRec{
	public int idx;
	public int stage_idx;
    public int spawn_cost;
    public int spawn_pool_idx;
    public int level_min;
	public int level_max;
	public int time_limit;
}

public class SqlBalance_stage_spawn {

	private SQLiteDB _db = null;

    private string _querySelect_all_stage_spawn = "SELECT * FROM balance_stage_spawn where stage_idx = {0} ;";

    public SqlBalance_stage_spawn()
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

    public ST_B_stage_spawnRec[] Get_stage_spawnData(int a_stage_idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        string filename = GetFileName_DB();

        List<ST_B_stage_spawnRec> rtnlist = new List<ST_B_stage_spawnRec>();
        
        try
        {
            _db.Open(filename);
            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_stage_spawn, a_stage_idx); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_B_stage_spawnRec rec = new ST_B_stage_spawnRec();
                rec.idx = qr.GetInteger("idx");
                rec.level_max = qr.GetInteger("level_max");
                rec.level_min = qr.GetInteger("level_min");
                rec.spawn_cost = qr.GetInteger("spawn_cost");
                rec.stage_idx = a_stage_idx;
                rec.time_limit = qr.GetInteger("time_limit");

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
	

}
