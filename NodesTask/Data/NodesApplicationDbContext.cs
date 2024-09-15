namespace NodesTask.Data
{
    using Microsoft.EntityFrameworkCore;
    using NodesTask.Data.Entities;

    public class NodesApplicationDbContext : DbContext
    {
        public DbSet<Tree> Trees { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<ExceptionJournal> ExceptionJournals { get; set; }

        public NodesApplicationDbContext(DbContextOptions<NodesApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Node>()
            .HasOne(n => n.Tree)
            .WithMany(t => t.Nodes)
            .HasForeignKey(n => n.TreeId);

            modelBuilder.Entity<Node>()
                .HasOne(n => n.ParentNode)
                .WithMany(p => p.Children)
                .HasForeignKey(n => n.ParentNodeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExceptionJournal>()
                .HasKey(e => e.EventId);
        }
    }
}
