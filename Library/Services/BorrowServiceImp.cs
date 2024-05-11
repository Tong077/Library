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

        public bool Create(Borrow borrow, BorrowDetail borrowDetail)
        {
            using (var connection = _service.Connection)
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string insertBorrowQuery = @"
                    INSERT INTO Borrow (IsHidden, CustomerId, LibrarianId, BorrowDate, BorrowCode, Depositamount, Duedate, FineAmount, Memo) 
                    VALUES (@IsHidden, @CustomerId, @LibrarianId, @BorrowDate, @BorrowCode, @Depositamount, @Duedate, @FineAmount, @Memo);
                    SELECT SCOPE_IDENTITY();"; // Get the ID of the inserted Borrow record

                        int borrowId = connection.ExecuteScalar<int>(insertBorrowQuery, borrow, transaction: transaction);

                        string insertDetailsQuery = @"
                    INSERT INTO BorrowDetail (BorrowId, BookId, Note, IsReturn, ReturnDate) 
                    VALUES (@BorrowId, @BookId, @Note, @IsReturn, @ReturnDate);";

                        borrowDetail.BorrowId = borrowId; // Set the BorrowId for the BorrowDetail
                        connection.Execute(insertDetailsQuery, borrowDetail, transaction: transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return true;
        }


        public bool Delete(int borrowId)
        {
            var sql = "DELETE FROM Borrow Where BorrowId = @BorrowId";
            var roweEffect = _service.Connection.Execute(sql, new { @BorrowId = borrowId });
            return roweEffect > 0;
        }

        public Borrow Get(int BorrowId)
        {
            var sql = "SELECT * FROM Borrow Where BorrowId=@BorrowId";
            var borrow = _service.Connection.QueryFirstOrDefault<Borrow>(sql, new { BorrowId });
            return borrow!;
        }


        public IEnumerable<Borrow> GetAll()
        {
            var sql = @"SELECT Borrow.BorrowId,
                        Borrow.BorrowCode,
                        Borrow.BorrowDate,
                        Borrow.Duedate,
                        Borrow.Depositamount,
                        Borrow.FineAmount,
                        Borrow.IsHidden,
                        Borrow.Memo,
                        Customer.CustomerId,
                        Customer.CustomerName,
                        Librarian.LibrarianId,
                        Librarian.LibrarianName
                FROM Borrow 
                INNER JOIN Customer ON Borrow.CustomerId = Customer.CustomerId
                INNER JOIN Librarian ON Borrow.LibrarianId = Librarian.LibrarianId";
            var borrow = _service.Connection.Query<Borrow, Customer, Librarian, Borrow>(sql,
                (borrow, customer, librarian) =>
                {
                    borrow.Customer = customer;
                    borrow.Librarian = librarian;
                    return borrow;
                },
                splitOn: "CustomerId,LibrarianId");

            return borrow;
        }

        public bool Updade(Borrow borrow)
        {
            var sql = "UPDATE Borrow SET IsHidden=@IsHidden,CustomerId=@CustomerId,LibrarianId=@LibrarianId,BorrowCode=@BorrowCode,BorrowDate=@BorrowDate,Depositamount=@Depositamount,DueDate=@DueDate,FineAmount=@FineAmount,Memo=@Memo Where BorrowId=@BorrowId";
            var roweEffect = _service.Connection.Execute(sql, borrow);
            return roweEffect > 0;
        }
    }
}
