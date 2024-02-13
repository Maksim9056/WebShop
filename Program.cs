using System.Reflection.PortableExecutable;
using WebShop.Operations;

namespace WebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

     
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            Operations.Operations work = new Operations.Operations();
            work.Start();

            work.Add();

            app.Run(async (context) =>
            {
                var stringBuilder = new System.Text.StringBuilder("<table>");

                if (context.Response.StatusCode == 404)
                {
                    await context.Response.WriteAsync("Resource Not Found");

                }

                 var DataBook =  work.SelectBook();
                foreach (var book in DataBook)
                {
                    stringBuilder.Append($"<tr><td>{book.Id}</td><td>{book.Name}</td></tr>");

                }
                stringBuilder.Append("</table>");
                await context.Response.WriteAsync(stringBuilder.ToString());
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
