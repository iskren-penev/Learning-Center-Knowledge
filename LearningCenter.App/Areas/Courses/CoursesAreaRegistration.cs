namespace LearningCenter.App.Areas.Courses
{
    using System.Web.Mvc;

    public class CoursesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Courses";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Courses_default",
                "Courses/{controller}/{action}/{id}",
                new { action = "AllCourses", id = UrlParameter.Optional }
            );
        }
    }
}