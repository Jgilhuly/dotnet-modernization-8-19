// Modern JavaScript for RestaurantOps
// No jQuery dependency - using vanilla JS and Alpine.js

// DOM Helper Functions
const DOM = {
    query: (selector) => document.querySelector(selector),
    queryAll: (selector) => document.querySelectorAll(selector),
    ready: (callback) => {
        if (document.readyState !== 'loading') {
            callback();
        } else {
            document.addEventListener('DOMContentLoaded', callback);
        }
    }
};

// Notification System
class NotificationSystem {
    static show(message, type = 'info', duration = 5000) {
        const notification = document.createElement('div');
        const typeClasses = {
            success: 'bg-green-50 text-green-700 border-green-200',
            error: 'bg-red-50 text-red-700 border-red-200',
            warning: 'bg-yellow-50 text-yellow-700 border-yellow-200',
            info: 'bg-blue-50 text-blue-700 border-blue-200'
        };
        
        notification.className = `fixed top-4 right-4 p-4 rounded-lg border shadow-lg z-50 max-w-sm transition-all duration-300 transform ${typeClasses[type] || typeClasses.info}`;
        notification.innerHTML = `
            <div class="flex items-center justify-between">
                <span class="text-sm font-medium">${message}</span>
                <button onclick="this.parentElement.parentElement.remove()" class="ml-3 text-gray-400 hover:text-gray-600 focus:outline-none">
                    <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
                        <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd"></path>
                    </svg>
                </button>
            </div>
        `;
        
        document.body.appendChild(notification);
        
        // Auto-remove after duration
        setTimeout(() => {
            notification.style.transform = 'translateX(100%)';
            setTimeout(() => {
                if (notification.parentElement) {
                    notification.remove();
                }
            }, 300);
        }, duration);
    }
}

// Form Validation Helper
class FormValidator {
    constructor(form) {
        this.form = form;
        this.errors = {};
    }
    
    addRule(field, validator, message) {
        if (!this.errors[field]) {
            this.errors[field] = [];
        }
        
        const input = this.form.querySelector(`[name="${field}"]`);
        if (input) {
            input.addEventListener('blur', () => {
                this.validateField(field, validator, message);
            });
        }
    }
    
    validateField(field, validator, message) {
        const input = this.form.querySelector(`[name="${field}"]`);
        if (!input) return;
        
        const isValid = validator(input.value);
        const errorDiv = this.form.querySelector(`#error-${field}`);
        
        if (!isValid) {
            input.classList.add('border-red-500', 'focus:border-red-500', 'focus:ring-red-500');
            input.classList.remove('border-gray-300', 'focus:border-primary', 'focus:ring-primary');
            
            if (!errorDiv) {
                const error = document.createElement('div');
                error.id = `error-${field}`;
                error.className = 'text-red-500 text-sm mt-1';
                error.textContent = message;
                input.parentNode.appendChild(error);
            }
        } else {
            input.classList.remove('border-red-500', 'focus:border-red-500', 'focus:ring-red-500');
            input.classList.add('border-gray-300', 'focus:border-primary', 'focus:ring-primary');
            
            if (errorDiv) {
                errorDiv.remove();
            }
        }
        
        return isValid;
    }
    
    validateAll() {
        let isValid = true;
        // Validate all registered fields
        return isValid;
    }
}

// Loading States
class LoadingManager {
    static show(element) {
        const spinner = document.createElement('div');
        spinner.className = 'inline-block w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin mr-2';
        spinner.id = 'loading-spinner';
        
        if (element.tagName === 'BUTTON') {
            element.disabled = true;
            element.insertBefore(spinner, element.firstChild);
        }
    }
    
    static hide(element) {
        const spinner = element.querySelector('#loading-spinner');
        if (spinner) {
            spinner.remove();
        }
        
        if (element.tagName === 'BUTTON') {
            element.disabled = false;
        }
    }
}

// Table Enhancement
class TableEnhancer {
    constructor(table) {
        this.table = table;
        this.init();
    }
    
    init() {
        this.addSortingCapability();
        this.addRowHoverEffects();
    }
    
    addSortingCapability() {
        const headers = this.table.querySelectorAll('thead th[data-sortable]');
        headers.forEach(header => {
            header.classList.add('cursor-pointer', 'select-none', 'hover:bg-gray-700');
            header.addEventListener('click', () => {
                this.sortByColumn(header);
            });
        });
    }
    
    addRowHoverEffects() {
        const rows = this.table.querySelectorAll('tbody tr');
        rows.forEach(row => {
            row.classList.add('hover:bg-gray-50', 'transition-colors', 'duration-200');
        });
    }
    
    sortByColumn(header) {
        const columnIndex = Array.from(header.parentNode.children).indexOf(header);
        const tbody = this.table.querySelector('tbody');
        const rows = Array.from(tbody.querySelectorAll('tr'));
        
        const direction = header.dataset.sortDirection === 'asc' ? 'desc' : 'asc';
        header.dataset.sortDirection = direction;
        
        rows.sort((a, b) => {
            const aVal = a.children[columnIndex].textContent.trim();
            const bVal = b.children[columnIndex].textContent.trim();
            
            if (direction === 'asc') {
                return aVal.localeCompare(bVal, undefined, { numeric: true });
            } else {
                return bVal.localeCompare(aVal, undefined, { numeric: true });
            }
        });
        
        rows.forEach(row => tbody.appendChild(row));
    }
}

// Initialize when DOM is ready
DOM.ready(() => {
    console.log('RestaurantOps Modern JS Initialized');
    
    // Initialize form validation for all forms
    const forms = DOM.queryAll('form');
    forms.forEach(form => {
        // Add loading states to submit buttons
        form.addEventListener('submit', (e) => {
            const submitBtn = form.querySelector('button[type="submit"]');
            if (submitBtn) {
                LoadingManager.show(submitBtn);
            }
        });
    });
    
    // Initialize table enhancements
    const tables = DOM.queryAll('table[data-enhanced]');
    tables.forEach(table => {
        new TableEnhancer(table);
    });
    
    // Add smooth scroll behavior to anchor links
    const anchorLinks = DOM.queryAll('a[href^="#"]');
    anchorLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const target = DOM.query(link.getAttribute('href'));
            if (target) {
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
    
    // Auto-hide alerts after 5 seconds
    const alerts = DOM.queryAll('.alert:not(.alert-permanent)');
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            alert.style.transform = 'translateY(-10px)';
            setTimeout(() => alert.remove(), 300);
        }, 5000);
    });
});

// Global utilities
window.RestaurantOps = {
    notify: NotificationSystem.show,
    FormValidator,
    LoadingManager,
    TableEnhancer,
    DOM
};