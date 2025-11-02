import { reactive } from 'vue';

/**
 * Hook para manejar paginacion
 * @param {Function} fetchFunction Funcion para buscar datos, depende de cada CRUD
 * por ejemplo fetchTurnos o fetchObrasSociales
 */
export function usePagination(fetchFunction) {
  const pagination = reactive({
    page: 1,
    itemsPerPage: 10,
    totalItems: 0
  });

  const updatePage = (newPage) => {
    pagination.page = newPage;
    fetchFunction();
  };

  const updateItemsPerPage = (newItemsPerPage) => {
    pagination.itemsPerPage = newItemsPerPage;
    pagination.page = 1;
    fetchFunction();
  };

  return {
    pagination,
    updatePage,
    updateItemsPerPage
  };
}