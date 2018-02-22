$(document).ready(function () {

    $("#form_ciclo #incluir_btn").click(function () {
        $('#form_ciclo').validationEngine('attach');
        if ($('#form_ciclo').validationEngine('validate')) {
            $("#form_ciclo").submit();
        }
    });

    $("#incluir_exportar").click(function () {
        $("#table2excel").table2excel({
            exclude: ".noExl",
            name: "Relatório",
            filename: "Relatório"
        });
    });

});
