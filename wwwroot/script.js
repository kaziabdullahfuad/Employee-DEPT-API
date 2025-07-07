
document.addEventListener('DOMContentLoaded', () => {
    // Base URL for the API
    const apiBaseUrl = '/api';

    // Load departments into the select dropdown
    async function loadDepartments() {
        try {
            const response = await fetch(`${apiBaseUrl}/Department`);
            if (!response.ok) throw new Error('Failed to fetch departments');
            const departments = await response.json();
            const deptSelect = document.getElementById('deptId');
            departments.forEach(dept => {
                const option = document.createElement('option');
                option.value = dept.id;
                option.textContent = dept.name;
                deptSelect.appendChild(option);
            });
        } catch (error) {
            console.error('Error loading departments:', error);
        }
    }

    // Load employees into the table
    async function loadEmployees() {
        try {
            const response = await fetch(`${apiBaseUrl}/Employee`);
            if (!response.ok) throw new Error('Failed to fetch employees');
            const employees = await response.json();
            const tableBody = document.getElementById('employeeTableBody');
            tableBody.innerHTML = '';
            employees.forEach(emp => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${emp.id}</td>
                    <td>${emp.name}</td>
                    <td>${emp.phoneNumber}</td>
                    <td>${emp.department ? emp.department.name : 'N/A'}</td>
                `;
                tableBody.appendChild(row);
            });
        } catch (error) {
            console.error('Error loading employees:', error);
        }
    }

    // Handle form submission
    document.getElementById('employeeForm').addEventListener('submit', async (e) => {
        e.preventDefault();
        const formMessage = document.getElementById('formMessage');
        formMessage.textContent = '';

        const employee = {
            id: 0,
            name: document.getElementById('name').value,
            phoneNumber: document.getElementById('phoneNumber').value,
            deptId: parseInt(document.getElementById('deptId').value)
        };

        try {
            const response = await fetch(`${apiBaseUrl}/Employee`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(employee)
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.title || 'Failed to create employee');
            }

            // Clear form and refresh employee list
            document.getElementById('employeeForm').reset();
            formMessage.textContent = 'Employee created successfully!';
            formMessage.style.color = '#28a745';
            await loadEmployees();
        } catch (error) {
            formMessage.textContent = `Error: ${error.message}`;
            console.error('Error creating employee:', error);
        }
    });

    // Initialize the page
    loadDepartments();
    loadEmployees();
});
