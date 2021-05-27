using System;
using MySql.Data.MySqlClient;

namespace WebApplication.Models
{
    public class DBManager
    {
        private MySqlConnection _mySqlConnection;
        private MySqlDataAdapter _mySqlDataAdapter;

        public DBManager()
        {
            _mySqlConnection = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"));
            _mySqlDataAdapter = new MySqlDataAdapter();
            _mySqlConnection.Open();
        }

        public MySqlDataReader GetReader(string cmd) => new MySqlCommand(cmd, _mySqlConnection).ExecuteReader();

        public void InsertCommand(string cmd)
        {
            var command = new MySqlCommand(cmd, _mySqlConnection);
            _mySqlDataAdapter.InsertCommand = command;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void Close() => _mySqlConnection.Close();
    }
}