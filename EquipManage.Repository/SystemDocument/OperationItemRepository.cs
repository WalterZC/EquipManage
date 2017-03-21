using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace EquipManage.Repository.SystemDocument
{
    public class OperationItemRepository : RepositoryBase<OperationItemEntity>, IOperationItemRepository
    {
        public List<OperationItemEntity> GetItemList(string FNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_OperationItem d
                                    INNER  JOIN Sys_OperationProject i ON i.FId = d.FItemId
                            WHERE   1 = 1
                                    AND i.FNumber = @FNumber
                                    AND d.FEnabledMark = 1
                                    AND ISNULL(d.FDeleteMark,0) = 0
                            ORDER BY d.FSortCode ASC");
            DbParameter[] parameter =
            {
                 new SqlParameter("@FNumber",FNumber)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
    }
}
