//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FleetPro2019.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PGPS_SystemUser
    {
        public PGPS_SystemUser()
        {
            this.PGPS_SystemUser1 = new HashSet<PGPS_SystemUser>();
            this.PGPS_SystemUser11 = new HashSet<PGPS_SystemUser>();
        }
    
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string encrypted_password { get; set; }
        public System.DateTime created_time { get; set; }
        public string remarks { get; set; }
        public bool enabled { get; set; }
        public string api_key { get; set; }
        public string api_status { get; set; }
        public string email { get; set; }
        public System.DateTime last_update_time { get; set; }
        public Nullable<int> create_role_value { get; set; }
        public Nullable<int> read_role_value { get; set; }
        public Nullable<int> update_role_value { get; set; }
        public Nullable<int> delete_role_value { get; set; }
        public Nullable<int> parent_user_id { get; set; }
        public string GroupCode { get; set; }
        public string Sensor1Label { get; set; }
        public string Sensor2Label { get; set; }
        public Nullable<bool> alert { get; set; }
        public Nullable<System.DateTime> last_login_time { get; set; }
    
        public virtual ICollection<PGPS_SystemUser> PGPS_SystemUser1 { get; set; }
        public virtual PGPS_SystemUser PGPS_SystemUser2 { get; set; }
        public virtual ICollection<PGPS_SystemUser> PGPS_SystemUser11 { get; set; }
        public virtual PGPS_SystemUser PGPS_SystemUser3 { get; set; }
    }
}