// Manejo del modal de confirmación de eliminación
function confirmDelete(url, itemName) {
    const deleteForm = document.getElementById('deleteForm');
    const deleteItemName = document.getElementById('deleteItemName');
    
    deleteForm.action = url;
    deleteItemName.textContent = itemName || 'Este elemento será eliminado permanentemente.';
    
    const modal = new bootstrap.Modal(document.getElementById('deleteModal'));
    modal.show();
}

// Event delegation para botones con data-delete-url
document.addEventListener('DOMContentLoaded', function() {
    document.addEventListener('click', function(e) {
        const deleteBtn = e.target.closest('[data-delete-url]');
        if (deleteBtn) {
            e.preventDefault();
            const url = deleteBtn.getAttribute('data-delete-url');
            const itemName = deleteBtn.getAttribute('data-item-name') || 'este registro';
            confirmDelete(url, itemName);
        }
    });
});
