using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Program;
using System;
using System.ComponentModel.Design;

namespace CourseWorkLibrary.BookLib
{
    public class ProgramContext : DbContext
    {
        public ProgramContext()
        {
        
        }

        public ProgramContext(DbContextOptions<ProgramContext> options)
            : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
