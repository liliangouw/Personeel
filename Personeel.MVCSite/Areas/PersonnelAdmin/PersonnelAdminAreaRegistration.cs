using System.Web.Mvc;

namespace Personeel.MVCSite.Areas.PersonnelAdmin
{
    public class PersonnelAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PersonnelAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PersonnelAdmin_default",
                "PersonnelAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}