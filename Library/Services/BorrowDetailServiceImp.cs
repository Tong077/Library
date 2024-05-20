using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BorrowDetailServiceImp : IBorrowDetailService
    {
        private readonly DapperDbConnext _service;
        public BorrowDetailServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }

        public bool Create(BorrowDetail borrowDetail)
        {
            var sql = "INSERT INTO BorrowDetail(BorrowId,BookId,Note,IsReturn,ReturnDate) Values(@BorrowId,@BookId,@Note,@IsReturn,@ReturnDate)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                BorrowId = borrowDetail.BorrowId,
                BookId = borrowDetail.BookId,
                Note = borrowDetail.Note,
                IsReturn = borrowDetail.IsReturn,
                ReturnDate = borrowDetail.ReturnDate,
            });
            return roweEffect > 0;
        }


        public bool Delete(int borrowDetailId)
        {
            var sql = "DELETE FROM BorrowDetail Where BorrowDetailId=@BorrowDetailId";
            var roweEffect = _service.Connection.Execute(sql, new { @borrowDetailId = borrowDetailId });
            return roweEffect > 0;

        }

        public BorrowDetail Get(int BorrowDetailId)
        {
            var sql = "SELECT * FROM BorrowDetail WHERE BorrowDetailId=@BorrowDetailId";
            var borrowdetail = _service.Connection.QueryFirstOrDefault<BorrowDetail>(sql, new { BorrowDetailId });

            return borrowdetail!;
        }

        public IEnumerable<BorrowDetail> GetAll()
        {
            //var sql = "SELECT   bd.BorrowDetailId,bd.BorrowDate,b.BookId,b.BookCode,b.BookDescription,c.CatalogId,c.CatalogName FROM BorrowDetail INNER JOIN Book b ON bd.BookId = b.BookId INNER JOIN Catalog c ON b.CatalogId = c.CatalogId Were c.CatalogName";

            //var bodetail  = _service.Connection.Query<BorrowDetail>(sql, )
            var sql = "SELECT * FROM Borrowdetail";
            var borrowdetail = _service.Connection.Query<BorrowDetail>(sql);
            return borrowdetail.ToList();
           
        }

        public bool Update(BorrowDetail borrowDetail)
        {
            var sql = "UPDATE BorrowDetail SET BorrowId=@BorrowId, BookId=@BookId,Note=@Note,IsReturn=@IsReturn,ReturnDate=@ReturnDate Where BorrowDetailId=@BorrowDetailId";
            var roweEffect = _service.Connection.Execute(sql, borrowDetail);
            return roweEffect > 0;

        }
    }
}
