using System;

namespace CourseWorkLibrary.BookLib
{
    interface IProduct
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        int YearOfPublication { get; set; }
        int QuantityInStock { get; set; }
    }
}