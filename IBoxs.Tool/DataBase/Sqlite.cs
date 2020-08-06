using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IBoxs.Tool.DataBase
{
    public static class Sqlite
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseFile"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int SqliteRun(string dataBaseFile, string sql)
        {
            string connStr = "Data Source = " + dataBaseFile + ";Version=3;Journal Mode=WAL";
            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand SQLiteCmd = new SQLiteCommand(sql, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            Monitor.Enter(obj);

            int result = SQLiteCmd.ExecuteNonQuery();

            Monitor.Exit(obj);
            SQLiteCmd.Dispose();
            conn.Close();
            conn.Dispose();
            return result;
        }

        private static readonly object obj = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseFile"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable SqliteSel(string dataBaseFile, string sql)
        {
            string connStr = "Data Source = " + dataBaseFile + ";Version=3;Journal Mode=WAL";
            DataTable ds = new DataTable();
            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand SQLiteCmd = new SQLiteCommand(sql, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            Monitor.Enter(obj);

            SQLiteDataAdapter dbDataAdapter = new SQLiteDataAdapter(sql, conn);
            dbDataAdapter.Fill(ds); //用适配对象填充表对象

            Monitor.Exit(obj);
            SQLiteCmd.Dispose();
            conn.Close();
            conn.Dispose();
            return ds;
        }
    }
}
