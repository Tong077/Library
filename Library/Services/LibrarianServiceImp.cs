using Dapper;
using Library.Data;
using Library.Models;

namespace Library.Services
{
    public class LibrarianServiceImp : ILibrarianService
    {
        private readonly DapperDbConnext _service;
        public LibrarianServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }
        public bool Create(Librarian librarian)
        {
            var sql = "INSERT INTO Librarain (IsHiDden,LibrarainCode,LibrarainName,Sex,Dob,Pob,Phone) Values(@IsHiDden,@LibrarainCode,@LibrarainName,@Sex,@Dob,@Pob,@Phone)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHiDden = librarian.IsHiDden,
                LibrarianCode = librarian.LibrarainCode,
                LibrarianName = librarian.LibrarainName,
                Sex = librarian.Sex,
                Dob = librarian.Dob,
                Pob = librarian.Pob,
                Phone = librarian.Phone,
            });
            return roweEffect > 0;
        }

        public bool Delete(int librarianId)
        {
            var sql = "DELETE FROM Librarain WHERE LibrarianId=@LibrarianId";
            var roweEffect = _service.Connection.Execute(sql, new { @LibrarianId = librarianId });
            return roweEffect > 0;
        }

        public Librarian Get(int LibrarianId)
        {
            var sql = "SELECT * FROM Librarain Where LibrarainId=@LibrarainId";
            var librarain = _service.Connection.QueryFirstOrDefault<Librarian>(sql, new {LibrarianId});
            return librarain;
        }

        public IEnumerable<Librarian> GetAll()
        {
            var sql = "SELECT * FROM Librarian";
            var librarains = _service.Connection.Query<Librarian>(sql);
            return librarains;
        }

        public bool Update(Librarian librarian)
        {
            var sql = "UPDATE Librarain SET IsHiDden=@IsHiDden,LibrarainCode=@LibrarainCode,LibrarainName=@LibrarainName,Sex=@Sex,Dob=@Dob,Pob=@Pob,Phone=@Phone";
            var roweEffect = _service.Connection.Execute(sql, librarian);
            return roweEffect > 0;
        }
    }
}
