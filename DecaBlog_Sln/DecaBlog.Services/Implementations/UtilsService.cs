using AutoMapper;
using DecaBlog.Data.Repositories.Interfaces;
using DecaBlog.Models;
using DecaBlog.Models.DTO;
using DecaBlog.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Services.Implementations
{
    public class UtilsService : IUtilsService
    {
        private readonly IStackRepository _stackRepository;
        private readonly ISquadRepository _squadRepository;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleTopicRepository _articleTopicRepository;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userMgr;
        public UtilsService(IStackRepository stackRepository, ISquadRepository squadRepository, IMapper mapper, IArticleRepository articleRepository, IArticleTopicRepository articleTopicRepository, IUserService userService, UserManager<User> userMgr)
        {
            _userMgr = userMgr;
            _userService = userService;
            _articleTopicRepository = articleTopicRepository;
            _articleRepository = articleRepository;
            _stackRepository = stackRepository;
            _squadRepository = squadRepository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<StackMinInfoToReturnDto>, IEnumerable<SquadMinInfoToReturnDto>)> GetAllSquadsAndStack()
        {
            var allSquads = await _squadRepository.GetAllSquads();
            if (allSquads == null) return (null, null);
            var SquadDataToReturn = _mapper.Map<IEnumerable<SquadMinInfoToReturnDto>>(allSquads);
            var allStacks = await _stackRepository.GetAllStacks();
            if (allStacks == null) return (null, null);
            var StackDataToReturn = _mapper.Map<IEnumerable<StackMinInfoToReturnDto>>(allStacks);
            return (StackDataToReturn, SquadDataToReturn);
        }

        public async Task<OverviewReturnDTO> GetOverviewData()
        {
            var articles = _articleTopicRepository.GetArticleTopics();
            var pendingContributions = _articleRepository.GetPendingArticlesAsync();
            var contributions = await _articleRepository.GetArticlesAsync();
            var approvedArticles = _articleRepository.GetPublishedArticlesAsync();

            var prevMonthsArticles = articles.Where(x =>DateTime.Now.Month==1 ? x.DateCreated.Month == 12 && x.DateCreated.Year == DateTime.Now.Year-1 : x.DateCreated.Month == DateTime.Now.Month -1 && x.DateCreated.Year == DateTime.Now.Year).Count();
            var presentMonthArticles = articles.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count();
            double articleIncreasePerMonth = prevMonthsArticles != 0 ? ((double)presentMonthArticles / prevMonthsArticles) * 100 : presentMonthArticles != 0 ? ((double)presentMonthArticles / presentMonthArticles) * 100 : 0;

            var prevMonthsContributions = contributions.Where(x => DateTime.Now.Month == 1 ? x.DateCreated.Month == 12 && x.DateCreated.Year == DateTime.Now.Year - 1 : x.DateCreated.Month == DateTime.Now.Month -1 && x.DateCreated.Year == DateTime.Now.Year).Count();
            var currentMonthContributions = contributions.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count();
            double contributionIncreasePerMonth = prevMonthsContributions != 0 ? ((double)currentMonthContributions / prevMonthsContributions) * 100 : currentMonthContributions != 0 ? ((double)currentMonthContributions / currentMonthContributions) * 100 : 0;

            var prevMonthsPendingContributions = pendingContributions.Where(x => DateTime.Now.Month == 1 ? x.DateCreated.Month == 12 && x.DateCreated.Year == DateTime.Now.Year - 1 : x.DateCreated.Month == DateTime.Now.Month -1 && x.DateCreated.Year == DateTime.Now.Year).Count();
            var currentMonthsPendingContributions = pendingContributions.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count();
            double pendingContributionIncrease = prevMonthsPendingContributions != 0 ? ((double)currentMonthsPendingContributions / prevMonthsPendingContributions) * 100 : currentMonthsPendingContributions != 0 ? ((double)currentMonthsPendingContributions / currentMonthsPendingContributions) * 100 : 0;

            var prevMonthsApprovedContributions = approvedArticles.Where(x => DateTime.Now.Month == 1 ? x.DateCreated.Month == 12 && x.DateCreated.Year == DateTime.Now.Year - 1 : x.DateCreated.Month == DateTime.Now.Month -1 && x.DateCreated.Year == DateTime.Now.Year).Count();
            var currentMonthApprovedContributions = approvedArticles.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count();
            double approvedContributionIncrease = prevMonthsApprovedContributions != 0 ? ((double)currentMonthApprovedContributions / prevMonthsApprovedContributions) * 100 : currentMonthApprovedContributions != 0 ? ((double)currentMonthApprovedContributions / currentMonthApprovedContributions) * 100 : 0;

            var prevMonthsUsers = _userMgr.Users.Where(x => DateTime.Now.Month == 1 ? x.DateCreated.Month == 12 && x.DateCreated.Year == DateTime.Now.Year - 1 : x.DateCreated.Month == DateTime.Now.Month -1 && x.DateCreated.Year == DateTime.Now.Year).Count();
            var currentMonthUsers = _userMgr.Users.Where(x => x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).Count();
            double userIncrease = prevMonthsUsers != 0 ? ((double)currentMonthUsers / prevMonthsUsers) * 100 : currentMonthUsers != 0 ? ((double)currentMonthUsers / currentMonthUsers) * 100 : 0;

            var result = new OverviewReturnDTO();
            result.TotalContributions = contributions.Count();
            result.TotalArticles = articles.Count();
            result.PendingContributions = pendingContributions.Count();
            result.ApprovedContributions = approvedArticles.Count();
            result.TotalUsers = _userMgr.Users.Count();
            result.ApprovedContributionsIncrease = approvedContributionIncrease;
            result.PendingContributionsIncrease = pendingContributionIncrease;
            result.ContributionsIncrease = contributionIncreasePerMonth;
            result.ArticleIncrease = articleIncreasePerMonth;
            result.UserIncrease = userIncrease;
            return result;
        }
    }
}
