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
    public class PositionRepository : RepositoryBase<PositionEntity>, IPositionRepository
    {
        public List<PositionEntity> GetItemList(string FBelongOrgID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH OrganizeTemp  
                            AS  
                            (  
                            SELECT * FROM Sys_Organize WHERE FId = @FBelongOrgID
                            UNION ALL  
                            SELECT m.* FROM Sys_Organize  AS m  
                            INNER JOIN OrganizeTemp AS child ON m.FParentID = child.FID  
                            )  
                            SELECT DISTINCT * FROM Sys_Position WHERE FBelongOrgID IN(SELECT DISTINCT FID FROM OrganizeTemp)");
            DbParameter[] parameter =
            {
                 new SqlParameter("@FBelongOrgID",FBelongOrgID)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
    }
}
