/*******************************************************************************
 * Copyright © 2017 EquipManage.Framework 版权所有
 * Author: 王成元
 * Description: 设备管理系统-匠盟科技
 * Date:2017-02-17
*********************************************************************************/
using EquipManage.Application.SystemManage;
using EquipManage.Code;
using EquipManage.Domain.Entity.SystemDocument;
using EquipManage.Domain.Entity.SystemManage;
using EquipManage.Domain.IRepository.SystemDocument;
using EquipManage.Domain.ViewModel;
using EquipManage.Repository.SystemDocument;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EquipManage.Application.SystemDocument
{
    public class RoleAuthorizeApp
    {
        private IRoleAuthorizeRepository service = new RoleAuthorizeRepository();
        private ModuleApp moduleApp = new ModuleApp();
        private ModuleButtonApp moduleButtonApp = new ModuleButtonApp();

        public List<RoleAuthorizeEntity> GetList(string ObjectId)
        {
            return service.IQueryable(t => t.FObjectId == ObjectId).ToList();
        }
        public List<ModuleEntity> GetMenuList(string roleId)
        {
            var data = new List<ModuleEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleApp.GetList();
            }
            else
            {
                var moduledata = moduleApp.GetList();
                List<RoleAuthorizeEntity> authorizedata = new List<RoleAuthorizeEntity>();
                string[] FRoleArray = roleId.Split(',');
                if (FRoleArray.Length > 0)
                {
                    foreach (string FRoleId in FRoleArray)
                    {
                        if (!string.IsNullOrEmpty(FRoleId))
                        {
                            authorizedata.AddRange(service.IQueryable(t => t.FObjectId == FRoleId && t.FItemType == 1).ToList());
                        }
                    }
                }
                foreach (var item in authorizedata)
                {
                    ModuleEntity moduleEntity = moduledata.Find(t => t.FId == item.FItemId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.OrderBy(t => t.FSortCode).ToList();
        }
        public List<ModuleButtonEntity> GetButtonList(string roleId)
        {
            var data = new List<ModuleButtonEntity>();
            if (OperatorProvider.Provider.GetCurrent().IsSystem)
            {
                data = moduleButtonApp.GetList();
            }
            else
            {
                var buttondata = moduleButtonApp.GetList();
                //var authorizedata = service.IQueryable(t => t.FObjectId == roleId && t.FItemType == 2).ToList();

                List<RoleAuthorizeEntity> authorizedata = new List<RoleAuthorizeEntity>();
                string[] FRoleArray = roleId.Split(',');
                if (FRoleArray.Length > 0)
                {
                    foreach (string FRoleId in FRoleArray)
                    {
                        if (!string.IsNullOrEmpty(FRoleId))
                        {
                            authorizedata.AddRange(service.IQueryable(t => t.FObjectId == FRoleId && t.FItemType == 2).ToList());
                        }
                    }
                }
                foreach (var item in authorizedata)
                {
                    ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.FId == item.FItemId);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.FSortCode).ToList();
        }
        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            var authorizeurldata = new List<AuthorizeActionModel>();
            var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            if (cachedata == null)
            {
                var moduledata = moduleApp.GetList();
                var buttondata = moduleButtonApp.GetList();

                List<RoleAuthorizeEntity> authorizedata = new List<RoleAuthorizeEntity>();
                string[] FRoleArray = roleId.Split(',');
                if (FRoleArray.Length > 0)
                {
                    foreach (string FRoleId in FRoleArray)
                    {
                        if (!string.IsNullOrEmpty(FRoleId))
                        {
                            authorizedata.AddRange(service.IQueryable(t => t.FObjectId == FRoleId).ToList());
                        }
                    }
                }
                //var authorizedata = service.IQueryable(t => t.FObjectId == roleId).ToList();
                foreach (var item in authorizedata)
                {
                    if (item.FItemType == 1)
                    {
                        ModuleEntity moduleEntity = moduledata.Find(t => t.FId == item.FItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { FId = moduleEntity.FId, FUrlAddress = moduleEntity.FUrlAddress });
                    }
                    else if (item.FItemType == 2)
                    {
                        ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.FId == item.FItemId);
                        authorizeurldata.Add(new AuthorizeActionModel { FId = moduleButtonEntity.FModuleId, FUrlAddress = moduleButtonEntity.FUrlAddress });
                    }
                }
                CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            }
            else
            {
                authorizeurldata = cachedata;
            }
            authorizeurldata = authorizeurldata.FindAll(t => t.FId.Equals(moduleId));
            foreach (var item in authorizeurldata)
            {
                if (!string.IsNullOrEmpty(item.FUrlAddress))
                {
                    string[] url = item.FUrlAddress.Split('?');
                    if (item.FId == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
