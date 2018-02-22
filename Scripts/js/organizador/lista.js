$(document).ready(function () {
    
    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            $('#form-modal').submit();
        }
    });

    $("#exportar").click(function () {
        $("#table2excel").table2excel({
            exclude: ".noExl",
            name: "Lista",
            filename: "Lista - " + $("ol li.tg-active").html()
        });
    });

});