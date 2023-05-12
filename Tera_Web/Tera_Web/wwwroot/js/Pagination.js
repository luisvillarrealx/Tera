var table = document.getElementById('Table');
var rowsPerPage = 10;
var currentPage = 0;

function updateTable() {
    var start = currentPage * rowsPerPage + 1;
    var end = start + rowsPerPage - 1;
    var rows = table.rows;

    for (var i = 1; i < rows.length; i++) {
        if (i < start || i > end) {
            rows[i].style.display = 'none';
        } else {
            rows[i].style.display = '';
        }
    }
}

function createPagination() {
    var numPages = Math.ceil((table.rows.length - 1) / rowsPerPage);
    var pagination = document.getElementById('paginacion');

    for (var i = 0; i < numPages; i++) {
        var button = document.createElement('button');
        button.innerHTML = i + 1;
        button.addEventListener('click', function () {
            currentPage = parseInt(this.innerHTML) - 1;
            updateTable();
        });
        pagination.appendChild(button);
    }

    var prevButton = document.createElement('button');
    prevButton.innerHTML = 'Anterior';
    prevButton.addEventListener('click', function () {
        if (currentPage > 0) {
            currentPage--;
            updateTable();
        }
    });
    pagination.insertBefore(prevButton, pagination.firstChild);

    var nextButton = document.createElement('button');
    nextButton.innerHTML = 'Siguiente';
    nextButton.addEventListener('click', function () {
        if (currentPage < numPages - 1) {
            currentPage++;
            updateTable();
        }
    });
    pagination.appendChild(nextButton);

    updateTable();
}

createPagination();

function createPagination() {
    var numPages = Math.ceil((table.rows.length - 1) / rowsPerPage);
    var pagination = document.getElementById('paginacion');

    for (var i = 0; i < numPages; i++) {
        var button = document.createElement('button');
        button.innerHTML = i + 1;
        button.classList.add('btn', 'btn-secondary');
        button.addEventListener('click', function () {
            currentPage = parseInt(this.innerHTML) - 1;
            updateTable();
        });
        pagination.appendChild(button);
    }

    var prevButton = document.createElement('button');
    prevButton.innerHTML = 'Anterior';
    prevButton.classList.add('btn', 'btn-secondary');
    prevButton.addEventListener('click', function () {
        if (currentPage > 0) {
            currentPage--;
            updateTable();
        }
    });
    pagination.insertBefore(prevButton, pagination.firstChild);

    var nextButton = document.createElement('button');
    nextButton.innerHTML = 'Siguiente';
    nextButton.classList.add('btn', 'btn-primary');
    nextButton.addEventListener('click', function () {
        if (currentPage < numPages - 1) {
            currentPage++;
            updateTable();
        }
    });
    pagination.appendChild(nextButton);

    updateTable();
}