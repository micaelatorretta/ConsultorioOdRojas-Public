using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Usuarios.Queries
{
    public class GetUsuarioByIdQuery : BaseQuery<GetUsuarioByIdResponse>
    {
        public GetUsuarioByIdQuery() { }
      
        public int Id { get; set; }
    }
}