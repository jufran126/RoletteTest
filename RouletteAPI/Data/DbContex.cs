﻿using Microsoft.Extensions.Configuration;
using RouletteAPI.Models.Login.Request;
using RouletteAPI.Models.Login.Response;
using RouletteAPI.Models.Roulette.Close.Response;
using RouletteAPI.Models.Roulette.NewRoulette.Response;
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
        public async Task<NewRouletteResponse> NewRoulette()
        {
            NewRouletteResponse response = new NewRouletteResponse();
            using (var ctx = GetInstance())
            {
                string query = "INSERT INTO Roulette (State, Bet) VALUES (?, ?)";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("State", 0));
                    command.Parameters.Add(new SQLiteParameter("Bet", 0));
                    command.ExecuteNonQuery();
                }
                query = "SELECT id FROM Roulette ORDER by id DESC LIMIT 1";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            response.id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            return response;
        }
        public async Task<CloseResponse> Close(int id)
        {
            CloseResponse response = new CloseResponse();
            using (var ctx = GetInstance())
            {
                string query = "UPDATE Roulette SET State=-1 WHERE id=?";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    command.ExecuteNonQuery();
                }
                query = "SELECT Bet FROM Roulette WHERE id=?";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            response.Bet = Convert.ToInt32(reader[0]);
                    }
                }
            }
            return response;
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
