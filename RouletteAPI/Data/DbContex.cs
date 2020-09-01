using Microsoft.Extensions.Configuration;
using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteAPI.Data
{
    public class DbContex:IDbContex
    {
        #region Properties
        private static string DBName;
        #endregion
        #region Constructor
        public DbContex(IConfiguration configuration)
        {
            DBName = configuration["BD"];
        }
        #endregion
        #region Methods
        public async Task<PersonResponse> GetPerson (LoginRequest request)
        {
            List<PersonResponse> people = new List<PersonResponse>();
            using (var ctx = GetInstance())
            {
                string query = "select id, Name, User, Money from Person where User=? AND Pass=?";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("User", request.User));
                    command.Parameters.Add(new SQLiteParameter("Pass", request.Pass));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new PersonResponse
                            {
                                id= Convert.ToInt32(reader["id"].ToString()),
                                Name= reader["Name"].ToString(),
                                User= reader["User"].ToString(),
                                Money= Convert.ToInt32(reader["Money"].ToString())
                            });
                        }
                    }

                }
            }
            return people.FirstOrDefault();
        }


        #region PrivateMethods
        private static SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(
                string.Format("Data Source={0};Version=3;", DBName)
            );

            db.Open();

            return db;
        }
        #endregion
#endregion
    }
}
