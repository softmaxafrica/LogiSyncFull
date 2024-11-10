using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace LogiSyncWebApi.Server.Models
{
    public class Location
    {
        [Key]
        [Column("LOCATION_ID")]
        public string LocationID { get; set; }
        [Column("TRUCK_ID")]
        public string TruckID { get; set; }
        
        [Column("LATITUDE")]
        public decimal Latitude { get; set; }

        [Column("LONGITUDE")]
        public decimal Longitude { get; set; }

        [Column("TIME_STAMP")]

        public DateTime Timestamp { get; set; }

        public Truck Truck { get; set; }
    }

}
