#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_B_UnitRec
{
    public int class_no;
    public int level;
    public string unit_code;
    public int hp;
    public int attack;
    public int point_min;
    public int point_max;
    public int gold_min;
    public int gold_max;
    
}

public class SqlBalance_unit {

	private SQLiteDB _db = null;

    // select b.hp+a.value1 as hp, b.attack+a.value2 as attack, class_no, level, unit_code, point_min, point_max, gold_min, gold_max from balance_level_info a, balance_unit b where unit_code = 'u0001' and level = 10 and class_no = 2
    private string _querySelect_UnitRec = "select b.hp+a.value1 as hp, (b.attack+(b.attack * (a.value2*0.2))) as attack, class_no, level, unit_code, point_min, point_max, gold_min, gold_max from balance_level_info a, balance_unit b where unit_code = '{0}' and level = {1} and class_no = {2} ;";

    public SqlBalance_unit()
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

    public ST_B_UnitRec Get_UnitRec(string a_unit_code, int a_level, int a_class_no)
	{
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}

        ST_B_UnitRec rtn = new ST_B_UnitRec();
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = string.Format(_querySelect_UnitRec, a_unit_code, a_level, a_class_no); // _querySelect_exp
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                try
                {
                    rtn.attack = qr.GetInteger("attack");
                }
                catch
                {
                    try
                    {
                        rtn.attack = (int) qr.GetDouble("attack");
                    }
                    catch
                    {
                        rtn.attack = 1;
                    }
                }
                
                rtn.gold_max = qr.GetInteger("gold_max");
                rtn.gold_min = qr.GetInteger("gold_min");
                rtn.hp = qr.GetInteger("hp");
                rtn.class_no = qr.GetInteger("class_no");
                rtn.level = a_level;
                rtn.point_max = qr.GetInteger("point_max");
                rtn.point_min = qr.GetInteger("point_min");
                rtn.unit_code = a_unit_code;
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

}
