/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Code;
using EquipManage.Data;
using EquipManage.Data.Extensions;
using EquipManage.Domain.Entity.SystemSecurity;
using EquipManage.Domain.IRepository.SystemSecurity;
using EquipManage.Repository.SystemSecurity;

namespace EquipManage.Repository.SystemSecurity
{
    public class DbBackupRepository : RepositoryBase<DbBackupEntity>, IDbBackupRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                var dbBackupEntity = db.FindEntity<DbBackupEntity>(keyValue);
                if (dbBackupEntity != null)
                {
                    FileHelper.DeleteFile(dbBackupEntity.FFilePath);
                }
                db.Delete<DbBackupEntity>(dbBackupEntity);
                db.Commit();
            }
        }
        public void ExecuteDbBackup(DbBackupEntity dbBackupEntity)
        {
            DbHelper.ExecuteSqlCommand(string.Format("backup database {0} to disk ='{1}'", dbBackupEntity.FDbName, dbBackupEntity.FFilePath));
            dbBackupEntity.FFileSize = FileHelper.ToFileSize(FileHelper.GetFileSize(dbBackupEntity.FFilePath));
            dbBackupEntity.FFilePath = "/Resource/DbBackup/" + dbBackupEntity.FFileName;
            this.Insert(dbBackupEntity);
        }
    }
}
