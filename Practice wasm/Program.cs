using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Practice_wasm;
using Practice_wasm.Interface;
using Practice_wasm.Services;
using Radzen;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Practice_wasm.Extensiones;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<DialogService>();


//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7034/") });


// Inyecta el servicio genérico
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

// Inyecta el servicio específico para HabilidadService
builder.Services.AddScoped<IHabilidadService, HabilidadService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IInventarioService, IventarioService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddScoped<IloginService, LoginService>();




builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();



await builder.Build().RunAsync();
