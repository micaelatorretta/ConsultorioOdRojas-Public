import { FilterOperator } from '../../constants/Filters/FilterOperator';

const UsuarioFilterService = {
  /**
   * Método para obtener filtros válidos para la búsqueda de usuarios.
   *
   * Genera un grupo de condiciones OR que busca coincidencias parciales
   * en las propiedades Nombre, Apellido y DNI.
   *
   * @param {string} searchQuery - Texto de búsqueda ingresado por el usuario
   * @returns {Array} - Lista de filtros compatibles con el backend
   */
  getCommonFilters(searchQuery) {
    const queryStr = String(searchQuery || '').trim(); // Convertir a string y limpiar espacios

    if (!queryStr) {
      return [];
    }

    return [
      {
        isGroup: true,
        isAnd: false, // OR entre condiciones
        subConditions: [
          {
            propertyName: 'Nombre',
            value: queryStr,
            isGroup: false,
            condition: FilterOperator.CONTAINS,
            isAnd: false
          },
          {
            propertyName: 'Apellido',
            value: queryStr,
            isGroup: false,
            condition: FilterOperator.CONTAINS,
            isAnd: false
          },
          {
            propertyName: 'DNI',
            value: queryStr,
            isGroup: false,
            condition: FilterOperator.CONTAINS,
            isAnd: false
          }
        ]
      }
    ];
  }
};

export default UsuarioFilterService;
