using System;
using System.Collections.Generic;
using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace EquipManage.Repository.SystemDocument
{
    public class OperationClassMemberRepository : RepositoryBase<OperationClassMemberEntity>, IOperationClassMemberRepository
    {
        public List<OperationClassMemberEntity> GetItemList(string FNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_OperationClassMember d
                                    INNER  JOIN Sys_OperationClass i ON i.FId = d.FOperationClassID
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
