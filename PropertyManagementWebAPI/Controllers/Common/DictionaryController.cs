



namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Common
{
    /// <summary>
    /// 字典相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Common")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebDicMentMatching.DicMentadd))]
        public async Task<ActionResult> AddDictionaryAsync(AddEditDictionaryModel model, CancellationToken cancellationToken = default)
        {
                                                         
            if (string.IsNullOrEmpty(model.Key))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "键不可为空"
                });
            }
            if (model.IsCreateType.HasValue && !model.IsCreateType.Value && string.IsNullOrEmpty(model.Value))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "值不可为空"
                });
            }
            if (model.IsCreateType.HasValue && model.IsCreateType.Value)
            {
                model.Value = "CreateType";
            }
            if (!model.Type.HasValue)
            {
                model.Type = 0;
            }
           
            //if (!model.Type.HasValue)
            //{
            //    return Ok(new
            //    {
            //        Status = "Failed",
            //        Msg = "类型不可为空"
            //    });
            //}
            var tuple = await _dictionaryService.AddDictionaryAsync(model, cancellationToken);
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
        [CheckPermission(nameof(WebDicMentMatching.DicMentedit))]
        public async Task<ActionResult> EditDictionaryAsync(AddEditDictionaryModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }
            var tuple = await _dictionaryService.EditDictionaryAsync(model, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 根据条件查询字典列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryAsync(DictionaryQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            var tuple = await _dictionaryService.GetDictionaryAsync(query, cancellationToken);
            var typeTuple = await _dictionaryService.GetDictionaryTypeAsync(new DictionaryQueryModel { Type = 0, Value= "CreateType" }, cancellationToken) ;
            var list = tuple.List.Select(x=>new DictionaryDTO 
            {
                CreateTime=x.CreateTime,
                Description = x.Description,
                Id = x.Id,
                Key = x.Key,
                Type = x.Type,
                Value = x.Value,
                TypeName= typeTuple.List.FirstOrDefault(i=>i.Id==x.Type)?.Key?? "无类型"
            }).ToList();
            return Ok(new
            {
                Status = "Ok",
                Data = list,
                TotalCount = tuple.Total
            });
        }
        /// <summary>
        /// 根据类型查询字典列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryByTypeNameAsync(string typeName, CancellationToken cancellationToken = default)
        {
            var tuple = await _dictionaryService.GetDictionaryByTypeNameAsync(typeName, cancellationToken);
            var list = tuple.Item1.Select(x => new DictionaryDTO
            {
                CreateTime = x.CreateTime,
                Description = x.Description,
                Id = x.Id,
                Key = x.Key,
                Type = x.Type,
                Value = x.Value
            }).ToList();
            return Ok(new
            {
                Status = "Ok",
                Data = list
            });
        }
        /// <summary>
        /// 根据条件查询字典类型列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryTypeAsync(DictionaryQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            query.Type = 0;
            query.Value = "CreateType";
            var tuple = await _dictionaryService.GetDictionaryTypeAsync(query, cancellationToken);
            tuple.List.Insert(0,new DictionaryDTO {Id=0,Key="无类型",Type=0});
            return Ok(new
            {
                Status = "Ok",
                Data = tuple.List,
                TotalCount = tuple.Total
            });
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryByIdAsync(GetByIdModel model)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _dictionaryService.GetDictionaryByIdAsync(model.Id);
            return Ok(new
            {
                Status = "Ok",
                Data = obj
            });
        }
        /// <summary>
        /// 根据类型的key获取该类型下的字典数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "key不能为空"
                });
            }

            var obj = await _dictionaryService.GetDictionaryByPropAsync(new DictionaryQueryModel {Key=key}, cancellationToken);
            if (obj==null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = $"key={key}的字典不存在"
                });
            }
            var list= await _dictionaryService.GetDictionaryAsync(new DictionaryQueryModel { Type=(int)obj.Id}, cancellationToken);
            return Ok(new
            {
                Status = "Ok",
                Data = list
            });
        }
        /// <summary>
        /// 根据字段获取一条数据
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(0, "返回数据属性注释", typeof(DictionaryDTO))]
        public async Task<ActionResult> GetDictionaryByPropsAsync(DictionaryQueryModel query, CancellationToken cancellationToken = default)
        {
            var obj = await _dictionaryService.GetDictionaryByPropAsync(query, cancellationToken);
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
        [CheckPermission(nameof(WebDicMentMatching.DicMentdel))]
        public async Task<ActionResult> RemoveDictionaryAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }

            var tuple = await _dictionaryService.RemoveDictionaryAsync(model.Id, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
    }
}
