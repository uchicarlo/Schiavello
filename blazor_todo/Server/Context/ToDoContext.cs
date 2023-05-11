using blazor_todo.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace blazor_todo.Server.Context
{
	public class ToDoContext : DbContext
	{
		public DbSet<KanBanSection> KanBanSections { get; set; }
		public DbSet<KanbanTaskItem> KanbanTaskItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<KanBanSection>().HasKey(x => x.Id);
			modelBuilder.Entity<KanBanSection>().Property(x => x.Id).ValueGeneratedOnAdd();
			modelBuilder.Entity<KanbanTaskItem>().HasIndex(x => x.Status);
		}
	}
}
