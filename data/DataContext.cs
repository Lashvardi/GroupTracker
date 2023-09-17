using GroupTracker.DTOs.GroupLectureSession;
using GroupTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupTracker.data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Lecturer> Lecturers { get; set;}
    public DbSet<LecturerGroup> LecturerGroups { get; set; }
    public DbSet<LectureSession> LectureSessions { get; set; }
    public DbSet<GroupLectureSession> GroupLectureSessions { get; set; }
    public DbSet<SyllabusTopic> SyllabusTopics { get; set; }
    public DbSet<AlternateWeek> AlternateWeeks { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<LecturerGroup>()
            .HasOne(lg => lg.CurrentSyllabusTopic)
            .WithMany()
            .HasForeignKey(lg => lg.CurrentSyllabusTopicId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
