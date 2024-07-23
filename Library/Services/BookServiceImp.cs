using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Library.Services
{
    public class BookServiceImp : IBookService
    {
        private readonly DapperDbConnext _service;
        public BookServiceImp(DapperDbConnext service)
        {
            _service = service;
        }

        public bool BookCodeExists(string bookCode)
        {
            using (var connection = _service.Connection)
            {
                var sql = "SELECT BookCode FROM Book WHERE BookCode = @BookCode";
                var book = connection.QueryFirstOrDefault<string>(sql, new { BookCode = bookCode });
                return book != null;
            }
        }
        

        public bool Create(Book book)
        {
            var sql = "INSERT INTO Book(IsHiDden, CatalogId, BookCode,BookDescription) Values(@IsHiDden,@CatalogId,@BookCode,@BookDescription)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHiDden = book.IsHidden,
                CatalogId = book.CatalogId,
                BookCode = book.BookCode,
                BookDescription = book.BookDescription,

            });
            return roweEffect > 0;

           
            
        }
       


        public bool Delete(int bookId)
        {
            var sql = "DELETE FROM BOOK WHERE BookId = @BookId";
            var roweEffect = _service.Connection.Execute(sql, new { @BookId = bookId });

            return roweEffect > 0;
        }

        public Book Get(int BookId)
        {
            var sql = "SELECT * FROM BOOK WHERE BookId=@BookId";
            var book = _service.Connection.QueryFirstOrDefault<Book>(sql, new { BookId });
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            var sql = @"SELECT
                           Book.BookId,
                           Book.BookCode,
                           Book.BookDescription,
                           Book.IsHidden,
                           Book.CatalogId,
                           Catalog.CatalogName
                       FROM
                           Book
                       INNER JOIN
                           Catalog ON Book.CatalogId = Catalog.CatalogId";
            int pageNumber = 1;
            int pageSize = 10;
            var books = _service.Connection.Query<Book, Catalog, Book>(sql,
                (book, catalog) =>
                {
                    book.Catalog = catalog;
                    return book;
                }, splitOn: "CatalogId");

            return books;
        }

        

        public bool IsBorrowed(int BookId)
        {
            using (var connection = _service.Connection)
            {
                var sql = @"
                            SELECT COUNT(*)
                            FROM BorrowDetail
                            WHERE BookId = @BookId AND IsReturn = 0";

                var count = connection.QueryFirstOrDefault<int>(sql, new { BookId = BookId });
                return count > 0;
            }
        }

        public bool Update(Book book)
        {
            var sql = "UPDATE Book SET IsHidden=@IsHidden, CatalogId=@CatalogId,BookCode=@BookCode,BookDescription=@BookDescription WHERE BookId=@BookId";
            var roweEffect = _service.Connection.Execute(sql, book);

            return roweEffect > 0;
        }
    }
}
