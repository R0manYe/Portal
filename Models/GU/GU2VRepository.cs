using Dapper;
using Oracle.ManagedDataAccess.Client;
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
                return db.Query<GU2V>("select num,notification_date as n_dat,railway_station_name as st_naim,planned_filing_date as pl_dat,planned_filing_time as pl_time," +
                    "wagons_total as col_w,id_doc_etran as id,date_ins,(select count(wagon_id) as vag from COMPLEX.prsd_wagon_load wl where wl.etran_doc_id = etran_gu2v_item.id_doc_etran) as col_w_f " +
                    "from etran_gu2v_item where date_ins> (sysdate-5) and wagons_total!=(select count(wagon_id) as vag from COMPLEX.prsd_wagon_load wl where wl.etran_doc_id=etran_gu2v_item.id_doc_etran) " +
                    " group by id_doc_etran, railway_station_name, planned_filing_date, planned_filing_time, wagons_total, num, notification_date, date_ins order by date_ins desc").ToList();
            }
        }

       

    }
}
