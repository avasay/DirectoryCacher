using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddMemoryCache())
    .Build();

IMemoryCache cache = host.Services.GetRequiredService<IMemoryCache>();

string dir1 = Environment.CurrentDirectory + @"\Images\abstract\";
string dir2 = Environment.CurrentDirectory + @"\Images\biology\";

DirectoryCacherConsoleApp.DirectoryCacher dirCacher = new DirectoryCacherConsoleApp.DirectoryCacher(cache);

try
{

    // First Pass
    Console.WriteLine(dirCacher.GetListCache(dir1));
    Console.WriteLine(dirCacher.GetListCache(dir2));

    // Second Pass
    Console.WriteLine(dirCacher.GetListCache(dir1));
    Console.WriteLine(dirCacher.GetListCache(dir2));
}
catch (Exception ex)
{
    // Do something
}

