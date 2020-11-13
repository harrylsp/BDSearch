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
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddBDFile(BDModel model)
        {
            var sql = @"insert into FInfo(fno, fid, fname, slink, scode, remarks) values(@fno, @fid, @fname, @slink, @scode, @remarks)";
            return SqliteHelper.ExcuteSql(sql, model);
        }

        /// <summary>
        /// 全字段更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public int UpdateFile(BDModel model, string where)
        {
            var sql = @"update FInfo set fno=@fno, fid=@fid, fname=@fname, slink=@slink, scode=@scode, remark=@remark where 1=1 ";
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

        /// <summary>
        /// 获取数据库表 最大
        /// </summary>
        /// <returns></returns>
        public string GetMaxNo(string tableName)
        {
            var sql = @"select no+0  as no from " + tableName + " order by no+0 desc";
            var list = SqliteHelper.QuerySql<BDModel>(sql);
            return list[0]?.fno;
        }
    }
}
