var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TheRideYourRent.Models.TheRideYourRentContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeController1}/{action=Index}/{id?}");

app.Run();

/*
 Code Attribution:
https://youtu.be/92VOAYiVlxg from HHV Technology
https://youtube.com/@hhvtechnology7034

https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/aspnet-mvc-controllers-overview-cs From Microsoft, StephenWalther
https://github.com/StephenWalther

https://stackoverflow.com/questions/66205651/how-to-delete-record-having-foreign-key-constraint from StackOverflow, sudip chand
https://stackoverflow.com/users/15145229/sudip-chand

https://stackoverflow.com/questions/41135761/how-to-implement-a-custom-error-page-in-asp-net-mvc from StackOverflow, Praddyumna Sangvikar
https://stackoverflow.com/users/7256292/praddyumna-sangvikar

https://youtu.be/2Cp8Ti_f9Gk from Youtube, Sameer Saini
https://youtube.com/@SameerSaini
 */
