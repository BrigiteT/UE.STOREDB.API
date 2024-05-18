using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UE.STOREDB.DOMAIN.Core.Entities;

namespace UE.STOREDB.DOMAIN.Core.DTO
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public string? ImageUrl { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

    }

    public class ProductDetailListDTO
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedAt { get; set; }


    }
}
