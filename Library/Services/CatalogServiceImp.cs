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
            var sql = "INSERT INTO Catalog (IsHidden,CatalogCode,CatalogName,Isbn,AuthorName,PubliSher,PublishYear,PublisheDition) Values(@IsHidden,@CatalogCode,@CatalogName,@Isbn,@AuthorName,@PubliSher,@PublishYear,@PublisheDition)";
            var roweEffect = _service.Connection.Execute(sql, new
            {
                IsHidden = catalog.IsHidden,
                CatalogCode = catalog.CatalogCode,
                CatalogName = catalog.CatalogName,
                Isbn = catalog.Isbn,
                AuthorName = catalog.AuthorName,
                PubliSher = catalog.PubliSher,
                PublishYear = catalog.PublishYear,
                PublisheDition = catalog.PublisheDition,
            });
            return roweEffect > 0;
        }

        public bool Delete(int catalogId)
        {
            var sql = "DELETE FROM Catalog WHERE CatalogId = @CatalogId";
            var roweEffect = _service.Connection.Execute(sql, new { @CatalogId = catalogId });
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
            var sql = "UPDATE Catalog SET IsHidden=@IsHidden,CatalogName=@CatalogName,CatalogCode=@CatalogCode,Isbn=@Isbn,AuthorName=@AuthorName,PubliSher=@PubliSher,PublishYear=@PublishYear,PublisheDition=@PublisheDition Where CatalogId=@CatalogId";
            var roweEffect = _service.Connection.Execute(sql,  catalog );
            return roweEffect > 0;
        }
    }
}
