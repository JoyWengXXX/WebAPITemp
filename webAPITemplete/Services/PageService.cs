using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using System.Data;
using WebAPITemplete.DBContexts.Dapper;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Services.interfaces;

namespace WebAPITemplete.Services
{
    public class PageService
    {
        private readonly IDbConnection _dbConnection;

        public PageService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault)
        {
            _dbConnection = baseDapperDefault.CreateConnection();
        }

        /// <summary>
        /// 取回使用者系統選單
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public async Task<PageDTO> GetMenuPages(int RoleID)
        {
            var sql = @"WITH RecursivePage AS
                        (
	                        SELECT pg.[PageID], pg.[PageName], pg.[ParentPageID], 0 AS Level
	                        FROM [Page] pg
	                        WHERE [ParentPageID] = 0

	                        UNION ALL

	                        SELECT pg.[PageID], pg.[PageName], pg.[ParentPageID], rp.Level + 1
	                        FROM [Page] pg
	                        INNER JOIN RecursivePage rp ON pg.ParentPageID = rp.PageID
                        )
                        SELECT rp.[PageID], [PageName], [ParentPageID], [Level]
                        FROM RecursivePage rp
                        RIGHT JOIN [Permission] pm ON rp.PageID = pm.PageID
                        WHERE pm.RoleID = @RoleID
                        ORDER BY [Level];";
            return await _dbConnection.QuerySingleOrDefaultAsync<PageDTO>(sql, RoleID);
        }
    }
}
