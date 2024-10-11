using Microsoft.EntityFrameworkCore;

public class MoodDb : DbContext
{
    public MoodDb(DbContextOptions<MoodDb> options) : base(options) { }

    public DbSet<MoodEntity> Moods => Set<MoodEntity>();
}