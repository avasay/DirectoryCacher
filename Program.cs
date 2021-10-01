using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddMemoryCache())
    .Build();

IMemoryCache cache = host.Services.GetRequiredService<IMemoryCache>();

string dir1 = Environment.CurrentDirectory + @"\Images\abstract\";
string dir2 = Environment.CurrentDirectory + @"\Images\biology\";

DirectoryCacherConsoleApp.DirectoryCacher dir1Cacher =  new DirectoryCacherConsoleApp.DirectoryCacher(cache, dir1);
DirectoryCacherConsoleApp.DirectoryCacher dir2Cacher = new DirectoryCacherConsoleApp.DirectoryCacher(cache, dir2);


// First Pass
Console.WriteLine(dir1Cacher.GetListCache());
Console.WriteLine(dir2Cacher.GetListCache());

// Second Pass
Console.WriteLine(dir1Cacher.GetListCache());
Console.WriteLine(dir2Cacher.GetListCache());

