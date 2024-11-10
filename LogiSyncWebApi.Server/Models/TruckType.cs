using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiSyncWebApi.Server.Models
{
    public class TruckType
    {
        [Key]
        [Column("TRUCK_TYPE_ID")]
        public string TruckTypeID { get; set; }

        [Required]
        [Column("TYPE_NAME")]
        public string TypeName { get; set; }

        [Required]
        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("SAMPLE_IMAGE_URL")]
        public string? SampleImageUrl { get; set; }  // Field for storing the URL of the sample truck image

        //public ICollection<Truck> Trucks { get; set; }
    }
}
