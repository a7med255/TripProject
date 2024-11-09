using TripProject.Models;

namespace TripProject.Bl
{

    public interface ICategories
    {
        public List<TbCategory> GetAll();
        public TbCategory GetById(int id);
        public bool Save(TbCategory category);
        public bool Delete(int id);
    }
    public class ClsCategories : ICategories
    {
        TripsContext context;

        public ClsCategories(TripsContext ctx)
        {
            context = ctx;
        }

        public List<TbCategory> GetAll()
        {
            try
            {
                var categories = context.TbCategories.ToList();
                return categories;
            }
            catch
            {

                return new List<TbCategory>();
            }
        }

        public TbCategory GetById(int id)
        {
            try
            {
                var category = context.TbCategories.FirstOrDefault(a => a.Id == id);
                return category;
            }
            catch
            {
                return new TbCategory();

            }

        }

        public bool Save(TbCategory category)
        {
            try
            {
                if (category.Id == 0)
                {
                    context.TbCategories.Add(category);
                }
                else
                {
                    context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var category = GetById(id);
                context.Remove(category);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
