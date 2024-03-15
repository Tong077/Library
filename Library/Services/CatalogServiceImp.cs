using Dapper;
using Library.Data;
using Library.Models;

namespace Library.Services
{
    public class CatalogServiceImp : ICatalogService
    {
        private readonly DapperDbConnext _service;
        public CatalogServiceImp(DapperDbConnext service)
        {
            this._service = service;
        }
        public bool Create(Catalog catalog)
        {
            var sql = "INSERT INTO Catalog (IsHidden,CatalogCode,CatalogName,Isbn,AuthorName,PubliSher,PublishYear,Publisheditin) Values(@IsHidden,@CatalogCode,@CatalogName,@Isbn,@AuthorName,@PubliSher,@PulishYear,@Publisheditin)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHiiden = catalog.IsHidden,
                CatalogCode = catalog.CatalogCode,
                CatalogName = catalog.CatalogName,
                Isbn = catalog.Isbn,
                AuthorName = catalog.AuthorName,
                PubliSher = catalog.PubliSher,
                PublishYear = catalog.PublishYear,
                Publisheditin = catalog.Publisheditin,
            });
            return roweEffect > 0;
        }

        public bool Delete(Catalog catalog)
        {
            var sql = "DELETE FROM Catalog WHERE CatalogId = @CatalogId";
            var roweEffect = _service.Connection.Execute(sql, new { catalog });
            return roweEffect > 0;
        }

        public Catalog Get(int CatalogId)
        {
            var sql = "SELECT * FROM Catalog WHERE CatalogId = @CatalogId";
            var catalog = _service.Connection.QueryFirstOrDefault<Catalog>(sql, new { CatalogId });
            return catalog;
        }

        public IEnumerable<Catalog> GetAll()
        {
            var sql = "SELECT * FROM Catalog";
            var catalogs = _service.Connection.Query<Catalog>(sql);
            return catalogs;
        }

        public bool Update(Catalog catalog)
        {
            var sql = "UPDATE Catalog SET IsHiiden=@IsHidden, CatalogCode=@CatalogCode,Isbn=@Isbn,AuthorName=@AuthorName,PubliSher=@Publisher,PublishYear=@PublishYear,Publisheditin=@Publisheditin Where CatalogId=@CatalogId";
            var roweEffect = _service.Connection.Execute(sql,  catalog );
            return roweEffect > 0;
        }
    }
}
