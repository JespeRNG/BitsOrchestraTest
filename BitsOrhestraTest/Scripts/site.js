$(document).ready(function () {
    $('#filterInput').on('keyup', function () {
        var value = $(this).val().toLowerCase();
        $('#contactsTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    $('th').on('click', function () {
        var table = $(this).parents('table').eq(0);
        var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()));
        this.asc = !this.asc;
        if (!this.asc) { rows = rows.reverse(); }
        for (var i = 0; i < rows.length; i++) { table.append(rows[i]); }
    });

    function comparer(index) {
        return function (a, b) {
            var valA = getCellValue(a, index), valB = getCellValue(b, index);
            return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB);
        }
    }

    function getCellValue(row, index) {
        return $(row).children('td').eq(index).text();
    }

    $('.btn-primary').on('click', function () {
        var row = $(this).closest('tr');
        row.find('td').attr('contenteditable', 'true').css('background-color', '#FFF8DC');
        $(this).text('Save').removeClass('btn-primary').addClass('btn-success');
    });

    $('.btn-success').on('click', function () {
        var row = $(this).closest('tr');
        row.find('td').attr('contenteditable', 'false').css('background-color', '');
        $(this).text('Edit').removeClass('btn-success').addClass('btn-primary');
    });

    $('.btn-danger').on('click', function () {
        var row = $(this).closest('tr');
        row.remove();
    });
});
