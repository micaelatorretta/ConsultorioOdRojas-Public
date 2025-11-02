using AutoMapper;
using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.ObrasSociales.Entities;
using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Domain.FunctionalUnits.Usuarios.Entities;
using Domain.ValueObjects;
using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.ValueObjects;
using Portable.ValueObjectsDTO;
using Shared.Application.Extensions;

namespace Application.Mappings
{

    public class ApplicationMappings : Profile
    {
        public ApplicationMappings()
        {
            #region Turnos
            CreateMap<TurnoDTO, Turno> ()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region ObrasSociales
            CreateMap<ObraSocialDTO, ObraSocial>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region Usuarios
            CreateMap<UsuarioDTO, Usuario>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region Pacientes
            CreateMap<PacienteDTO, Paciente>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region Odontogramas

            #region Odontograma
            CreateMap<OdontogramaDTO, Odontograma>()
                .PreserveReferences()
                .IgnoreAllNonExisting()
                .ReverseMap();
            
            CreateMap<OdontogramaPiezaDentalDTO, OdontogramaPiezaDental>()
                .PreserveReferences()
                .IgnoreAllNonExisting()
                .ReverseMap();
            
            CreateMap<OdontogramaCaraDentalDTO, OdontogramaCaraDental>()
                .PreserveReferences()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region PiezaDental
            CreateMap<PiezaDentalDTO, PiezaDental>()
                .PreserveReferences()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region CaraDental
            CreateMap<CaraDentalDTO, CaraDental>()
                .PreserveReferences()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #endregion

            #region Prestaciones

            #region Nomencladores
            CreateMap<NomencladorDTO, Nomenclador>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #endregion

            #region HistoriasClinicas
            CreateMap<HistoriaClinicaDTO, HistoriaClinica>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region EntidadDummies

            #region EntidadDummyA
            CreateMap<EntidadDummyDTO, EntidadDummy>()
                .IgnoreAllNonExisting()
                .ReverseMap();

            CreateMap<EntidadDummyBDTO, EntidadDummyB>()
                .IgnoreAllNonExisting()
                .ReverseMap();

            CreateMap<EntidadDummyCDTO, EntidadDummyC>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #region EntidadDummyD
            CreateMap<EntidadDummyDDTO, EntidadDummyD>()
                .IgnoreAllNonExisting()
                .ReverseMap();
            #endregion

            #endregion

            #region ValueObjects
            CreateMap<ContactoTelefonicoDTO, ContactoTelefonico>()
                .IgnoreAllNonExisting()
                .ReverseMap();

            CreateMap<HabitoDTO, Habito>()
              .IgnoreAllNonExisting()
              .ReverseMap();

            CreateMap<RespuestaConDetalleDTO, RespuestaConDetalle>()
              .IgnoreAllNonExisting()
              .ReverseMap();

            CreateMap<RespuestaConFechaDTO, RespuestaConFecha>()
              .IgnoreAllNonExisting()
              .ReverseMap();
            #endregion
        }
    }
}
