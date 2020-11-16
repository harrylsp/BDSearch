using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBService
{
    public class DbInit
    {
        public const string _dbName = "lsp.db";

        public void initTable()
        {
            //增加税盘数据表
            string Table_FileInfo = @"CREATE TABLE IF NOT EXISTS FInfo (fno, TEXT, fid TEXT, fname TEXT, slink TEXT, scode TEXT, modifytime TEXT, fstatus TEXT, fscore TEXT, remarks TEXT);";

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(Table_FileInfo);

            string dbfile = SqliteHelper.CreateDB(_dbName);
            SqliteHelper._ConnectString = "data source=" + dbfile + ";version=3;";
            SqliteHelper.Excute(sqlBuilder.ToString());
        }
    }
}
