using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FleetPro2019.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Role> RoleList { get; set; }
        public int ParentID { get; set; }
        public int? ReadRoleValue { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }

    public class Role
    {
        public Nullable<int> ModuleID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Value { get; set; }
        public Nullable<bool> Active { get; set; }
    }

    public class ModuleRoles
    {
        public string Module { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }

    }

    public class AccountSensorLabels
    {
        public int AccountID { get; set; }
        public string Sensor1Label { get; set; }
        public string Sensor2Label { get; set; }
    }
}