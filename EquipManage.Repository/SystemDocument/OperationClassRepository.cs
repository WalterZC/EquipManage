/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-03-02
*********************************************************************************/
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
    public class OperationClassRepository : RepositoryBase<OperationClassEntity>, IOperationClassRepository
    {
        public List<OperationClassEntity> GetItemList(string FOrgID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH OrganizeTemp  
                            AS  
                            (  
                                SELECT * FROM Sys_Organize WHERE FId = @FOrgID
                                UNION ALL  
                                SELECT m.* FROM Sys_Organize  AS m  
                                INNER JOIN OrganizeTemp AS child ON m.FParentID = child.FID  
                            )  
                            SELECT DISTINCT * FROM Sys_OperationClass WHERE FBelongOrgID IN(SELECT DISTINCT FID FROM OrganizeTemp)");
            DbParameter[] parameter =
            {
                 new SqlParameter("@FOrgID",FOrgID)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
    }
}
