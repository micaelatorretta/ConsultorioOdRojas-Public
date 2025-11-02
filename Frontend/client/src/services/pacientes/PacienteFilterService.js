import { FilterOperator } from '../../constants/Filters/FilterOperator';

const PacienteFilterService = {

  /**
   *   Método para obtener filtros válidos para la busqueda.
   * */
  getCommonFilters(searchQuery) {
    const queryStr = String(searchQuery || '').trim(); // Convertir a string y limpiar espacios

    if (!queryStr) {
      return [];
    }

    return [
      {
        isGroup: true,
        isAnd: false,
        subConditions:
        [
          // Aca defino los filtros, nombre de la propiedad a filtrar y el searchQuery.value
          { 
            propertyName: 'Nombre', value: queryStr, isGroup: false, condition: FilterOperator.CONTAINS, isAnd: false
          },
          { 
            propertyName: 'Apellido', value: queryStr, isGroup: false, condition: FilterOperator.CONTAINS, isAnd: false
          },
          { 
            propertyName: 'DNI', value: queryStr, isGroup: false, condition: FilterOperator.CONTAINS, isAnd: false
          }
        ]
      },
      

    ];
  }

    
};

export default PacienteFilterService;
