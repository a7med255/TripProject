namespace TripProject.Models
{
    public class VwHome
    {
        public VwHome()
        {
            lstNewTrips = new List<VwTrip>();
            lstRandomlyTrips = new List<VwTrip>();
            lstBestSallerTrips = new List<VwTrip>();
        }

        public List<VwTrip> lstNewTrips { get; set; }
        public List<VwTrip> lstRandomlyTrips { get; set; }
        public List<VwTrip> lstBestSallerTrips { get; set; }


    }
}
