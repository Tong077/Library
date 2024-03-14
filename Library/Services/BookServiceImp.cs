using Dapper;
using Library.Data;
using Library.Models;

namespace Library.Services
{
    public class BookServiceImp :IBookService
    {
        private readonly DapperDbConnext _service;
        public BookServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }

        public bool Create(Book book)
        {
            var sql = "INSERT INTO Book(IsHiDden, CatalogId, BookCode,BookDescription) Values(@IsHiDden,@Catalog,@BookCode,BookDescription)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHiDden = book.IsHidden,
                CatalogId = book.CatalogId,
                BookCode = book.BookCode,
                BookDescription = book.BookDescription,

            });
            return roweEffect > 0;
        }

        public bool Delete(Book book)
        {
            var sql = "DELETE FROM BOOK WHERE BookId = @BookId";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                BookId = book.BookId,
            });
            return roweEffect > 0;
        }

        public Book Get(int BookId)
        {
            var sql = "SELECT * FROM BOOK WHERE BookId=@BooId";
            var book = _service.Connection.QueryFirstOrDefault<Book>(sql, new { BookId });
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            var sql = "SELECT * FROM Book";
            var books = _service.Connection.Query<Book>(sql);
            return books;
        }

        public bool Update(Book book)
        {
            var sql = "UPDATE Book SET IsHidden=@IsHidden, CatalogId=@CatalogId,BookCode=@BookCode,BookDescription=@BookDescription WHERE BookId=@BookId)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                BookId = book.BookId,
            });
            return roweEffect > 0;
        }
    }
}
