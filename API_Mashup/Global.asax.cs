using System.Web.Http;

namespace API_Mashup
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //Called everytime the web-app is started
        protected void Application_Start()
        {
            //Always at top (Before any MVC route config)
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
