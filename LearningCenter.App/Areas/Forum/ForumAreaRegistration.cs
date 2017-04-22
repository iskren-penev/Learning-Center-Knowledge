namespace LearningCenter.App.Areas.Forum
{
    using System.Web.Mvc;

    public class ForumAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Forum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Forum_default",
                "{controller}/{action}/{id}",
                new { action = "All", id = UrlParameter.Optional }
            );
        }
    }
}