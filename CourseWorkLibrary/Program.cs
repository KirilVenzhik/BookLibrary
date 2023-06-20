using System;
using CourseWorkLibrary;
using CourseWorkLibrary.BookLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Program
{
    class Library
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(70, 50);
            Console.SetBufferSize(70, 50);

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\kiril\\OneDrive\\Рабочий стол\\CourseWorks\\CourseWorkLibrary\\CourseWorkLibrary\\LibDb.mdf\";Integrated Security=True";
            var options = new DbContextOptionsBuilder<ProgramContext>()
                .UseSqlServer(connectionString)
                .Options;
            using var db = new ProgramContext(options);
            db.Database.EnsureCreated();

            var controller = new MenuFunctions(db);
            controller.Introduction();
        }
    }
}