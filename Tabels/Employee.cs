//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Tabels
{
    using System;
    using System.Collections.Generic;
    [KnownType(typeof(employee))]
    public partial class employee
    {
        public employee(){}
        public int id { get; set; }
        public string name { get; set; }
        public System.DateTime date_of_hiring { get; set; }
        public string sex { get; set; }
        
        public  int? department { get; set; }

      
        public  virtual department department1 { get; set; }
    }
}