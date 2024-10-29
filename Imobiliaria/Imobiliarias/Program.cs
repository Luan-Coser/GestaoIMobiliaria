using DAO;
using DAO.Repositorios.EF.ModuloCliente;
using DAO.Repositorios.EF.ModuloCorretor;
using DAO.Repositorios.EF.ModuloImovel;
using Imobiliaria.Dominio.ModuloCliente;
using Imobiliaria.Dominio.ModuloCorretor;
using Imobiliaria.Dominio.ModuloImovel;
using Imobiliaria.Dominio.ModuloLogin;
using Imobiliarias;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Academia.Programador.Bk.Gestao.Imobiliaria.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//SEED


			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(
				CookieAuthenticationDefaults.AuthenticationScheme
				).AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Login/Denied/";
                    options.LoginPath = "/Login/Login"; // Defenir página de login
                    options.LogoutPath = "/Login/Logout"; // Defenir página logout
                });

            //IOC
            //Injeção de dependencia
            builder.Services.AddTransient<ImobiliariaDbContext>();
			builder.Services.Configure<ConnectionStrings>(
			builder.Configuration.GetSection("ConnectionStrings"));

			builder.Services.AddTransient<IServiceCliente,ServiceCliente>();
			builder.Services.AddTransient<IClienteRepositorio,ClienteRepositorio>();;

			builder.Services.AddTransient<IServiceCorretor,ServiceCorretor>();
			builder.Services.AddTransient<ICorretorRepositorio,CorretorRepositorio>();

			builder.Services.AddTransient<IServiceImovel, ServiceImovel>();
			builder.Services.AddTransient<IImovelRepositorio, ImovelRepositorio>();

			builder.Services.AddTransient<IServiceLogin, ServiceLogin>();

			var app = builder.Build();


			var db = app.Services.GetService<ImobiliariaDbContext>();
			//db.Seed();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();
			app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Clientes}/{action=Index}/{id?}");
			app.Run();
		}
	}
}