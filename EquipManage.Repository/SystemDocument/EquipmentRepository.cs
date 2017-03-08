using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace EquipManage.Repository.SystemDocument
{
    public class EquipmentRepository: RepositoryBase<EquipmentEntity>, IEquipmentRepository
    {
        public List<EquipmentEntity> GetEquipmentList(string FTypeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  d.*
                            FROM    Sys_Equipment d
                                    INNER  JOIN Sys_EquipmentType i ON i.FId = d.FEquipmentTypeId
                            WHERE   1 = 1
                                    AND i.FNumber = @FTypeID
                                    AND d.FEnabledMark = 1
                                    AND d.FDeleteMark = 0
                            ORDER BY d.FSortCode ASC");
            DbParameter[] parameter =
            {
                 new SqlParameter("@FTypeID",FTypeID)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
    }
}
