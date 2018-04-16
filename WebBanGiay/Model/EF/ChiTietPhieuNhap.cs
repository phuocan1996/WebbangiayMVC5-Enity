namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCTPN { get; set; }

        public int? IdPN { get; set; }

        public decimal? DonGiaNhap { get; set; }

        public int? SoLuongNhap { get; set; }

        public int? IdGiay { get; set; }

        public virtual Giay Giay { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
