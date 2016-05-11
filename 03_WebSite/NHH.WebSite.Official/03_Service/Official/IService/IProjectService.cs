using NHH.Models.Official;

namespace NHH.Service.Official.IService
{
    public interface IProjectService
    {
        /// <summary>
        /// 获取项目详细页顶部banner
        /// </summary>
        /// <returns></returns>
        string GetProjectBanner(int projectID);

        /// <summary>
        /// 获取项目详细页顶部banner数量
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        int GetProjectBannerCount(int projectID);

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        ProjectInfo GetProjectInfo(int projectID);

        /// <summary>
        /// 获取项目图片
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        string GetProjectPic(int projectID);

        /// <summary>
        /// 获取项目活动
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        string GetProjectCampaign(int projectID);

        /// <summary>
        /// 获取官网项目列表
        /// </summary>
        /// <returns></returns>
        string GetProjectList(int projectID);
    }
}
