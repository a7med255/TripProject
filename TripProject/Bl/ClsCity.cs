using TripProject.Models;

namespace TripProject.Bl
{

    public interface ICity
    {
        public List<TbCity> GetAll(int? countryId);
        public List<TbCity> GetAllCity(int? id);
        public TbCity GetById(int id);
        public bool Save(TbCity city);
        public bool Delete(int id);
    }
    public class ClsCity : ICity
    {
        TripsContext context;

        public ClsCity(TripsContext ctx)
        {
            context = ctx;
        }

        public List<TbCity> GetAll(int? countryId)
        {
            try
            {
                var city = context.TbCities.Where(a => (a.CounturyId == countryId || countryId == 0 || countryId == null)).ToList();
                return city;
            }
            catch
            {

                return new List<TbCity>();
            }
        }
        public List<TbCity> GetAllCity(int? id)
        {
            try
            {
                var city = context.TbCities.Where(a => a.CounturyId == id).ToList();
                return city;
            }
            catch
            {

                return new List<TbCity>();
            }
        }
        public TbCity GetById(int id)
        {
            try
            {
                var city = context.TbCities.FirstOrDefault(a => a.Id == id);
                return city;
            }
            catch
            {
                return new TbCity();

            }

        }

        public bool Save(TbCity city)
        {
            try
            {
                if (city.Id == 0)
                {
                    context.TbCities.Add(city);
                }
                else
                {
                    context.Entry(city).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var city = GetById(id);
                context.Remove(city);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
