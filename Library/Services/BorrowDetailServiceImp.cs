using Dapper;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Library.Services
{
    public class BorrowDetailServiceImp : IBorrowDetailService
    {
        private readonly DapperDbConnext _service;
        public BorrowDetailServiceImp(DapperDbConnext service)
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
            using (var connection = _service.Connection)
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"DELETE FROM Borrow WHERE BorrowId = @BorrowId;
                                        DELETE FROM BorrowDetail WHERE BorrowDetailId = @BorrowDetailId";
                        var parameters = new { BorrowId = BorrowId, BorrowDetailId = BorrowDetailId };

                        var rowsAffected = connection.Execute(query, parameters, transaction);

                        transaction.Commit(); // Commit the transaction if all queries succeed
                        return rowsAffected > 0;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback the transaction if an exception occurs
                        throw; // Optionally handle or log the exception
                    }
                }
            }
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


        public IEnumerable<BorrowDetail> GetAll()
        {
            var sql = @"
                SELECT 
                        bd.BorrowDetailId, 
                        bd.BorrowId, 
                        bd.BookId, 
                        bd.Note, 
                        bd.IsReturn, 
                        bd.ReturnDate,
                        b.BookId, 
                        b.BookCode, 
                        b.BookDescription, 
                        b.CatalogId,
                        br.BorrowId, 
                        br.BorrowCode, 
                        br.BorrowDate, 
                        br.Duedate, 
                        br.Depositamount, 
                        br.FineAmount, 
                        br.IsHidden, 
                        br.Memo, 
                        cu.CustomerId, 
                        cu.CustomerName,
                        li.LibrarianId, 
                        li.LibrarianName,
                        c.CatalogId, 
                        c.CatalogName
                    FROM 
                        BorrowDetail bd
                    INNER JOIN 
                        Book b ON bd.BookId = b.BookId
                    INNER JOIN 
                        Borrow br ON bd.BorrowId = br.BorrowId
                    INNER JOIN 
                        Customer cu ON br.CustomerId = cu.CustomerId
                    INNER JOIN 
                        Librarian li ON br.LibrarianId = li.LibrarianId
                    INNER JOIN 
                        Catalog c ON b.CatalogId = c.CatalogId;
                    ";

            using (var db = _service.Connection)
            {
                var borrowDetails = db.Query<BorrowDetail, Book, Borrow, Customer, Librarian, Catalog, BorrowDetail>(
                    sql,
                    (borrowDetail, book, borrow, customer, librarian, catalog) =>
                    {
                        book.Catalog = catalog;
                        borrow.Customer = customer;
                        borrow.Librarian = librarian;
                        borrowDetail.Book = book;
                        borrowDetail.Borrow = borrow;
                        return borrowDetail;
                    },
                    splitOn: "BookId,BorrowId,CustomerId,LibrarianId,CatalogId");

                return borrowDetails.ToList();
            }
        }

       
       

        public BorrowDetail GetById(int BorrowDetailId, bool IsReturn)
        {
            var sql = "Select BorrowDetailId, IsReturn From BorrowDetail Where BorrowDetailId = @BorrowDetailId";
            var borrow = _service.Connection.QueryFirstOrDefault<BorrowDetail>(sql, new { BorrowDetailId });
            return borrow;
        }

        public bool IsBorrowedAndNotReturned(int BookId)
        {
            using (var connection = _service.Connection)
            {
                var sql = "SELECT COUNT(*) FROM BorrowDetail WHERE BookId = @BookId AND IsReturn = 0";
                var count = connection.QueryFirstOrDefault<int>(sql, new { BookId = BookId });
                return count > 0;
            }
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
                                DepositAmount = @DepositAmount, 
                                DueDate = @DueDate, 
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
                                ReturnDate = @ReturnDate,
                                BookId = @BookId,
                                BorrowId = @BorrowId
                            WHERE BorrowDetailId = @BorrowDetailId";

                        int rowsAffectedDetail = connection.Execute(updateDetailsQuery, borrowDetail, transaction: transaction);

                        if (rowsAffectedDetail == 0)
                        {
                            throw new Exception("BorrowDetail record update failed, no record found with the provided BorrowId and BookId.");
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


    }
}


