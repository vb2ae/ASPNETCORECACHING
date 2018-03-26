# Asp.net Core Caching

Sample app which shows how to use caching in asp.net core 2.0



With classic Asp.Net you used the HtppContext in the System.Web namespace for caching. Since Asp.Net Core was created to be cross platform caching is done differently.

Once the app is created go to the startup.cs class. and change the ConfigureServices method to this

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();
        }  


Now to use this we need to get access to the IMemoryCache in the controllers constructor.  In the demo I will use the about page in the project created

        IMemoryCache caching= null;


        public HomeController(IMemoryCache cache)
        {
            caching = cache;
        }

To use it. I am just going to cache the DateTime

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            DateTime currentTime;
            if (!caching.TryGetValue<DateTime>("currentTime", out currentTime))
            {
                currentTime = DateTime.Now;
                caching.Set<DateTime>("currentTime", currentTime, DateTimeOffset.Now.AddMinutes(10));
            }
        
                
            ViewBag.Time = currentTime;

            return View();
        }

To cache something use the Set method.  I would recommend using the version that allow you to specify the type of object.

When setting the cache you pass in the key, what you want cached and optionally how long you want it cached.  I set it to 10 minutes in this example.

Getting items from the cache you use TryGetValue it will return true if the value was found and it passes it out in an out parameter.

If you need to remove something use the cache Remove method where you pass in the key of the object you want to remove.

To show the item is cached I changed the About.cshtml to show the current date time and the cached value

@{
    ViewData["Title"] = "About";
}
<h2>@ViewData["Title"]</h2>
<h3>@ViewData["Message"]</h3>

<p>Use this area to provide additional information.</p>
@DateTime.Now
<br/>
@ViewBag.Time
