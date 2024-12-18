

namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Identity
{
    /// <summary>
    /// web菜单相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Identity")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }     
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMentadd))]
        public async Task<ActionResult> AddMenuAsync(AddEditMenuModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.Path) && model.IsButton==IsButtonEnum.否)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "路由不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.Component) && model.IsButton == IsButtonEnum.否)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "组件不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "菜单或按钮名称不可为空"
                });              
            }
            model.MenuName = model.Name;
            if (string.IsNullOrEmpty(model.MenuCode) && model.Level != 1)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "菜单或按钮编码不可为空"
                });
            }         
            if (!model.IsButton.HasValue)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "是否是按钮不可为空"
                });
            }
            if (model.IsButton.Value==IsButtonEnum.是 && string.IsNullOrEmpty(model.ButtonName))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "按钮名称不能为空"
                });
            }
            var tuple = await _menuService.AddMenuAsync(model, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 != null ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMentedit))]
        public async Task<ActionResult> EditMenuAsync(AddEditMenuModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }
            var tuple = await _menuService.EditMenuAsync(model, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 查询所有菜单及按钮
        /// </summary>
        /// <param name="isFilter">1-过滤  2-不过滤</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(MenuTreeModel))]
        public async Task<ActionResult> GetAllMenusAndButtonAsync(int isFilter,CancellationToken cancellationToken = default)
        {
            var data = await _menuService.GetAllMenusAndButtonAsync(cancellationToken);
            if (isFilter==1)
            {
                string[] array = new string[] { "DepartmentMent", "RoleMent", "TypeSettings" , "DicMent", "SystemMent" , "OperationLog" };
                data = data.Where(x => x.Name != "系统管理" && 
                (string.IsNullOrEmpty(x.MenuCode)  ||               
                !array.Any(i=> !string.IsNullOrEmpty(x.MenuCode) && x.MenuCode.Contains(i))) 
                ).OrderBy(x => x.Id).ToList();
            }           
            List<MenuTreeModel> treeData = data.Select(x => new MenuTreeModel
            {
                Component = x.Component,
                Createtime = x.CreateTime,
                Description = x.Description,
                Icon = x.Icon,
                Id = x.Id,
                Name = x.Name,
                MenuName = x.MenuName,
                ParentId = x.ParentId.HasValue ? x.ParentId.Value : 0,
                Path = x.Path,
                MenuCode = x.MenuCode,
                ButtonName= x.ButtonName,
                Level = x.Level
                
            })
                .OrderBy(x => x.Id)
                .ToList();

            return Ok(new
            {
                Status = "Ok",
                Data = treeData == null ? null : CommonUtils.ToTreeData(null, treeData)
            });
        }
      
        /// <summary>
        /// 根据父级id查询菜单
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(MenuTreeModel))]
        public async Task<ActionResult> GetMenusByParentIdAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {         
            var data = await _menuService.GetMenusByParentIdAsync(model.Id, cancellationToken);          
            return Ok(new
            {
                Status = "Ok",
                Data = data
            });
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(MenuDTO))]
        public async Task<ActionResult> GetMenusAndButtonByIdAsync(GetByIdModel model)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _menuService.GetMenusAndButtonByIdAsync(model.Id);
            return Ok(new
            {
                Status = "Ok",
                Data = obj
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebSystemMentMatching.SystemMentdel))]
        public async Task<ActionResult> RemoveMenusAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }

            var tuple = await _menuService.RemoveMenusAsync(model.Id, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
    }
}
