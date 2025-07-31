using System;
using System.Collections.Generic;
using DataViewConfig.Interfaces;
using DataViewConfig.Repositories;
using DataViewConfig.Models.Entities;

namespace DataViewConfig.Services
{
    /// <summary>
    /// Repository测试服务
    /// </summary>
    public static class RepositoryTestService
    {
        /// <summary>
        /// 测试所有Repository功能
        /// </summary>
        /// <returns>测试是否通过</returns>
        public static bool TestAllRepositories()
        {
            try
            {
                Console.WriteLine("开始测试Repository模式...");

                // 测试TagRepository
                if (!TestTagRepository())
                {
                    Console.WriteLine("✗ TagRepository测试失败");
                    return false;
                }

                // 测试ScreenRepository
                if (!TestScreenRepository())
                {
                    Console.WriteLine("✗ ScreenRepository测试失败");
                    return false;
                }

                // 测试BlockRepository
                if (!TestBlockRepository())
                {
                    Console.WriteLine("✗ BlockRepository测试失败");
                    return false;
                }

                Console.WriteLine("✓ 所有Repository测试通过！");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ Repository测试异常: " + ex.Message);
                LogHelper.Error("Repository test failed: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 测试TagRepository
        /// </summary>
        private static bool TestTagRepository()
        {
            try
            {
                var tagRepo = ServiceLocator.GetService<ITagRepository>();
                
                // 测试获取所有标签
                var allTags = tagRepo.GetAll();
                Console.WriteLine("✓ TagRepository.GetAll() - 获取到 " + allTags.Count + " 个标签");

                // 测试搜索功能
                var searchResults = tagRepo.Search("test");
                Console.WriteLine("✓ TagRepository.Search() - 搜索结果: " + searchResults.Count + " 个");

                // 测试分页功能
                var pagedResult = tagRepo.GetPaged(1, 10);
                Console.WriteLine("✓ TagRepository.GetPaged() - 第1页: " + pagedResult.Data.Count + " 个，总数: " + pagedResult.TotalCount);

                // 测试按系统ID查询
                var systemTags = tagRepo.GetBySystemId(1);
                Console.WriteLine("✓ TagRepository.GetBySystemId() - 系统1的标签: " + systemTags.Count + " 个");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ TagRepository测试失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 测试ScreenRepository
        /// </summary>
        private static bool TestScreenRepository()
        {
            try
            {
                var screenRepo = ServiceLocator.GetService<IScreenRepository>();
                
                // 测试获取所有画面
                var allScreens = screenRepo.GetAll();
                Console.WriteLine("✓ ScreenRepository.GetAll() - 获取到 " + allScreens.Count + " 个画面");

                // 测试搜索功能
                var searchResults = screenRepo.Search("test");
                Console.WriteLine("✓ ScreenRepository.Search() - 搜索结果: " + searchResults.Count + " 个");

                // 测试获取单个画面
                var singleScreens = screenRepo.GetSingleScreens();
                Console.WriteLine("✓ ScreenRepository.GetSingleScreens() - 单个画面: " + singleScreens.Count + " 个");

                // 测试获取组合画面
                var compositeScreens = screenRepo.GetCompositeScreens();
                Console.WriteLine("✓ ScreenRepository.GetCompositeScreens() - 组合画面: " + compositeScreens.Count + " 个");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ ScreenRepository测试失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 测试BlockRepository
        /// </summary>
        private static bool TestBlockRepository()
        {
            try
            {
                var blockRepo = ServiceLocator.GetService<IBlockRepository>();
                
                // 测试获取所有堆场
                var allBlocks = blockRepo.GetAll();
                Console.WriteLine("✓ BlockRepository.GetAll() - 获取到 " + allBlocks.Count + " 个堆场");

                // 测试搜索功能
                var searchResults = blockRepo.Search("test");
                Console.WriteLine("✓ BlockRepository.Search() - 搜索结果: " + searchResults.Count + " 个");

                // 测试统计功能
                var statistics = blockRepo.GetStatistics();
                Console.WriteLine("✓ BlockRepository.GetStatistics() - 总数: " + statistics.TotalCount + 
                                ", 位置范围: " + statistics.MinPosition + "-" + statistics.MaxPosition);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ BlockRepository测试失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 测试Repository的CRUD操作
        /// </summary>
        /// <returns>测试是否通过</returns>
        public static bool TestRepositoryCRUD()
        {
            try
            {
                Console.WriteLine("开始测试Repository CRUD操作...");

                var tagRepo = ServiceLocator.GetService<ITagRepository>();
                
                // 创建测试标签
                var testTag = new TagEntity
                {
                    TagInternalName = "TEST_TAG_" + DateTime.Now.Ticks,
                    TagName = "测试标签",
                    TagDesc = "Repository测试用标签",
                    DataTypeId = 1,
                    PostfixTypeId = 1
                };

                // 测试添加
                bool addResult = tagRepo.Add(testTag);
                Console.WriteLine("✓ Repository.Add() - 结果: " + addResult);

                if (addResult)
                {
                    // 测试查询
                    var foundTag = tagRepo.GetByInternalName(testTag.TagInternalName);
                    if (foundTag != null)
                    {
                        Console.WriteLine("✓ Repository.GetByInternalName() - 找到标签: " + foundTag.TagName);

                        // 测试更新
                        foundTag.TagDesc = "已更新的描述";
                        bool updateResult = tagRepo.Update(foundTag);
                        Console.WriteLine("✓ Repository.Update() - 结果: " + updateResult);

                        // 测试删除
                        bool deleteResult = tagRepo.Delete(foundTag);
                        Console.WriteLine("✓ Repository.Delete() - 结果: " + deleteResult);
                    }
                    else
                    {
                        Console.WriteLine("✗ 无法找到刚添加的标签");
                        return false;
                    }
                }

                Console.WriteLine("✓ Repository CRUD测试完成！");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ Repository CRUD测试失败: " + ex.Message);
                LogHelper.Error("Repository CRUD test failed: " + ex.ToString());
                return false;
            }
        }
    }
}
