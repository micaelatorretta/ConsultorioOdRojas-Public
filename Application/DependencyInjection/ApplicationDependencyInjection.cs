using System.Reflection;
using Application.FunctionalUnits.Auth.Commands;
using Application.FunctionalUnits.EntidadDummies.Commands;
using Application.FunctionalUnits.EntidadDummies.Queries;
using Application.FunctionalUnits.HistoriasClinicas.Commands;
using Application.FunctionalUnits.HistoriasClinicas.Queries;
using Application.FunctionalUnits.ObrasSociales.Commands;
using Application.FunctionalUnits.ObrasSociales.Queries;
using Application.FunctionalUnits.Odontogramas.Commands;
using Application.FunctionalUnits.Odontogramas.Queries;
using Application.FunctionalUnits.Pacientes.Commands;
using Application.FunctionalUnits.Pacientes.Queries;
using Application.FunctionalUnits.Prestaciones.Commands;
using Application.FunctionalUnits.Prestaciones.Queries;
using Application.FunctionalUnits.RespaldosDeInformacion.Commands;
using Application.FunctionalUnits.Turnos.Commands;
using Application.FunctionalUnits.Turnos.Queries;
using Application.FunctionalUnits.Usuarios.Commands;
using Application.FunctionalUnits.Usuarios.Queries;
using Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Portable.FunctionalUnits.Auth.Commands;
using Portable.FunctionalUnits.Auth.Responses;
using Portable.FunctionalUnits.EntidadDummies.Commands;
using Portable.FunctionalUnits.EntidadDummies.Queries;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Portable.FunctionalUnits.HistoriasClinicas.Commands;
using Portable.FunctionalUnits.HistoriasClinicas.Queries;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Portable.FunctionalUnits.ObrasSociales.Commands;
using Portable.FunctionalUnits.ObrasSociales.Queries;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Portable.FunctionalUnits.Odontogramas.Commands;
using Portable.FunctionalUnits.Odontogramas.Queries;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Portable.FunctionalUnits.Pacientes.Commands;
using Portable.FunctionalUnits.Pacientes.Queries;
using Portable.FunctionalUnits.Pacientes.Responses;
using Portable.FunctionalUnits.Prestaciones.Commands;
using Portable.FunctionalUnits.Prestaciones.Queries;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Portable.FunctionalUnits.RespaldosDeInformacion.Commands;
using Portable.FunctionalUnits.RespaldosDeInformacion.Responses;
using Portable.FunctionalUnits.Turnos.Commands;
using Portable.FunctionalUnits.Turnos.Queries;
using Portable.FunctionalUnits.Turnos.Responses;
using Portable.FunctionalUnits.Usuarios.Commands;
using Portable.FunctionalUnits.Usuarios.Queries;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Application.DependencyInjection;

namespace Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services, Assembly assembly)
        {
            services.AddSharedApplicationLayer(assembly);

            // AutoMapper
            services.AddAutoMapper(typeof(ApplicationMappings));

            // Se definen todas las inyeciones de los handlers
            HandlerInjection(services);
        
        }


        public static void HandlerInjection(this IServiceCollection services)
        {
            // Inyeccion general
            //services.AddTransient(typeof(IRequestHandler<,>), typeof(BaseCommandHandler<,>));
            // services.AddTransient(typeof(IRequestHandler<,>), typeof(BaseQueryHandler<,>));


            #region Respaldos de Informacion
            services.AddTransient<IRequestHandler<CreateBackupCommand, CreateBackupResponse>, CreateBackupCommandHandler>();
            services.AddTransient<IRequestHandler<RestoreDatabaseCommand, RestoreDatabaseResponse>, RestoreDatabaseCommandHandler>();
            #endregion

            #region Turnos
            services.AddTransient<IRequestHandler<CreateTurnoCommand, CreateTurnoResponse>, CreateTurnoCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateTurnoCommand, UpdateTurnoResponse>, UpdateTurnoCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteTurnoCommand, DeleteTurnoResponse>, DeleteTurnoCommandHandler>();

            services.AddTransient<IRequestHandler<GetTurnoByIdQuery, GetTurnoByIdResponse>, GetTurnoByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetTurnoPagedQuery, GetTurnoPagedResponse>, GetTurnoPagedQueryHandler>();
            #endregion

            #region ObrasSociales
            services.AddTransient<IRequestHandler<CreateObraSocialCommand, CreateObraSocialResponse>, CreateObraSocialCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateObraSocialCommand, UpdateObraSocialResponse>, UpdateObraSocialCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteObraSocialCommand, DeleteObraSocialResponse>, DeleteObraSocialCommandHandler>();

            services.AddTransient<IRequestHandler<GetObraSocialByIdQuery, GetObraSocialByIdResponse>, GetObraSocialByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetObraSocialPagedQuery, GetObraSocialPagedResponse>, GetObraSocialPagedQueryHandler>();
            #endregion

            #region Usuarios
            services.AddTransient<IRequestHandler<CreateUsuarioCommand, CreateUsuarioResponse>, CreateUsuarioCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateUsuarioCommand, UpdateUsuarioResponse>, UpdateUsuarioCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteUsuarioCommand, DeleteUsuarioResponse>, DeleteUsuarioCommandHandler>();

            services.AddTransient<IRequestHandler<GetUsuarioByIdQuery, GetUsuarioByIdResponse>, GetUsuarioByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetUsuarioPagedQuery, GetUsuarioPagedResponse>, GetUsuarioPagedQueryHandler>();
            #endregion

            #region Pacientes
            services.AddTransient<IRequestHandler<CreatePacienteCommand, CreatePacienteResponse>, CreatePacienteCommandHandler>();
            services.AddTransient<IRequestHandler<UpdatePacienteCommand, UpdatePacienteResponse>, UpdatePacienteCommandHandler>();
            services.AddTransient<IRequestHandler<DeletePacienteCommand, DeletePacienteResponse>, DeletePacienteCommandHandler>();

            services.AddTransient<IRequestHandler<GetPacienteByIdQuery, GetPacienteByIdResponse>, GetPacienteByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetPacientePagedQuery, GetPacientePagedResponse>, GetPacientePagedQueryHandler>();
            #endregion

            #region Auth
            services.AddTransient<IRequestHandler<LoginCommand, LoginResponse>, LoginCommandHandler>();
            #endregion

            #region Odontogramas

            #region Odontograma
            services.AddTransient<IRequestHandler<EliminarPrestacionOdontogramaCommand, EliminarPrestacionOdontogramaResponse>, EliminarPrestacionOdontogramaCommandHandler>();
            services.AddTransient<IRequestHandler<AplicarCaraOdontogramaCommand, AplicarCaraOdontogramaResponse>, AplicarCaraOdontogramaCommandHandler>();
            services.AddTransient<IRequestHandler<CreateOdontogramaCommand, CreateOdontogramaResponse>, CreateOdontogramaCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateOdontogramaCommand, UpdateOdontogramaResponse>, UpdateOdontogramaCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteOdontogramaCommand, DeleteOdontogramaResponse>, DeleteOdontogramaCommandHandler>();

            services.AddTransient<IRequestHandler<GetOdontogramaByIdQuery, GetOdontogramaByIdResponse>, GetOdontogramaByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetOdontogramaPagedQuery, GetOdontogramaPagedResponse>, GetOdontogramaPagedQueryHandler>();
            #endregion

            #region Piezas Dentales
            services.AddTransient<IRequestHandler<CreatePiezaDentalCommand, CreatePiezaDentalResponse>, CreatePiezaDentalCommandHandler>();
            services.AddTransient<IRequestHandler<UpdatePiezaDentalCommand, UpdatePiezaDentalResponse>, UpdatePiezaDentalCommandHandler>();
            services.AddTransient<IRequestHandler<DeletePiezaDentalCommand, DeletePiezaDentalResponse>, DeletePiezaDentalCommandHandler>();

            services.AddTransient<IRequestHandler<GetPiezaDentalByIdQuery, GetPiezaDentalByIdResponse>, GetPiezaDentalByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetPiezaDentalPagedQuery, GetPiezaDentalPagedResponse>, GetPiezaDentalPagedQueryHandler>();
            #endregion

            #region Caras Dentales
            services.AddTransient<IRequestHandler<CreateCaraDentalCommand, CreateCaraDentalResponse>, CreateCaraDentalCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateCaraDentalCommand, UpdateCaraDentalResponse>, UpdateCaraDentalCommandHandler>();
            services.AddTransient<IRequestHandler<GetCaraDentalByIdQuery, GetCaraDentalByIdResponse>, GetCaraDentalByIdQueryHandler>();
            
            services.AddTransient<IRequestHandler<GetCaraDentalPagedQuery, GetCaraDentalPagedResponse>, GetCaraDentalPagedQueryHandler>();
            services.AddTransient<IRequestHandler<DeleteCaraDentalCommand, DeleteCaraDentalResponse>, DeleteCaraDentalCommandHandler>();
            #endregion
            #endregion

            #region Prestaciones

            #region Nomencladores
            services.AddTransient<IRequestHandler<CreateNomencladorCommand, CreateNomencladorResponse>, CreateNomencladorCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateNomencladorCommand, UpdateNomencladorResponse>, UpdateNomencladorCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteNomencladorCommand, DeleteNomencladorResponse>, DeleteNomencladorCommandHandler>();

            services.AddTransient<IRequestHandler<GetNomencladorByIdQuery, GetNomencladorByIdResponse>, GetNomencladorByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetNomencladorPagedQuery, GetNomencladorPagedResponse>, GetNomencladorPagedQueryHandler>();
            #endregion

            #endregion

            #region HistoriasClinicas
            services.AddTransient<IRequestHandler<SaveHistoriaClinicaCommand, SaveHistoriaClinicaResponse>, SaveHistoriaClinicaCommandHandler>();

            services.AddTransient<IRequestHandler<GetHistoriaClinicaByIdQuery, GetHistoriaClinicaByIdResponse>, GetHistoriaClinicaByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetHistoriaClinicaPagedQuery, GetHistoriaClinicaPagedResponse>, GetHistoriaClinicaPagedQueryHandler>();
            #endregion
            #region Commands Handlers

            #region EntidadDummies

            #region EntidadDummy
            services.AddTransient<IRequestHandler<CreateEntidadDummyCommand, CreateEntidadDummyResponse>, CreateEntidadDummyCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateEntidadDummyCommand, UpdateEntidadDummyResponse>, UpdateEntidadDummyCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteEntidadDummyCommand, DeleteEntidadDummyResponse>, DeleteEntidadDummyCommandHandler>();
            #endregion

            #region EntidadDummyD
            services.AddTransient<IRequestHandler<CreateEntidadDummyDCommand, CreateEntidadDummyDResponse>, CreateEntidadDummyDCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateEntidadDummyDCommand, UpdateEntidadDummyDResponse>, UpdateEntidadDummyDCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteEntidadDummyDCommand, DeleteEntidadDummyDResponse>, DeleteEntidadDummyDCommandHandler>();
            #endregion

            #endregion

            #endregion

            #region Query Handlers

            #region EntidadDummies

            #region EntidadDummy
            services.AddTransient<IRequestHandler<GetEntidadDummyByIdQuery, GetEntidadDummyByIdResponse>, GetEntidadDummyByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEntidadDummyPagedQuery, GetEntidadDummyPagedResponse>, GetEntidadDummyPagedQueryHandler>();
            #endregion

            #region EntidadDummyD
            services.AddTransient<IRequestHandler<GetEntidadDummyDByIdQuery, GetEntidadDummyDByIdResponse>, GetEntidadDummyDByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetEntidadDummyDPagedQuery, GetEntidadDummyDPagedResponse>, GetEntidadDummyDPagedQueryHandler>();
            #endregion


            #endregion

            #endregion
        }

    }
}
