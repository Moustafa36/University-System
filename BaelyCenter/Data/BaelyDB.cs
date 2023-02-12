using BaelyCenter.Models;
using Microsoft.EntityFrameworkCore;


namespace BaelyCenter.Data
{
    public class BaelyDB : DbContext
    {

        //public BaelyDB() : base()
        //{

        //}
        public BaelyDB(DbContextOptions<BaelyDB> Options) : base(Options)
        {

        }
        //public BaelyDB(DbContextOptions)
        //{

        //}
        public DbSet<Course> courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentsCourses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Department> departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-2QMF0UN\\SS17;Initial Catalog=BaelyCollage;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.Student_Id, x.Course_Id }); //make composit Key for both Student ID and course ID

            modelBuilder.Entity<StudentCourse>()
                .HasOne(s => s.student)
                .WithMany(sc => sc.StudentCourse)
                .HasForeignKey(s => s.Student_Id); //the relationship between student and Student Course (on to many)

            modelBuilder.Entity<StudentCourse>()
                .HasOne(c => c.Course)
                .WithMany(sc => sc.StudentCourse)
                .HasForeignKey(c => c.Course_Id);  //the realationship between Course and student course (on to many)
        }



    }
}
