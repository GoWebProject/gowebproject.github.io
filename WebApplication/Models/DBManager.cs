using System;
using MySql.Data.MySqlClient;

namespace WebApplication.Models
{
    public class DBManager
    {
        private MySqlConnection _mySqlConnection;
        private MySqlDataAdapter _mySqlDataAdapter;
        private MySqlDataReader _cachedReader;

        public DBManager()
        {
            _mySqlConnection = new MySqlConnection(Environment.GetEnvironmentVariable("mainDB_credentials"));
            _mySqlDataAdapter = new MySqlDataAdapter();
            _mySqlConnection.Open() ;
        }

        public MySqlDataReader GetReader(string cmd)
        {
            _cachedReader = new MySqlCommand(cmd, _mySqlConnection).ExecuteReader();
            return _cachedReader;
        }

        public void InsertCommand(string cmd)
        {
            var command = new MySqlCommand(cmd, _mySqlConnection);
            _mySqlDataAdapter.InsertCommand = command;
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void Close()
        {
            _mySqlConnection.Close();
            if(_cachedReader!=null)  _cachedReader.Close();
        }
    }
}