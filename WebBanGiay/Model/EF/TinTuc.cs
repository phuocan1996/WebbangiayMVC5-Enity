namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTinTuc { get; set; }

        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public string HinhAnhDaiDien { get; set; }

        public bool? Moi { get; set; }

        public bool? NoiBat { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDang { get; set; }

        public string Mota { get; set; }
    }
}
