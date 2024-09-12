using DAO;
using Imobiliarias;
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
			//IOC
			//Injeção de dependencia
			builder.Services.AddTransient<ImobiliariaDbContext>();
			builder.Services.Configure<ConnectionStrings>(
				builder.Configuration.GetSection("ConnectionStrings"));

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
			app.UseAuthorization();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Clientes}/{action=Index}/{id?}");
			app.Run();
		}
	}
}