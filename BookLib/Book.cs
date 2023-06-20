using System;

namespace CourseWorkLibrary.BookLib
{
    public class Book : IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearOfPublication { get; set; }
        public int QuantityInStock { get; set; }
        public Book()
        { }

        public Book(int id, string title, string author, int yearOfPublication, int quantityInStock)
        {
            Id = id;
            Title = title;
            Author = author;
            YearOfPublication = yearOfPublication;
            QuantityInStock = quantityInStock;
        }

        public Book(string title, string author, int yearOfPublication, int quantityInStock)
        {
            Title = title;
            Author = author;
            YearOfPublication = yearOfPublication;
            QuantityInStock = quantityInStock;
        }
    }
}
