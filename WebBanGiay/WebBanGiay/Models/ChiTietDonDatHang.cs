//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanGiay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietDonDatHang
    {
        public int IdCTDDH { get; set; }
        public Nullable<int> IdDDH { get; set; }
        public Nullable<int> IdGiay { get; set; }
        public string TenGiay { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
    
        public virtual DonDatHang DonDatHang { get; set; }
        public virtual Giay Giay { get; set; }
    }
}
