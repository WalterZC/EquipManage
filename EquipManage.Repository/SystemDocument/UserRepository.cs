/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Code;
using EquipManage.Data;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Repository.SystemDocument;

namespace EquipManage.Repository.SystemDocument
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<UserEntity>(t => t.FId == keyValue);
                db.Delete<UserLogOnEntity>(t => t.FUserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(UserEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    userLogOnEntity.FId = userEntity.FId;
                    userLogOnEntity.FUserId = userEntity.FId;
                    userLogOnEntity.FUserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.FUserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.FUserPassword, 32).ToLower(), userLogOnEntity.FUserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }
    }
}
