using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Models.DTO
{
    public class OverviewReturnDTO
    {
        public int TotalUsers { get; set; }
        public int TotalContributions { get; set; }
        public int TotalArticles { get; set; }
        public int PendingContributions { get; set; }
        public int ApprovedContributions { get; set; }
        public int DeactivationRequest { get; set; }
        public double ContributionsIncrease { get; set; }
        public double PendingContributionsIncrease { get; set; }
        public double ApprovedContributionsIncrease { get; set; }
        public double ArticleIncrease { get; set; }
        public double UserIncrease { get; set; }
    }
}
