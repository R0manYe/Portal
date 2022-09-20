using Dapper;
using Oracle.ManagedDataAccess.Client;
using Portal.Models.Identific;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models.GU
{
    public interface IGU2VRepository
    {       
        List<GU2V> GetGU2V();
        List<LOGI> GetLOGI();
        List<Dislokacia> GetDislokacia();
        Task<IEnumerable<Dislokacia>> GetDislokacias();

    }
   
    public class GU2VRepository : IGU2VRepository
    {
        string connectionString = null;
        public GU2VRepository(string conn)
        {
            connectionString = conn;
        }
        public List<GU2V> GetGU2V()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                return db.Query<GU2V>("select t1.id_doc_etran as id,t1.num,t1.railway_station_name as st_naim,t1.date_ins as PL_DAT,t1.notification_time as PL_TIME,t1.v1 as COL_W," +
                    "t2.v2 as COL_W_F from (select railway_station_name, id_doc_etran, num, max(wagons_total) as v1, date_ins, notification_time from etran_gu2v_item GROUP by id_doc_etran, " +
                    "date_ins, num, railway_station_name, notification_time) t1 left join (select etran_doc_id, count(wagon_id) as v2, date_load from COMPLEX.prsd_wagon_load where " +
                    " etran_doc_id is not null group by etran_doc_id, bu_id, date_load)t2 on t1.id_doc_etran = t2.etran_doc_id order by t1.date_ins desc").ToList();
            }
        }
        public List<LOGI> GetLOGI()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                return db.Query<LOGI>("select * from spr_etran_log where data>to_date(sysdate-10) order by data desc").ToList();
            }
        }
        public List<Dislokacia> GetDislokacia()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                return db.Query<Dislokacia>("select NOM_VAG,(select naim from spr_collection sc where id_spr='railways' and DOR_RASCH=sc.sv) as dor,NAIM_ROD_VAG,STAN_NAZN,NAIM_STAN_NAZN,GRUZPOL_OKPO,NAIM_GRUZPOL_OKPO,NAIM_GRUZOTPR_OKPO,NAIM_KOD_GRZ," +
                    " dislokacia.VES_GRZ / 1000 as ves_grz, date_op, NAIM_STAN_OP, RASST_STAN_NAZN, DATE_DOSTAV, NAIM_KOP_VMD, NOM_POEZD, INDEX_POEZD, npp_vag from dislokacia " +
                    "where NOT exists(select wagon_id from complex.prsd_wagon_oper where complex.prsd_wagon_oper.wagon_id = dislokacia.nom_vag and is_del = 0 and date_close is null " +
                    " and wagon_id is not null and LENGTH(wagon_id) = 8)  and not EXISTS(select nom_vag from DISLOKACIA_CHECK where DISLOKACIA_CHECK.NOM_VAG = dislokacia.nom_vag and DISLOKACIA_CHECK.COMPLEX = 1) " +
                    "and NOM_VAG is not null and STAN_NAZN in (select distinct id_st from spr_cli) and GRUZPOL_OKPO in (select distinct spr_cli.OKPO from spr_cli) and stan_nazn in " +
                    "(select STATION_ECP_ID as st_id from SPR_STATION_BU where bu_id in (select distinct f.spr_id as id from(select s.filter_detail_id, s.filter_id, s.spr_id, tf.anal_c1 " +
                    " as type_filter from spr_filter_detail @complex_link s, spr_filter @complex_link sf, app_dict @complex_link tf, spr_person_filter @complex_link spf, " +
                    " spr_person @complex_link sp where nvl(s.is_del, 0) != 1 and nvl(sf.is_del, 0) != 1 and nvl(spf.is_del, 0) != 1 and  nvl(sp.is_del, 0) != 1 and nvl(tf.is_del, 0) != 1 and sf.filter_id = s.filter_id " +
                    "and tf.list_id = 'ФИЛЬТР' and lower(tf.anal_c1) = 'филиал' and tf.id = sf.type_id and spf.filter_id = s.filter_id and spf.person_id = sp.person_id " +
                    " and sp.nik_name ='105119') f, (SELECT to_char(BU_ID) as SPR_ID,name as name from SPR_BU @complex_link where nvl(is_del, 0) != 1) s where s.spr_id = f.spr_id " +
                    "group by f.filter_id,f.spr_id,s.name,f.type_filter)) order by STAN_NAZN,NAIM_GRUZPOL_OKPO,NOM_POEZD,npp_vag").ToList();

            }
        }
        public async Task<IEnumerable<Dislokacia>> GetDislokacias()
        {
            var query = "SELECT * FROM DISLOKACIA";
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                var dislokacias = await db.QueryAsync<Dislokacia>(query);
                return dislokacias.ToList();

            }
        }


    }
}
