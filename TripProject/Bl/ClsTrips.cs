using TripProject.Models;

namespace TripProject.Bl
{
    public interface ITrips
    {
        public List<TbTrip> GetAll();
        public List<VwTrip> GetAllItemsData(int? categoryid, int? cityid);
        public TbTrip GetById(int id);
        public bool Save(TbTrip item);
        public bool Delete(int id);

    }

    public class ClsTrips : ITrips
    {
        TripsContext context;

        public ClsTrips(TripsContext ctx)
        {
            context = ctx;
        }

        public List<TbTrip> GetAll()
        {
            try
            {
                var trips = context.TbTrips.ToList();
                return trips;
            }
            catch
            {

                return new List<TbTrip>();
            }
        }
        public List<VwTrip> GetAllItemsData(int? categoryid, int? cityid)
        {
            try
            {
                var trips = context.VwTrips.Where(a => (a.CategoryId == categoryid || categoryid == 0 || categoryid == null) && (a.CityId == cityid || cityid == 0 || cityid == null)).OrderByDescending(a=>a.BestSellerRequest).ToList();
                return trips;
            }
            catch
            {

                return new List<VwTrip>();
            }
        }

        public TbTrip GetById(int id)
        {
            try
            {
                var trips = context.TbTrips.FirstOrDefault(a => a.Id == id);
                return trips;
            }
            catch
            {
                return new TbTrip();

            }

        }

        public bool Save(TbTrip trip)
        {
            try
            {
                if (trip.Id == 0)
                {
                    trip.CreatedDate = DateTime.Now;
                    trip.BestSellerRequest = 0;
                    context.Add(trip);
                }
                else
                {
                    trip.UpdateDate = DateTime.Now;
                    context.Entry(trip).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var trip = GetById(id);
                context.Remove(trip);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
