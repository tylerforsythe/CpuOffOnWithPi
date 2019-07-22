using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(CpuOffOnWithPi.WebAPI.WebOwinStartup))]

namespace CpuOffOnWithPi.WebAPI
{
    public class WebOwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = RouteParameter.Optional }
            );
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(config);

            var physicalFileSystem = new PhysicalFileSystem(ConfigurationManager.AppSettings["OwinHostWebsitePath"]);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[]
            {
                "index.html"
            };

            app.UseFileServer(options);
        }
    }
}