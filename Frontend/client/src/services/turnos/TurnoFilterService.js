import { FilterOperator } from '../../constants/Filters/FilterOperator';

const TurnoFilterService = {

  /**
   *   Método para obtener filtros válidos para la busqueda.
   * */
  getCommonFilters(searchQuery) {
    const queryStr = String(searchQuery || '').trim(); // Convertir a string y limpiar espacios

    if (!queryStr) {
      return [];
    }

    return [
      // Aca defino los filtros, nombre de la propiedad a filtrar y el searchQuery.value
      { 
        propertyName: 'Descripcion', value: queryStr, isGroup: false, condition: FilterOperator.CONTAINS, isAnd: false
      },
      // Agregar más filtros si es necesario...
      // { 
      //   propertyName: 'NombrePropiedad', value: searchQuery.value
      // },
    ];
  }

    
};

export default TurnoFilterService;
