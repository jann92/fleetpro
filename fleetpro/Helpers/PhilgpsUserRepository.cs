using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FleetPro2019.Models;
using FleetPro2019.DAL;
using System.Data.Entity.Validation;

namespace FleetPro2019.Helpers
{
    public class PhilgpsUserRepository
    {

        public string WebConfigurationName { get; set; }

        public PhilgpsUserRepository(string _WebConfigurationName)
        {
            this.WebConfigurationName = _WebConfigurationName;
        }

        public Account FindUser(string _Username, string _Password, string _IP)
        {
            using (TFDBEntities tfdb = new TFDBEntities(this.WebConfigurationName))
            {


                var result = (from s in tfdb.PGPS_SystemUser.ToList()
                              where s.username == _Username
                              && s.password == _Password
                              select new Account()
                              {
                                  AccountID = s.user_id,
                                  Password = s.password,
                                  Username = s.username,
                                  Email = s.email,
                                  ParentID = s.parent_user_id ?? 0,
                                  //LastLoginTime = s.last_login_time,
                                  //RoleList = (from i in tfdb.sp_PGPS_get_roles_list(s.read_role_value ?? 0).ToList()
                                  //            select new Role()
                                  //            {
                                  //                ModuleID = i.ModuleID,
                                  //                Name = i.Name,
                                  //                Value = i.Value,
                                  //                Active = i.Active
                                  //            }).ToList()
                                  ReadRoleValue = s.read_role_value
                              }).ToList();
                if (result.Count() > 0)
                {

                    var check = (from s in tfdb.PGPS_SystemUser.ToList()
                                 where s.user_id == result.FirstOrDefault().AccountID
                                 select s).ToList();

                    check.FirstOrDefault().last_login_time = DateTime.Now;




                    PGPS_UserLogs userlogs = new PGPS_UserLogs()
                    {
                        user_id = result.FirstOrDefault().AccountID,
                        log_time = DateTime.Now,
                        log_type = false,
                        app = "fleetpro",
                        ip = _IP
                    };

                    tfdb.PGPS_UserLogs.Add(userlogs);
                    tfdb.SaveChanges();

                    return result.FirstOrDefault();

                }
                else
                {
                    return null;
                }

            }
        }
    }
}