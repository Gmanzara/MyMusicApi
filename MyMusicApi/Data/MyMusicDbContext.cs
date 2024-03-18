using Microsoft.EntityFrameworkCore;
using MyMusicApi.Core.Models;
using MyMusicApi.Data.Configuration;
using System.Linq.Expressions;

namespace MyMusicApi.Data
{
    public class MyMusicDbContext : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<User > Users { get; set; }

        public MyMusicDbContext(DbContextOptions<MyMusicDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MusicConfiguration());
            builder.ApplyConfiguration(new ArtistConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
