using TripProject.Models;

namespace TripProject.Bl
{

    public interface ICountry
    {
        public List<TbCountry> GetAll();
        public TbCountry GetById(int id);
        public bool Save(TbCountry country);
        public bool Delete(int id);
    }
    public class ClsCountry : ICountry
    {
        TripsContext context;

        public ClsCountry(TripsContext ctx)
        {
            context = ctx;
        }

        public List<TbCountry> GetAll()
        {
            try
            {
                var countries = context.TbCountries.ToList();
                return countries;
            }
            catch
            {

                return new List<TbCountry>();
            }
        }

        public TbCountry GetById(int id)
        {
            try
            {
                var country = context.TbCountries.FirstOrDefault(a => a.Id == id);
                return country;
            }
            catch
            {
                return new TbCountry();

            }

        }

        public bool Save(TbCountry country)
        {
            try
            {
                if (country.Id == 0)
                {
                    context.TbCountries.Add(country);
                }
                else
                {
                    context.Entry(country).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var country = GetById(id);
                context.Remove(country);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
