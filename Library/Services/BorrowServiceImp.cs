using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public bool Delete(int BorrowId, int BorrowDetailId)
        {
            var query = @"DELETE FROM Borrow WHERE BorrowId = @BorrowId;
                      DELETE FROM BorrowDetail WHERE BorrowDetailId = @BorrowDetailId";
            var parameters = new { BorrowId = BorrowId, BorrowDetailId = BorrowDetailId };
            var rowsAffected = _service.Connection.Execute(query, parameters);
            return rowsAffected > 0;
        }


        public BorrowandBorrowDetail Get(int BorrowId, int BorrowDetailId)
        {
           

            string sql = @"
                            SELECT * FROM Borrow WHERE BorrowId = @BorrowId;
                            SELECT * FROM BorrowDetail WHERE BorrowDetailId = @BorrowDetailId;";

            using (var multi = _service.Connection.QueryMultiple(sql, new { BorrowId = BorrowId, BorrowDetailId = BorrowDetailId }))
            {
                var borrow = multi.ReadFirstOrDefault<Borrow>();
                var borrowDetail = multi.ReadFirstOrDefault<BorrowDetail>();

                if (borrow == null || borrowDetail == null)
                {
                    throw new Exception("Borrow or BorrowDetail record not found.");
                }

               
                return new BorrowandBorrowDetail
                {
                    Borrow = borrow,
                    BorrowDetail = borrowDetail
                };
            }
            

        }

        public IEnumerable<Borrow> GetAll()
        {
            var sql = @"
                        SELECT Borrow.BorrowId,
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

        public bool Updade(Borrow borrow, BorrowDetail borrowDetail)
        {
          
            using (var connection = _service.Connection)
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update Borrow record
                        string updateBorrowQuery = @"
                            UPDATE Borrow
                            SET IsHidden = @IsHidden, 
                                CustomerId = @CustomerId, 
                                LibrarianId = @LibrarianId, 
                                BorrowDate = @BorrowDate, 
                                BorrowCode = @BorrowCode, 
                                Depositamount = @Depositamount, 
                                Duedate = @Duedate, 
                                FineAmount = @FineAmount, 
                                Memo = @Memo
                            WHERE BorrowId = @BorrowId";

                        int rowsAffectedBorrow = connection.Execute(updateBorrowQuery, borrow, transaction: transaction);

                        if (rowsAffectedBorrow == 0)
                        {
                            throw new Exception("Borrow record update failed, no record found with the provided BorrowId.");
                        }

                        // Update BorrowDetail record
                        string updateDetailsQuery = @"
                            UPDATE BorrowDetail
                            SET Note = @Note, 
                                IsReturn = @IsReturn, 
                                ReturnDate = @ReturnDate
                            WHERE BorrowId = @BorrowId AND BookId = @BookId";

                        int rowsAffectedDetail = connection.Execute(updateDetailsQuery, borrowDetail, transaction: transaction);

                        if (rowsAffectedDetail == 0)
                        {
                            throw new Exception("BorrowDetail record update failed, no record found with the provided BorrowId and BookId.");
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        
                    }
                }
            }
            return true;

        }
    }
}
