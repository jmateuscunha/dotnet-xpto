using Xpto.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(o => {  o.UseStartup<Startup>(); }).Build().Run();
    }
}