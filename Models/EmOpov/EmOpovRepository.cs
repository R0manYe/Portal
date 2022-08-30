using Dapper;
using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using Portal.Models.Identific;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models.EmOpov
{
   
        public interface IEmOpovRepository
        {
            void Create(EmOpov EmOpov);
            void Delete(int id);
            EmOpov Get(int id);
         //   public void GetFIO(int id);
           List<EmOpov> GetEmOpov();
            void Update(EmOpov EmOpov);
        }
    public class EmOpovRepository : IEmOpovRepository
    {
        string connectionString = null;
        public EmOpovRepository(string conn)
        {
            connectionString = conn;
        }
        public List<EmOpov> GetEmOpov()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {

                  return db.Query<EmOpov>("select id,naim,id_sp,(select naim_full from station_ppgt  where station_ppgt.id_sp=spr_collection.id_sp) as naim_full  from spr_collection " +
                " where id_spr='EMAIL_COMPLEX'").ToList();
             

            }
        }
       

            public EmOpov Get(int id)
            {
                using (IDbConnection db = new OracleConnection(connectionString))
                {
                    return db.Query<EmOpov>("SELECT * FROM SPR_COLLECTION WHERE Id = :id", new { id }).FirstOrDefault();
                }
            }
      /*  private IHttpContextAccessor context;
        public void SomeRepo(IHttpContextAccessor context)
        {
            this.context = context;
           
        }
        public  List<EmOpov> GetFio()
        {
            using (IDbConnection db = new OracleConnection(connectionString))
            {
                return db.Query<EmOpov>("select  last_name||' '||substr(first_name,0,1)||'.'||substr(second_name,0,1)||'.' as fio from vspt_subject_persone where id = :'" + this.context.HttpContext.User.Identity.Name + "'").ToList();
            }
        }*/
       

        public void Create(EmOpov emOpov)
            {
                using (IDbConnection db = new OracleConnection(connectionString))
                {
                    var sqlQuery = "INSERT INTO spr_collection (ID_SP,NAIM,ID_SPR) VALUES(:ID_SP,:NAIM,'EMAIL_COMPLEX')";
                    db.Execute(sqlQuery, emOpov);

                    // если мы хотим получить id добавленного пользователя
                    //var sqlQuery = "INSERT INTO Users (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                    //int? userId = db.Query<int>(sqlQuery, user).FirstOrDefault();
                    //user.Id = userId.Value;
                }
            }

            public void Update(EmOpov emOpov)
            {
                using (IDbConnection db = new OracleConnection(connectionString))
                {
                    var sqlQuery = "UPDATE spr_collection SET ID_SP = :ID_SP, NAIM = :NAIM WHERE ID = :ID";
                    db.Execute(sqlQuery, emOpov);
                }
            }

            public void Delete(int id)
            {
                using (IDbConnection db = new OracleConnection(connectionString))
                {
                    var sqlQuery = "DELETE FROM spr_collection WHERE Id = :id";
                    db.Execute(sqlQuery, new { id });
                }
            }
        }

    
}
