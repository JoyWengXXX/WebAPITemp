using CommomLibrary.Dapper.Repository.interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using webAPITemp.DBContexts.Dapper;
using webAPITemp.Models.DTOs.DefaultDB;
using webAPITemp.Models.Mapper.interfaces;
using webAPITemp.Models.ViewModels;
using webAPITemp.Services.interfaces;

namespace webAPITemp.Services
{
    public class PageService : IPageService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IPageMapper _pageMapper;

        public PageService(IBaseDapper<ProjectDBContext_Default> baseDapperDefault, IPageMapper pageMapper)
        {
            _dbConnection = baseDapperDefault.CreateConnection();
            _pageMapper = pageMapper;
        }

        /// <summary>
        /// 取回使用者系統選單
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public async Task<List<MenuTreeViewModel>> GetMenuPages(int RoleID)
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

            var aa = await _dbConnection.QueryAsync<PageDTO>(sql, new { RoleID });

            var mapper = _pageMapper.ToMenuTreeViewModel().CreateMapper();
            List<MenuTreeViewModel> result = mapper.Map<List<MenuTreeViewModel>>(await _dbConnection.QueryAsync<PageDTO>(sql, new { RoleID }));
            List<MenuTreeViewModel> menuTree = new List<MenuTreeViewModel>();
            foreach (MenuTreeViewModel page in result)
            {
                if (page.ParentPageID == 0)
                {
                    // 如果是最上層的頁面，加入根節點列表
                    menuTree.Add(page);
                }
                else
                {
                    FindAndAddToParent(page, menuTree);
                }
            }
            return menuTree;
        }

        #region private methods

        /// <summary>
        /// 用疊代尋找父節點並將該頁面加入其 SubPages
        /// </summary>
        /// <param name="page"></param>
        /// <param name="nodes"></param>
        private void FindAndAddToParent(MenuTreeViewModel page, List<MenuTreeViewModel> nodes)
        {
            foreach (var node in nodes)
            {
                if (node.PageID == page.ParentPageID)
                {
                    if (node.SubPages == null)
                    {
                        node.SubPages = new List<MenuTreeViewModel>();
                    }
                    node.SubPages.Add(page);
                    return;
                }
                else if (node.SubPages != null)
                {
                    // 遞迴尋找父節點
                    FindAndAddToParent(page, node.SubPages);
                }
            }
        }
    }

    #endregion
}
