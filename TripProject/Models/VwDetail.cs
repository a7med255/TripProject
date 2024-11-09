namespace TripProject.Models
{
    public class VwDetail
    {
        public VwDetail()
        {
            vwTrips = new VwTrip();
            TbRequest = new TbRequest();
        }
        public VwTrip vwTrips {  get; set; }
        public TbRequest TbRequest { get; set; }
    }
}
