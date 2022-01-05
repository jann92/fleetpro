using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FleetPro2019.Models;
using FleetPro2019.DAL;
using FleetPro2019.Helpers;
using System.Security.Claims;
using FleetPro2019.Helpers.SERVERDB;

namespace FleetPro2019.Controllers
{

    public class IP
    {
        public string IPAdd { get; set; }
    }

    public class AccountID
    {
        public int UserID { get; set; }
        public int ParentID { get; set; }
    }


    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        [Authorize]
        [Route("Module/List")]
        [HttpGet]
        public IHttpActionResult GetAccountModuleList()
        {
            var Identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> Claims = Identity.Claims;
            int AccountID = Claims.Where(x => x.Type == "AccountID").FirstOrDefault().Value.ToString().strToInt();
            int ReadRoleValue = Claims.Where(x => x.Type == "ReadRoleValue").FirstOrDefault().Value.ToString().strToInt();
            string ConfigurationName = Claims.Where(x => x.Type == "ConfigurationName").FirstOrDefault().Value.ToString();
            string WebConfigurationName = ConfigurationName.GetWebConfigurationName("TFDB");

            using (TFDBEntities tfdb = new TFDBEntities(WebConfigurationName))
            {
                List<ModuleRoles> ModuleRolesList = new List<ModuleRoles>();

                var checkuser = tfdb.PGPS_SystemUser.Where(x => x.user_id == AccountID).ToList();

                if (checkuser.Count() > 0)
                {
                    var user = checkuser.FirstOrDefault();

                    var readRole = tfdb.sp_PGPS_get_roles_list(user.read_role_value).ToList();

                    var createRole = tfdb.sp_PGPS_get_roles_list(user.create_role_value).ToList();

                    var updateRole = tfdb.sp_PGPS_get_roles_list(user.update_role_value).ToList();

                    var deleteRole = tfdb.sp_PGPS_get_roles_list(user.delete_role_value).ToList();

                    var moduleList = tfdb.PGPS_Module.ToList();


                    foreach (var module in moduleList)
                    {
                        ModuleRolesList.Add(new ModuleRoles()
                        {

                            Module = module.name,
                            Read = readRole.ToList().Where(x => x.ModuleID == module.module_id).FirstOrDefault().Active == true ? true : false,
                            Create = createRole.ToList().Where(x => x.ModuleID == module.module_id).FirstOrDefault().Active == true ? true : false,
                            Update = updateRole.ToList().Where(x => x.ModuleID == module.module_id).FirstOrDefault().Active == true ? true : false,
                            Delete = deleteRole.ToList().Where(x => x.ModuleID == module.module_id).FirstOrDefault().Active == true ? true : false,

                        });

                    }

                    return Ok(ModuleRolesList);

                }
                else
                {

                    return null;

                }
            }
        }

        [Authorize]
        [Route("Get/ParentID")]
        [HttpGet]
        public IHttpActionResult GetAccountParentID()
        {
            var Identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> Claims = Identity.Claims;
            int ParentID = Claims.Where(x => x.Type == "ParentID").FirstOrDefault().Value.ToString().strToInt();
            int AccountID = Claims.Where(x => x.Type == "AccountID").FirstOrDefault().Value.ToString().strToInt();

            var res = new AccountID()
            {
                UserID = AccountID,
                ParentID = ParentID
            };


            return Ok(res);
        }

        [Authorize]
        [Route("User/SensorLabels")]
        [HttpGet]
        public IHttpActionResult GetUserLabels()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> Claims = identity.Claims;
            int AccountID = Claims.Where(x => x.Type == "AccountID").FirstOrDefault().Value.ToString().strToInt();
            string ConfigurationName = Claims.Where(x => x.Type == "ConfigurationName").FirstOrDefault().Value.ToString();
            string WebConfigurationName = ConfigurationName.GetWebConfigurationName("TFDB");

            using (TFDBEntities tfdb = new TFDBEntities(WebConfigurationName))
            {
                var res = (from i in tfdb.PGPS_SystemUser.ToList()
                           where i.user_id == AccountID
                           select new AccountSensorLabels()
                           {
                               AccountID = i.user_id,
                               Sensor1Label = i.Sensor1Label,
                               Sensor2Label = i.Sensor2Label
                           }).ToList();
                if (res.Count() > 0)
                {
                    return Ok(res.FirstOrDefault());
                }
                else
                {
                    return null;
                }
            }
        }

        [Route("User/Email")]
        [HttpGet]
        public IHttpActionResult GetUserEmail()
        {
            var Identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> Claims = Identity.Claims;
            string UserEmail = Claims.Where(x => x.Type == "UserEmail").FirstOrDefault().Value.ToString();

            return Ok(UserEmail);
        }


        [Authorize]
        [Route("Logout")]
        [HttpPost]
        public IHttpActionResult AccountLogout(IP _IP)
        {
            var Identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> Claims = Identity.Claims;
            int AccountID = Claims.Where(x => x.Type == "AccountID").FirstOrDefault().Value.ToString().strToInt();
            string ConfigurationName = Claims.Where(x => x.Type == "ConfigurationName").FirstOrDefault().Value.ToString();
            string WebConfigurationName = ConfigurationName.GetWebConfigurationName("TFDB");

            using (TFDBEntities tfdb = new TFDBEntities(WebConfigurationName))
            {
                var checkuser = tfdb.PGPS_SystemUser.Where(x => x.user_id == AccountID).ToList();

                if(checkuser.Count() > 0)
                {
                    PGPS_UserLogs logs = new PGPS_UserLogs()
                    {
                        log_time = DateTime.Now,
                        user_id = AccountID,
                        log_type = true,
                        app = "fleetpro",
                        ip = _IP.IPAdd
                    };
                    tfdb.PGPS_UserLogs.Add(logs);
                    tfdb.SaveChanges();

                    return Ok("Logs successfully");
                }
                else
                {
                    return BadRequest("Log failed");
                }
            }

        }
    }
}
