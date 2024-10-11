using ApiPractice.Auth;
using AutoMapper;
using Capa_Servicio.Interface;
using CapaDominio;
using CapaDominio.Utils;

namespace ApiPractice.EndPoints
{
    public static class LoginEndPoint
    {
        public static void RegisterLoginEndpoints(this IEndpointRouteBuilder routes)
        {

            var app = routes.MapGroup("/login");


            app.MapPost("/", async (Usuariodto userModel, TokenService service, ILoginService loginservice, IMapper mapper) =>

            {
                var response = new LoginResponse<Usuariodto>();

                try
                {

                    var user = await loginservice.Login(userModel.NombreUsuario, userModel.Password);
                    var userdto = mapper.Map<Usuariodto>(user);

                    if (user != null)
                    {
                        var token = service.GenerateToken(user);

                        response.value = userdto;
                        response.status = true;
                        response.token = token;
                        return Results.Ok(response);

                    }
                    else
                    {
                        response.status = false;
                        response.message = "Usuario no encontrado1";
                        return Results.Ok(response);
                    }
                }
                catch (Exception ex)
                {
                    response.status = false;
                    response.message = "Usuario no encontrado !";
                    return Results.BadRequest(response);
                }

            }).AllowAnonymous();
        }
    }
}
