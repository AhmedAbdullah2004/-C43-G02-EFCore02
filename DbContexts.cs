using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ITIDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Stud_Course> Stud_Courses { get; set; }
    public DbSet<Course_Inst> Course_Insts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-FT06FQ7;Database=ITI;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(s => s.ID);
        modelBuilder.Entity<Department>().HasKey(d => d.ID);
        modelBuilder.Entity<Course>().HasKey(c => c.ID);
        modelBuilder.Entity<Instructor>().HasKey(i => i.ID);
        modelBuilder.Entity<Topic>().HasKey(t => t.ID);
        modelBuilder.Entity<Stud_Course>().HasKey(sc => new { sc.Stud_ID, sc.Course_ID });
        modelBuilder.Entity<Course_Inst>().HasKey(ci => new { ci.Inst_ID, ci.Course_ID });

        modelBuilder.Entity<Student>()
            .HasOne<Department>()
            .WithMany()
            .HasForeignKey(s => s.Dep_Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Department>()
            .HasOne<Instructor>()
            .WithMany()
            .HasForeignKey(d => d.Ins_ID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne<Topic>()
            .WithMany()
            .HasForeignKey(c => c.Top_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Instructor>()
            .HasOne<Department>()
            .WithMany()
            .HasForeignKey(i => i.Dept_ID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Stud_Course>()
            .HasOne<Student>()
            .WithMany()
            .HasForeignKey(sc => sc.Stud_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Stud_Course>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(sc => sc.Course_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Course_Inst>()
            .HasOne<Instructor>()
            .WithMany()
            .HasForeignKey(ci => ci.Inst_ID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Course_Inst>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(ci => ci.Course_ID)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}

public class Student
{
    [Key]
    public int ID { get; set; }

    [Required, MaxLength(50)]
    public string FName { get; set; }

    [Required, MaxLength(50)]
    public string LName { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

    [Range(18, 100)]
    public int Age { get; set; }

    public int Dep_Id { get; set; }
}

public class Department
{
    [Key]
    public int ID { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public int Ins_ID { get; set; }
    public DateTime HiringDate { get; set; }
}

public class Course
{
    [Key]
    public int ID { get; set; }

    public int Duration { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public int Top_ID { get; set; }
}

public class Instructor
{
    [Key]
    public int ID { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public decimal Salary { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

    public decimal HourRateBonus { get; set; }

    public int Dept_ID { get; set; }
}

public class Topic
{
    [Key]
    public int ID { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }
}

public class Stud_Course
{
    public int Stud_ID { get; set; }
    public int Course_ID { get; set; }
    public decimal Grade { get; set; }
}

public class Course_Inst
{
    public int Inst_ID { get; set; }
    public int Course_ID { get; set; }
    public int Evaluate { get; set; }
}
