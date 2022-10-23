using SomeonesToDoListApp.DataAccessLayer.Entities;
using System.Data.Entity;

namespace SomeonesToDoListApp.DataAccessLayer.Context
{
	public class SomeonesToDoListContext : DbContext
	{
		public virtual DbSet<ToDo> ToDos { get; set; }

        public SomeonesToDoListContext()
            : base("SomeonesToDoListConnection")
        {
        }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<ToDo>()
                .HasKey(x => x.Id)
                .ToTable("dbo.ToDo");

			base.OnModelCreating(modelBuilder);
		}
    }
}
