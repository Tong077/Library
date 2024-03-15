using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.VisualBasic;

namespace Library.Services
{
    public class BorrowServiceImp : IBorrowService
    {
        private readonly DapperDbConnext _service;
        public BorrowServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }

        public bool Create(Borrow borrow)
        {
            var sql = "INSERT INTO Borrow(IsHidden, CustomerId,LibrarianId,BorrowDate,BorrowCode,Desitamount,Duedate,FineAmount,Memo) Values(@IsHidden,@CustomerId,@BorrowDate,@BorrowCode,@Despositamount,@DueDate,@FineAmount,@Memo)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHidden = borrow.IsHidden,
                CustomerId = borrow.CustomerId,
                LibrarianId = borrow.LibrarianId,
                BorrowDate = borrow.BorrowDate,
                Despositamount = borrow.Despositamount,
                DueDate = borrow.DueDate,
                FineAmount = borrow.FineAmount,
                Memo = borrow.Memo,
            });
            return roweEffect > 0;
        }

        public bool Delete(Borrow borrow)
        {
            var sql = "DELECT FROM Borrow Where BorrowId = @BorrowId";
            var roweEffect = _service.Connection.Execute(sql, new { borrow });
            return roweEffect > 0;
        }

        public Borrow Get(int BorrowId)
        {
            var sql = "SELECT * FROM Borrow Where BorrowId = @BorrowId";
            var borrow = _service.Connection.QueryFirstOrDefault<Borrow>(sql, new {BorrowId});
            return borrow;
        }

        public IEnumerable<Borrow> GetAll()
        {
            var sql = "SELECT * FROM Borrow";
            var borrows = _service.Connection.Query<Borrow>(sql);
            return borrows;
        }

        public bool Updade(Borrow borrow)
        {
            var sql = "UPDATE Borrow SET IsHidden=@IsHidden,CustomerId=@CustomerId,LibrarianId=@LibrarianId,BorrowDate=@BorrowDate,Despositamount=@Despositamount,DueDate=@DueDate,FineAmount=@FineAmount,Memo=@Memo Where BorrowId= @BorrowId";
            var roweEffect = _service.Connection.Execute(sql, new { borrow });
            return roweEffect > 0;
        }
    }
}
