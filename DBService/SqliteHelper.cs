using Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace DBService
{
    public class SqliteHelper
    {
        public static string _ConnectString { get; set; }

        /// <summary>
        /// 当前目创建数据库
        /// </summary>
        /// <param name="dbName"></param>
        public static string CreateDB(string dbName)
        {
            string dbfile = Path.Combine(Util.GetAppExePath(), dbName);

            if (!File.Exists(dbfile))
            {
                SQLiteConnection.CreateFile(dbfile);
            }
            return dbfile;
        }

        public static SQLiteConnection SQLiteConn()
        {
            var connection = new SQLiteConnection(_ConnectString);
            connection.Open();
            return connection;
        }

        public static List<T> QuerySqlString<T>(string sqlStr)
        {
            using (IDbConnection conn = new SQLiteConnection(_ConnectString))
            {
                return conn.Query<T>(sqlStr) as List<T>;
            }
        }

        public static int Excute(string sql)
        {
            using (IDbConnection conn = new SQLiteConnection(_ConnectString))
            {
                return conn.Execute(sql);
            }
        }

        /// <summary>
        /// 查询表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> QuerySql<T>(string sql)
        {
            using (IDbConnection conn = SQLiteConn())
            {
                return conn.Query<T>(sql) as List<T>;
            }
        }

        /// <summary>
        /// 执行 插入 更新
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExcuteSql(string sql, object param = null)
        {
            using (IDbConnection conn = SQLiteConn())
            {
                return conn.Execute(sql, param);
            }
        }

    }
}
