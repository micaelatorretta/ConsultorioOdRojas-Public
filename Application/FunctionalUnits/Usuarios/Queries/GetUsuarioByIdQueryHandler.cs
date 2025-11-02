using Domain.FunctionalUnits.Usuarios.Entities;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Queries;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.Base;
using Shared.Application.Services.Interfaces;
using Shared.Domain.Utils;

namespace Application.FunctionalUnits.Usuarios.Queries
{
    public class GetUsuarioByIdQueryHandler : BaseQueryHandler<GetUsuarioByIdQuery, GetUsuarioByIdResponse>
    {
        
        public GetUsuarioByIdQueryHandler(IWorkContext workContext) : base(workContext) { }


        public override async Task<GetUsuarioByIdResponse> Handle(GetUsuarioByIdQuery query, CancellationToken cancellationToken)
        {
            var response = new GetUsuarioByIdResponse();

            var graphFull = new GraphExplorerConfiguration() { Mode = GraphExplorerMode.Full };
            
            var usuario = await em.GetByIdAsync<Usuario>(graphFull, query.Id);

            response.Usuario = WorkContext.Services.Mapper.Map<UsuarioDTO>(usuario);
            return response;
        }
    }
}
