using Library.Data;
using Library.Models;

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
            throw new NotImplementedException();
        }

        public bool Delete(Borrow borrow)
        {
            throw new NotImplementedException();
        }

        public Borrow Get(int BorrowId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Borrow> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Updade(Borrow borrow)
        {
            throw new NotImplementedException();
        }
    }
}
