using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class rows_ch_base
{
    public List<ch_base> rows = new List<ch_base>();
}
[System.Serializable]
public class ch_base
{
    public int ch_index;
    public float ch_height;
    public float Bot_Rotate;
    public float ch_ability_index;
    public float ch_lv_type;
    public float ch_variety;
    public float ch_class;
    public float ch_class_job;
    public float ch_grade;
    public float buildup_max;
    public float upgrade_lv;
    public float upgrade_pay_type;
    public float upgrade_pay_cost;
    public float upgrade_rune_grade;
    public float upgrade_rune_count;
    public float upgrade_lottery_index;
    public float upgrade_result_index;
    public float sell_coin;
    public float rune_open_point1;
    public float rune_open_point2;
    public float rune_open_point3;
    public float rune_type;
    public float ch_att_s_range;
    public float ch_att_l_range;
    public float ch_agg_range;
    public float ch_atk_delay_time;
    public float ch_shadow_scale;
    public float att_index;
    public float skill_index_1;
    public float skill_index_2;
    public float skill_index_3;
    public float skill_index_4;
    public string ch_icon;
    public string ch_filename;
    public float ch_weapon_model_index;
    public string ch_name_kor;
    public string ch_name_eng;
    public string ch_name_jpn;
    public string ch_name_chn;
    public string ch_script_kor;
    public string ch_script_eng;
    public string ch_script_jpn;
    public string ch_script_chn;
    public float grade_max;
    public float Mercernary_Scale;
    public float rage_max;
    public float rage_per_grade;
    public float rage_by_attack;
    public string ch_filepath;
}
//====================================================================================
[System.Serializable]
public class rows_char_ability
{
    public List<char_ability> rows = new List<char_ability>();
}
[System.Serializable]
public class char_ability
{
    public int ability_index;
    public float ability_type;
    public float lv;
    public float hp;
    public float hp_recovery;
    public float hp_kill;
    public float atk;
    public float att_spd;
    public float skill_cool_down;
    public float hit_rate;
    public float crt_rate;
    public float crt_quantity;
    public float def;
    public float era_rate;
    public float crt_res;
    public float move_spd;

}

