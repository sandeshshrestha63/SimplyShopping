//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace e_commerce.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Users
    {
        public long id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int type { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string contact_info { get; set; }
        public string photo { get; set; }
        public bool status { get; set; }
        public string activate_code { get; set; }
        public string reset_code { get; set; }
        public System.DateTime created_on { get; set; }
    }
}
