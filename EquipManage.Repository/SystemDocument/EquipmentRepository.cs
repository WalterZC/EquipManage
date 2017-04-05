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
        /// <summary>
        /// 加入人员权限，获取该用户有权限部门的设备信息
        /// </summary>
        /// <param name="OrgId">用户所在部门Id</param>
        /// <param name="UserId">用户dI</param>
        /// <returns></returns>
        public List<EquipmentEntity> GetPermissionOrgItemList(string OrgId,string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH OrganizeTemp  
                            AS  
                            (  
                                SELECT * FROM Sys_Organize WHERE FId = @OrgId
                                UNION ALL  
                                SELECT m.* FROM (SELECT * FROM Sys_Organize WHERE FId IN (SELECT FObjectId FROM Sys_ItemRight WHERE FUserId=@UserId AND FObjectType='Organize'))  AS m  
                                INNER JOIN OrganizeTemp AS child ON m.FParentID = child.FID  
                            )
                            SELECT * FROM Sys_Equipment WHERE FBelongOrgID IN(SELECT DISTINCT FID FROM OrganizeTemp)
                            ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@OrgId",OrgId),
                 new SqlParameter("@UserId",UserId)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

        public List<EquipmentEntity> GetPermissionTypeItemList(string TypeId, string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH EquipmentTypeTemp  
                            AS  
                            (  
	                            SELECT * FROM Sys_EquipmentType WHERE FId = @TypeId
	                            UNION ALL  
	                            SELECT m.* FROM Sys_EquipmentType  AS m  
	                            INNER JOIN EquipmentTypeTemp AS child ON m.FParentId = child.FID  
                            )  
                            SELECT * FROM Sys_Equipment WHERE FEquipmentTypeId IN( SELECT FId FROM EquipmentTypeTemp)
                            AND FBelongOrgID IN(SELECT FObjectId FROM Sys_ItemRight WHERE FUserId=@UserId AND FObjectType='Organize')
                            ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@TypeId",TypeId),
                 new SqlParameter("@UserId",UserId)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

        /// <summary>
        /// 不加人员权限，获取该部门下所有的设备列表
        /// </summary>
        /// <param name="OrgId"></param>
        /// <returns></returns>
        public List<EquipmentEntity> GetSelectOrgItemList(string OrgId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH OrganizeTemp  
                            AS  
                            (  
                                SELECT * FROM Sys_Organize WHERE FId = @OrgId
                                UNION ALL  
                                SELECT m.* FROM Sys_Organize  AS m  
                                INNER JOIN OrganizeTemp AS child ON m.FParentID = child.FID  
                            )
                            SELECT * FROM Sys_Equipment WHERE FBelongOrgID IN(SELECT DISTINCT FID FROM OrganizeTemp)
                            ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@OrgId",OrgId)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

        public List<EquipmentEntity> GetSelectTypeItemList(string TypeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"WITH EquipmentTypeTemp  
                            AS  
                            (  
	                            SELECT * FROM Sys_EquipmentType WHERE FId = @TypeId
	                            UNION ALL  
	                            SELECT m.* FROM Sys_EquipmentType  AS m  
	                            INNER JOIN EquipmentTypeTemp AS child ON m.FParentId = child.FID  
                            )  
                            SELECT * FROM Sys_Equipment WHERE FEquipmentTypeId IN( SELECT FId FROM EquipmentTypeTemp)
                            ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@TypeId",TypeId)
            };
            return this.FindList(strSql.ToString(), parameter);
        }
    }
}
