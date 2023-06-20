using CourseWorkLibrary.BookLib;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Transactions;
using System.Xml;

namespace Program
{
    class MenuFunctions : IMenuFunctions
    {
        private readonly ProgramContext db;

        public MenuFunctions(ProgramContext db)
        {
            this.db = db;
        }
        public MenuFunctions()
        { }

        public void Introduction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(25, 3);
            foreach (char c in "Welcome To")
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.SetCursorPosition(24, 4);
            foreach (char c in "The Library!")
            {
                Console.Write(c);
                Thread.Sleep(200);
            }
            Console.ResetColor();
            Console.SetCursorPosition(10, 25);
            Console.WriteLine("(press any key)");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }

        public void Menu()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 3);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Menu:");
            for (int i = 0; i < 60; i++)
            {
                Console.Write("_");
            }

            Console.Write("\n|");
            Console.ResetColor();
            Console.Write("   Commands:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t|");
            Console.ResetColor();
            Console.Write("   Description:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t   |");

            Console.WriteLine();
            for (int i = 0; i < 60; i++)
            {
                if (i == 0 || i == 59)
                    Console.Write("|");
                else
                    Console.Write("-");
            }

            Console.Write("\n|");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("   add");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t\t|");
            Console.ResetColor();
            Console.Write("   Add new book");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t   |");

            Console.Write("\n|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   del");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t\t|");
            Console.ResetColor();
            Console.Write("   Delete the book");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t   |");

            Console.Write("\n|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("   cor");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t\t|");
            Console.ResetColor();
            Console.Write("   Correct the book");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t   |");

            Console.Write("\n|");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("   list");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t|");
            Console.ResetColor();
            Console.Write("   Opens a list of books");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t   |");

            Console.Write("\n|");
            Console.ResetColor();
            Console.Write("   exit");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t\t|");
            Console.ResetColor();
            Console.Write("   Exites from the pfogram");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\t   |");

            Console.WriteLine();
            for (int i = 0; i < 60; i++)
            {
                Console.Write("-");
            }
            Console.ResetColor();



            Console.SetCursorPosition(2, 13);
            Console.Write(" Enter the command ");
            string command = Console.ReadLine();



            if (command.Length == 0)
            {
                Error(" Command must contain some text!");
                Menu();
            }
            switch (command)
            {
                case "add":
                    Add();
                    break;
                case "del":
                    Delete();
                    break;
                case "cor":
                    Correct();
                    break;
                case "list":
                    BookList();
                    break;
                case "exit":
                    Console.Clear();
                    Console.SetCursorPosition(2, 3);
                    Console.WriteLine("Good bye!");
                    Environment.Exit(0);
                    break;
                default:
                    Error(" Enter command in correct form!");
                    break;
            }
            Menu();
        }

        public void Add()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 3);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ADD THE BOOK:\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            int cursorPositionY = 4, numberOfRows = 4;
            TableToEnterTheBookInfo(cursorPositionY, numberOfRows);

            Console.ResetColor();



            Console.SetCursorPosition(2, cursorPositionY + 3);
            Console.Write(" Title: ");
            string eTitle = Console.ReadLine();
            if (eTitle.Length == 0)
            {
                Error("Empty field is not allowed! Enter some information pleace!");
                Menu();
            }



            Console.SetCursorPosition(2, cursorPositionY + 5);
            Console.Write("Author: ");
            string eAuthor = Console.ReadLine();
            if (eAuthor.Length == 0)
            {
                Error("Empty field is not allowed! Enter some information pleace!");
                Menu();
            }



            Console.SetCursorPosition(2, cursorPositionY + 7);
            Console.Write("Year of publcation: ");
            if (!int.TryParse(Console.ReadLine(), out int eYearOfPublication))
            {
                Error("Enter the number pleace!");
                Menu();
            }
            else if (eYearOfPublication < 500 || eYearOfPublication > DateTime.Today.Year)
            {
                Error("Enter a valid year pleace!");
                Menu();
            }



            Console.SetCursorPosition(2, cursorPositionY + 9);
            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int eQuantityInStock))
            {
                Error("Enter the number pleace!");
                Menu();
            }
            else if (eQuantityInStock < 0)
            {
                eQuantityInStock = 0;
            }

            var book = new Book(eTitle, eAuthor, eYearOfPublication, eQuantityInStock);
            db.Books.Add(book);
            db.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\n Congratilation! Book ({book.Title}) added to library successfully!");
            Console.ResetColor();

            Console.WriteLine("\n (press any button)");
            Console.ReadKey();
        }

        public void Delete()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DELETE THE BOOK:\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            int cursorPositionY = 4, numberOfRows = 1;
            TableToEnterTheBookInfo(cursorPositionY, numberOfRows);
            Console.ResetColor();



            Console.SetCursorPosition(2, cursorPositionY + 3);
            Console.Write(" Book ID: ");

            if (!int.TryParse(Console.ReadLine(), out int eId))
            {
                Error("Book ID can not include letters and symbols! Enter the number!");
                Menu();
            }

            var book = db.Books.Find(eId);
            if (book != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\n Congratilation! Book ({book.Title}) deleted from library successfully!");
                Console.ResetColor();
                db.Books.Remove(book);
                db.SaveChanges();
            }
            else
            {
                Error($"There is no Book with this ID({eId})!");
                Menu();
            }

            Console.WriteLine("\n (press any button)");
            Console.ReadKey();
        }

        public void Correct()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 3);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("CORRECT THE BOOK:\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            int cursorPositionY = 4, numberOfRows = 1;
            TableToEnterTheBookInfo(cursorPositionY, numberOfRows);
            Console.ResetColor();



            Console.SetCursorPosition(2, cursorPositionY + 3);
            Console.Write(" Book ID: ");

            if (!int.TryParse(Console.ReadLine(), out int eId))
            {
                Error("Book ID can not include letters and symbols! Enter the number!");
                Menu();
            }



            var book = db.Books.Find(eId);
            if (book != null)
            {
                Console.Clear();
                Console.SetCursorPosition(2, 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Book finded!");
                Console.ResetColor();
                Console.WriteLine("Current information about book:");

                int lastPosY = ShowBookInfo(book, 5), coef = 4;



                Console.ForegroundColor = ConsoleColor.Yellow;
                TableToEnterTheBookInfo(lastPosY + 1, 4);




                Console.ResetColor();
                Console.SetCursorPosition(2, lastPosY + coef);
                Console.Write(" Title: ");
                string eTitle = Console.ReadLine();
                if (eTitle.Length == 0)
                {
                    Error("Empty field is not allowed! Enter some information pleace!");
                    Menu();
                }


                coef += 2;
                Console.SetCursorPosition(2, lastPosY + coef);
                Console.Write("Author: ");
                string eAuthor = Console.ReadLine();
                if (eAuthor.Length == 0)
                {
                    Error("Empty field is not allowed! Enter some information pleace!");
                    Menu();
                }



                coef += 2;
                Console.SetCursorPosition(2, lastPosY + coef);
                Console.Write("Year of publcation: ");
                if (!int.TryParse(Console.ReadLine(), out int eYearOfPublication))
                {
                    Error("Enter the number pleace!");
                    Menu();
                }
                else if (eYearOfPublication < 500 || eYearOfPublication > DateTime.Today.Year)
                {
                    Error("Enter a valid year pleace!");
                    Menu();
                }



                coef += 2;
                Console.SetCursorPosition(2, lastPosY + coef);
                Console.Write("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int eQuantityInStock))
                {
                    Error("Enter the number pleace!");
                    Menu();
                }
                else if (eQuantityInStock < 0)
                {
                    eQuantityInStock = 0;
                }



                book.Title = eTitle;
                book.Author = eAuthor;
                book.YearOfPublication = eYearOfPublication;
                book.QuantityInStock = eQuantityInStock;
                db.SaveChanges();

                Console.SetCursorPosition(1, lastPosY + coef + 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratilations! Books has been updated!");
                Console.ResetColor();
            }
            else
            {
                Error($"There is no Book with this ID({eId})!");
                Menu();
            }

            Console.SetCursorPosition(50, 29);
            Console.WriteLine(" (press any button)");
            Console.ReadKey();
        }

        public void BookList()
        {
            const int PageSize = 3;
            int maxPage = (int)Math.Ceiling((double)db.Books.Count() / PageSize);
            int currentPage = 1;

            bool exit = false;
            bool needToSort = false;
            int counter;


            while (!exit)
            {
                Console.Clear();
                Console.SetCursorPosition(2, 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Showing the List:");
                Console.ResetColor();

                counter = 1 + 5 * (currentPage - 1);

                List<Book> books = null;

                books = db.Books
                    .Skip((currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                if (needToSort == true)
                {
                    books = db.Books
                        .OrderBy(book => book.YearOfPublication)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                }

                int currentLine = 3;
                Console.WriteLine($" Page: {currentPage}");

                foreach (var book in books)
                {
                    currentLine = ShowBookInfo(book, currentLine);
                    counter++;
                }




                Console.SetCursorPosition(0, 41);
                Console.ForegroundColor = ConsoleColor.Yellow;

                for (int i = 0; i < 60; i++)
                {
                    Console.Write("_");
                }

                Console.Write("\n|");
                Console.ResetColor();
                Console.Write("   Commands:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Description:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t   |");

                Console.WriteLine();
                for (int i = 0; i < 60; i++)
                {
                    if (i == 0 || i == 59)
                        Console.Write("|");
                    else
                        Console.Write("-");
                }

                Console.Write("\n|");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("   back");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Scrolls to the previous page");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("   |");

                Console.Write("\n|");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("   next");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Scrolls to the next page");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t   |");

                Console.Write("\n|");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("   sort");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Sort by year of publication");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t   |");

                Console.Write("\n|");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("   gbrb");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Get/Return book from/to Library");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("|");

                Console.Write("\n|");
                Console.ResetColor();
                Console.Write("   quit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t|");
                Console.ResetColor();
                Console.Write("   Quits the list");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t\t   |");

                Console.WriteLine();
                for (int i = 0; i < 60; i++)
                {
                    Console.Write("-");
                }
                Console.ResetColor();


                Console.SetCursorPosition(2, 40);
                Console.Write("Enter the command: ");
                string input = Console.ReadLine();
                if (input.Length == 0)
                {
                    Error("Command must contain symbols!");
                    continue;
                }

                switch (input)
                {
                    case "back":
                        if (currentPage > 1)
                            currentPage--;
                        else
                            Error("This is first page, so you can`t swipe back!");
                        break;

                    case "next":
                        if (currentPage < maxPage)
                            currentPage++;
                        else
                            Error("This is last page, so you can`t swipe next!");
                        break;

                    case "sort":
                        currentPage = 1;
                        if (needToSort == true)
                            needToSort = false;
                        else
                            needToSort = true;
                        break;

                    case "gbrb":
                        Console.SetCursorPosition(2, 40);
                        Console.Write("Enter the 'g'(to get)/'r'(to return): ");
                        string inputGR = Console.ReadLine();
                        if (inputGR.Length == 0)
                        {
                            Error("Command must contain symbols!");
                            continue;
                        }

                        if (inputGR == "g")
                        {
                            Console.Clear();
                            Console.SetCursorPosition(2, 3);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("GET THE BOOK:\n");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            int cursorPositionY = 4, numberOfRows = 1;
                            TableToEnterTheBookInfo(cursorPositionY, numberOfRows);
                            Console.ResetColor();



                            Console.SetCursorPosition(2, cursorPositionY + 3);
                            Console.Write(" Book ID: ");

                            if (!int.TryParse(Console.ReadLine(), out int eId))
                            {
                                Error("Book ID can not include letters and symbols! Enter the number!");
                                continue;
                            }

                            var book = db.Books.Find(eId);
                            if(book == null)
                            {
                                Error($"There is no book with Id ({eId})!");
                                continue;
                            }
                            else if(book.QuantityInStock <= 0)
                            {
                                Error("This book is not avilable!");
                                continue;
                            }
                            book.QuantityInStock--;
                            db.SaveChanges();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n\nThanks for using our library!");
                            Console.WriteLine("Don`t forget to return!)");
                            Console.ResetColor();

                            Console.SetCursorPosition(50, 30);
                            Console.WriteLine("(press any button)");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else if (inputGR == "r")
                        {
                            Console.Clear();
                            Console.SetCursorPosition(2, 3);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("RETURN THE BOOK:\n");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            int cursorPositionY = 4, numberOfRows = 1;
                            TableToEnterTheBookInfo(cursorPositionY, numberOfRows);
                            Console.ResetColor();



                            Console.SetCursorPosition(2, cursorPositionY + 3);
                            Console.Write(" Book ID: ");

                            if (!int.TryParse(Console.ReadLine(), out int eId))
                            {
                                Error("Book ID can not include letters and symbols! Enter the number!");
                                continue;
                            }

                            var book = db.Books.Find(eId);
                            if (book == null)
                            {
                                Error($"There is no book with Id ({eId})!");
                                continue;
                            }
                            book.QuantityInStock++;
                            db.SaveChanges();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n\nThanks for using our library!");
                            Console.WriteLine("Come again!)");
                            Console.ResetColor();

                            Console.SetCursorPosition(50, 30);
                            Console.WriteLine("(press any button)");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            Error("Incorrect input!");
                            continue;
                        }
                        break;

                    case "quit":
                        exit = true;
                        break;
                    default:
                        Error("No command exist!");
                        break;
                }
            }
        }

        public void TableToEnterTheBookInfo(int positionY, int numberOfRows)
        {
            Console.SetCursorPosition(5, positionY++);
            for (int rowLength = 0; rowLength < 30; rowLength++)
            {
                if (rowLength == 0 || rowLength == 29)
                {
                    Console.Write(" ");
                    continue;
                }
                Console.Write("_");
            }

            Console.SetCursorPosition(5, positionY);
            Console.Write("|");
            Console.SetCursorPosition(34, positionY);
            Console.WriteLine("|");

            for (int counter = positionY + 2; counter <= positionY + (2 * numberOfRows); counter += 2)
            {
                for (int rowLength = 0; rowLength < 40; rowLength++)
                {
                    if (rowLength == 0 || rowLength == 39)
                    {
                        if (counter == positionY + 2)
                        {
                            Console.Write(" ");
                            continue;
                        }
                        Console.Write("|");
                        continue;
                    }
                    Console.Write("-");
                }

                Console.Write("\n|");
                Console.SetCursorPosition(39, counter);
                Console.WriteLine("|");
            }

            for (int rowLength = 0; rowLength < 40; rowLength++)
            {
                if (rowLength == 0 || rowLength == 39)
                {
                    Console.Write(" ");
                    continue;
                }
                Console.Write("-");
            }
            Console.SetCursorPosition(7, positionY);
            Console.Write(" Enter the book info:");
        }

        public int ShowBookInfo(Book book, int rowCounter)
        {
            rowCounter++;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                Console.Write("-");
            }

            Console.Write($"\n| ");
            Console.SetCursorPosition(34, rowCounter);
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(2, rowCounter++);
            Console.WriteLine($"Id: {book.Id}");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                if (rowLength == 0 || rowLength == 34)
                    Console.Write("|");
                else
                    Console.Write("-");
            }
            Console.Write($"\n| ");
            Console.SetCursorPosition(34, rowCounter);
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(2, rowCounter++);
            Console.WriteLine($"Title: {book.Title}");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                if (rowLength == 0 || rowLength == 34)
                    Console.Write("|");
                else
                    Console.Write("-");
            }
            Console.Write($"\n| ");
            Console.SetCursorPosition(34, rowCounter);
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(2, rowCounter++);
            Console.WriteLine($"Author: {book.Author}");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                if (rowLength == 0 || rowLength == 34)
                    Console.Write("|");
                else
                    Console.Write("-");
            }
            Console.Write($"\n| ");
            Console.SetCursorPosition(34, rowCounter);
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(2, rowCounter++);
            Console.WriteLine($"Published: {book.YearOfPublication}");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                if (rowLength == 0 || rowLength == 34)
                    Console.Write("|");
                else
                    Console.Write("-");
            }
            Console.Write($"\n| ");
            Console.SetCursorPosition(34, rowCounter);
            Console.WriteLine("|");
            Console.ResetColor();

            Console.SetCursorPosition(2, rowCounter++);
            Console.Write($"In stock:");
            if (book.QuantityInStock == 0)
                Console.WriteLine(" out of stock");
            else
                Console.WriteLine($" present ({book.QuantityInStock})");



            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, rowCounter++);
            for (int rowLength = 0; rowLength < 35; rowLength++)
            {
                Console.Write("-");
            }
            Console.ResetColor();

            return rowCounter;
        }

        public void Error(string errorDescription)
        {
            Console.Clear();
            Console.SetCursorPosition(25, 3);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR");
            Console.SetCursorPosition(5, 7);
            Console.WriteLine($"Description:\n {errorDescription}");
            Console.ResetColor();
            Console.SetCursorPosition(50, 10);
            Console.WriteLine("(press any button)");
            Console.ReadKey();
            Console.Clear();
        }
    }
}