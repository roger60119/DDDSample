using Hangfire.Dashboard;

namespace DDDSample.Infrastructure.Common
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // 在這裡實作您的授權邏輯
            // 例如，檢查使用者是否已登入且具有特定角色
            // 為了示範，這裡我們簡單地允許所有請求
            // **警告：在正式環境中，您應該實作更嚴格的驗證！**
            // return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Admin");

            return true;
        }
    }
}