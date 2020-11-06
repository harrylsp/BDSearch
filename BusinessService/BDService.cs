using DBService;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessService
{
    public class BDService
    {
        public int AddBDFile(BDModel model)
        {
            var sql = @"insert into FInfo(fid, fname, slink, scode, remarks) values(@fid, @fname, @slink, @scode, @remarks)";
            return SqliteHelper.ExcuteSql(sql, model);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<BDModel> Query(string where)
        {
            var sql = @"select * from FInfo where 1=1 " + where;
            return SqliteHelper.QuerySql<BDModel>(sql);
        }
    }
}
