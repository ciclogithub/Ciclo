$(document).ready(function () {

    $("#incluir_exportar").click(function () {
        $("#table2excel").table2excel({
            exclude: ".noExl",
            name: "Lista",
            filename: "Lista_de_Alunos_" + $("#curso_name").val().replace(/ /g, "_")
        });
    });

});