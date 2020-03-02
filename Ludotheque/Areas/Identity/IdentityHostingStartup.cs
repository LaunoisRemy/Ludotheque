using System;
using Ludotheque.Areas.Identity.Data;
using Ludotheque.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Ludotheque.Areas.Identity.IdentityHostingStartup))]
namespace Ludotheque.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LudothequeAccountContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LudothequeAccountContextConnection")));

                services.AddDefaultIdentity<LudothequeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LudothequeAccountContext>();
            });
        }
    }
}