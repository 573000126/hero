﻿using Surging.Core.CPlatform.Ioc;
using Surging.Core.CPlatform.Runtime.Server.Implementation.ServiceDiscovery.Attributes;
using Surging.Core.Domain.PagedAndSorted;
using Surging.Core.System.Intercept;
using Surging.Hero.BasicData.IApplication.Wordbook.Dtos;
using Surging.Hero.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surging.Hero.BasicData.IApplication.Wordbook
{
    [ServiceBundle(HeroConstants.RouteTemplet)]
    public interface IWordbookAppService : IServiceKey
    {
        /// <summary>
        /// 创建字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [ServiceRoute("create")]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "创建字典")]
        Task<string> Create(CreateWordbookInput input);

        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut(true)]
        [ServiceRoute("update")]
        [InterceptMethod(CachingMethod.Remove, CorrespondingKeys = new string[] { CacheKeyConstant.GetWordBookById }, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "更新字典")]
        Task<string> Update(UpdateWordbookInput input);

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceRoute("delete/{id}")]
        [HttpDelete(true)]
        [InterceptMethod(CachingMethod.Remove, CorrespondingKeys = new string[] { CacheKeyConstant.GetWordBookById }, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "删除字典")]
        Task<string> Delete([CacheKey(1)]long id);

        /// <summary>
        /// 查询字典
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ServiceRoute("query")]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "查询字典", AllowPermission = true)]
        Task<IPagedResult<GetWordbookOutput>> Query(QueryWordbookInput query);

        /// <summary>
        /// 获取字典信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceRoute("get/{id}")]
        [HttpGet(true)]
        [InterceptMethod(CachingMethod.Get, Key = CacheKeyConstant.GetWordBookById, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "获取字典信息", AllowPermission = true)]
        Task<GetWordbookOutput> Get([CacheKey(1)]long id);

        /// <summary>
        /// 获取字典项
        /// </summary>
        /// <param name="wordbookId"></param>
        /// <returns></returns>
        [ServiceRoute("items/id/{wordbookId}")]
        [HttpGet(true)]
        [InterceptMethod(CachingMethod.Get, Key = CacheKeyConstant.GetWordBookItemsById, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "获取字典项")]
        Task<IEnumerable<GetWordbookItemOutput>> GetWordbookItems([CacheKey(1)]long wordbookId);

        /// <summary>
        /// 新增字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [ServiceRoute("items/create")]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "新增字典项")]
        Task<string> CreateWordbookItem(CreateWordbookItemInput input);

        /// <summary>
        /// 更新字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut(true)]
        [ServiceRoute("items/update")]
        [InterceptMethod(CachingMethod.Remove, CorrespondingKeys = new string[] { CacheKeyConstant.GetWordBookItemsById,CacheKeyConstant.GetWordBookItemById, CacheKeyConstant.RemoveGetWordBookItem, CacheKeyConstant.RemoveGetWordBookItems }, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "更新字典项")]
        Task<string> UpdateWordbookItem(UpdateWordbookItemInput input);

        /// <summary>
        /// 删除字典项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceRoute("items/delete/{id}")]
        [HttpDelete(true)]
        [InterceptMethod(CachingMethod.Remove, CorrespondingKeys = new string[] { CacheKeyConstant.GetWordBookItemsById, CacheKeyConstant.GetWordBookItemById, CacheKeyConstant.RemoveGetWordBookItem,CacheKeyConstant.RemoveGetWordBookItems }, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "删除字典项")]
        Task<string> DeleteWordbookItem([CacheKey(1)]long id);

        /// <summary>
        /// 获取字典项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ServiceRoute("items/get/{id}")]
        [HttpGet(true)]
        [InterceptMethod(CachingMethod.Get, Key = CacheKeyConstant.GetWordBookItemById, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "获取字典项目", AllowPermission = true)]
        Task<GetWordbookItemOutput> GetWordbookItem([CacheKey(1)]long id);

        /// <summary>
        /// 检查是否存在某个字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost(true)]
        [ServiceRoute("check")]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "检查是否存在某个字典项", DisableNetwork = true)]
        Task<bool> Check(CheckWordbookInput input);

        [ServiceRoute("items/code/{code}")]
        [HttpGet(true)]
        [InterceptMethod(CachingMethod.Get, Key = CacheKeyConstant.GetWordBookItemsByCode, Mode = Core.Caching.CacheTargetType.Redis)]
        [Service(Director = Developers.Liuhll, Date = "2020-07-04", Name = "根据字典标识获取字典项", AllowPermission = true)]
        Task<IEnumerable<GetWordbookItemOutput>> GetWordbookItemByCode([CacheKey(1)]string code);
    }
}
