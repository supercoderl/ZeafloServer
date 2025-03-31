using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.DomainNotifications;
using ZeafloServer.Infrastructure.Configurations.EventSourcing;

namespace ZeafloServer.Infrastructure.Database
{
    public class DomainNotificationStoreDbContext : DbContext
    {
        public virtual DbSet<StoreDomainNotification> StoreDomainNotifications { get; set; } = null!;

        public DomainNotificationStoreDbContext(DbContextOptions<DomainNotificationStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StoreDomainNotificationConfiguration());
        }
    }
}
