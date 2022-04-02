using DecaBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Commons.Helpers
{
    public static class ArticlesChart
    {
        public static Task<Dictionary<string,int>> ArticlesBymonth(Dictionary<int,int> articles)
        {
            var result = new Dictionary<string, int> {
                {"jan",0},{"feb",0},{"mar",0},{"apr",0},{"may",0},{"jun",0},{"jul",0},{"aug",0},{"sep",0},{"oct",0},{"nov",0},{"dec",0}
            };
            foreach (var b in articles)
            {
                switch (b.Key)
                {
                    case 1:
                        result["jan"]=b.Value;
                        break;
                    case 2:
                        result["feb"] = b.Value;
                        break;
                    case 3:
                        result["mar"] = b.Value;
                        break;
                    case 4:
                        result["apr"] = b.Value;
                        break;
                    case 5:
                        result["may"] = b.Value;
                        break;
                    case 6:
                        result["jun"] = b.Value;
                        break;
                    case 7:
                        result["jul"] = b.Value;
                        break;
                    case 8:
                        result["aug"] = b.Value;
                        break;
                    case 9:
                        result["sep"] = b.Value;
                        break;
                    case 10:
                        result["oct"] = b.Value;
                        break;
                    case 11:
                        result["nov"] = b.Value;
                        break;
                    default:
                        result["dec"] = b.Value;
                        break;
                }
            }
            return Task.Run(() => result);
    }   }
}
