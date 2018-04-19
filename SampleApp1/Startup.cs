using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSUI.Common.Data.Utils;
using SampleApp1.Entities;

namespace SampleApp1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //init NHIbernate
            InitNHib();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //middleware to add NHibernate session to HttpContext
            app.Use(async (context, next) =>
            {

                //open session
                var session = NHibernateConfiguration.SessionFactory.OpenSession();
                context.Items["RSUINHibSession"] = session;

                await next.Invoke();

                //close session here
                var openedsession = context.Items["RSUINHibSession"] as NHibernate.ISession;
                if (openedsession != null && openedsession.IsOpen)
                    openedsession.Close();
            });

            app.UseMvc();

        }


        public void InitNHib()
        {
            //configure logging
            //if (!log.Logger.Repository.Configured)
            //{
            //    XmlConfigurator.Configure();
            //}

            string configFile = "hibernate.cfg.xml";
            var connString = Configuration.GetConnectionString("SampleAppDB");


            var conn1 = new NHibConfigOptions()
            {
                ConnectionName = "DEV3",
                ConnectionString = connString,
                FluentAssemblies = new List<Assembly> { Assembly.GetAssembly(typeof(Department)) }
            };

            NHibernateConfiguration.Initialize(new List<NHibConfigOptions>() { conn1 });


        }
    }
}
