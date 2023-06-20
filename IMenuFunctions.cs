using CourseWorkLibrary.BookLib;
using System;

namespace Interfaces
{
    internal interface IMenuFunctions
    {
        public void Introduction();
        public void Menu();
        void Add();
        void Delete();
        void Correct();
        void BookList();
        void Error(string errorDescription);
    }
}
