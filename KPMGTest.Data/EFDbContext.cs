
using KPMGTest.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KPMGTest.Data
{
    public class EFDbContext: DbContext
    {
        #region Constructor

        public EFDbContext()
            : base("EFDbContext")
        {
            //Use this if u dont wan ef to check
            Database.SetInitializer<EFDbContext>(null);
           // this.Database.CommandTimeout = 300; need to check
        }
        #endregion Constructor


        public DbSet<Tax_Information> Tax_Information { get; set; }
       
        public void MarkAsModified(object entity, params string[] properties)
        {
            foreach (var property in properties)
                this.Entry(entity).Property(property).IsModified = true;
        }
        
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public static T NewContext<T>(Func<EFDbContext, T> func)
        {
            using (var con = EFDbContext.Create())
            {
                return func(con);
            }
        }

        public static void NewContext(Action<EFDbContext> func)
        {
            using (var con = EFDbContext.Create())
            {
                func(con);
            }
        }

       
 

        
    }
}