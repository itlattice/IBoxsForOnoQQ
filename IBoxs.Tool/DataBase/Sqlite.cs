using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IBoxs.Tool.DataBase
{
    public static class Sqlite
    {
        public static string sqls = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseFile"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int SqliteRun(string dataBaseFile, string sql)
        {
            if (sql == sqls)
            {
                return 0;
            }
            sqls = sql;
            string connStr = "Data Source = " + dataBaseFile + ";Version=3;";
            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand SQLiteCmd = new SQLiteCommand(sql, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            int result = SQLiteCmd.ExecuteNonQuery();
            SQLiteCmd.Dispose();
            conn.Dispose();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseFile"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable SqliteSel(string dataBaseFile, string sql)
        {
            string connStr = "Data Source = " + dataBaseFile + ";Version=3;";
            DataTable ds = new DataTable();
            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand SQLiteCmd = new SQLiteCommand(sql, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SQLiteDataAdapter dbDataAdapter = new SQLiteDataAdapter(sql, conn);
            dbDataAdapter.Fill(ds); //用适配对象填充表对象
            SQLiteCmd.Dispose();
            conn.Close();
            conn.Dispose();
            return ds;
        }
    }
}
