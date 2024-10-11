using Practice_wasm.Models;
using Practice_wasm.Utils;

namespace Practice_wasm.Interface
{
    public interface IloginService
    {
        Task<LoginResponse<UsuarioDto>> login(UsuarioDto usuario);
    }

}
