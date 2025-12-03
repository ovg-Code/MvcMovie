/* ============================================
   LIST VIEWS - Funcionalidad compartida
   ============================================ */

/**
 * Inicializa búsqueda en tabla con contador de resultados
 * @param {string} searchInputId - ID del input de búsqueda
 * @param {string} tableId - ID de la tabla
 */
function initTableSearch(searchInputId, tableId) {
    const searchInput = document.getElementById(searchInputId);
    const table = document.getElementById(tableId);
    
    if (!searchInput || !table) return;
    
    searchInput.addEventListener('input', function() {
        const filter = this.value.toLowerCase();
        const rows = table.getElementsByTagName('tr');
        
        for (let i = 1; i < rows.length; i++) {
            const row = rows[i];
            const text = row.textContent.toLowerCase();
            row.style.display = text.includes(filter) ? '' : 'none';
        }
        
        updateVisibleCount(tableId);
    });
}

/**
 * Inicializa filtro por estado con contador de resultados
 * @param {string} filterSelectId - ID del select de filtro
 * @param {string} tableId - ID de la tabla
 */
function initStatusFilter(filterSelectId, tableId) {
    const filterSelect = document.getElementById(filterSelectId);
    const table = document.getElementById(tableId);
    
    if (!filterSelect || !table) return;
    
    filterSelect.addEventListener('change', function() {
        const filterValue = this.value;
        const rows = table.getElementsByTagName('tr');
        
        for (let i = 1; i < rows.length; i++) {
            const row = rows[i];
            const status = row.getAttribute('data-status');
            
            if (filterValue === '' || status === filterValue) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        }
        
        updateVisibleCount(tableId);
    });
}

/**
 * Actualiza el contador de registros visibles
 * @param {string} tableId - ID de la tabla
 */
function updateVisibleCount(tableId) {
    const table = document.getElementById(tableId);
    if (!table) return;
    
    const rows = table.getElementsByTagName('tr');
    let visibleCount = 0;
    
    for (let i = 1; i < rows.length; i++) {
        if (rows[i].style.display !== 'none') {
            visibleCount++;
        }
    }
    
    const visibleCountElement = document.getElementById('visibleCount');
    if (visibleCountElement) {
        visibleCountElement.textContent = visibleCount;
    }
}

/**
 * Inicializa vista de lista completa (búsqueda + filtro + contador)
 * @param {Object} config - Configuración
 * @param {string} config.searchInputId - ID del input de búsqueda
 * @param {string} config.filterSelectId - ID del select de filtro
 * @param {string} config.tableId - ID de la tabla
 */
function initListView(config) {
    initTableSearch(config.searchInputId, config.tableId);
    initStatusFilter(config.filterSelectId, config.tableId);
}
