using System.Xml.Serialization;
using TripProject.Models;

namespace TripProject.Bl
{

    public interface IRequests
    {
        public List<TbRequest> GetAll();
        public TbRequest GetById(int id);
        public List<TbRequest> GetAllRequsetData(int? categoryid, int? cityid);
        public bool Save(TbRequest request);
        public bool Delete(int id);
    }
    public class ClsRequest : IRequests
    {
        TripsContext context;

        public ClsRequest(TripsContext ctx)
        {
            context = ctx;
        }

        public List<TbRequest> GetAll()
        {
            try
            {
                var requests = context.TbRequests.ToList();
                return requests;
            }
            catch
            {

                return new List<TbRequest>();
            }
        }
        public List<TbRequest> GetAllRequsetData(int? categoryid, int? cityid)
        {
            try
            {
                var trips = context.VwTrips.Where(a => (a.CategoryId == categoryid || categoryid == 0 || categoryid == null) && (a.CityId == cityid || cityid == 0 || cityid == null)).Select(a=>a.Id).ToList();
                var request=context.TbRequests.Where(a=> trips.Contains(a.RequestId.Value)).ToList();
                return request;
            }
            catch
            {

                return new List<TbRequest>();
            }
        }
        public TbRequest GetById(int id)
        {
            try
            {
                var request = context.TbRequests.FirstOrDefault(a => a.Id == id);
                return request;
            }
            catch
            {
                return new TbRequest();

            }

        }

        public bool Save(TbRequest request)
        {
            try
            {
                if (request.Id == 0)
                {
                    request.CreateDate = DateTime.Now;
                    context.TbRequests.Add(request);
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
                var request = GetById(id);
                var req = context.TbTrips.FirstOrDefault(a=>a.Id==request.RequestId);
                if (req != null )
                {
                    req.BestSellerRequest -= 1;
                }
                context.Remove(request);
                context.SaveChanges();
               
                return true;
            }
            catch { return false; }
        }
    }
}
