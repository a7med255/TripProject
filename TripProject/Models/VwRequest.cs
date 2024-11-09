namespace TripProject.Models
{
    public class VwRequest
    {
        public VwRequest()
        {
            lstCategories = new List<TbCategory>();
            lstCity = new List<TbCity>();
            lstRequest = new List<TbRequest>();
            VwTrips= new List<VwTrip>();
        }
        public List<TbCategory> lstCategories { get; set; }
        public List<TbCity> lstCity { get; set; }
        public List<TbRequest> lstRequest { get; set; }
        public List<VwTrip> VwTrips { get; set; }

    }
}
