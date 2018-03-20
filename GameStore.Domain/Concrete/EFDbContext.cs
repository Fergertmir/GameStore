using GameStore.Domain.Entities;
using System.Data.Entity;

namespace GameStore.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public EFDbContext() : base("EFDbContext")
        {
            // Указывает EF, что если модель изменилась,
            // нужно воссоздать базу данных с новой структурой
            Database.SetInitializer(
                new NullDatabaseInitializer<EFDbContext>());
        }

        public DbSet<Game> Games { get; set; }
    }
}
