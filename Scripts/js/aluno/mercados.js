$(function () {

    $(".bg-menu").height($("#tg-main").height());
    $('select').not('.no-js').select2();

    $("#incluir_btn_alterar").click(function () {
        $('#form-modal_usuario').validationEngine('attach');
        if ($('#form-modal_usuario').validationEngine('validate')) {
            IncluirUsuario();
        }
    });

});

function IncluirUsuario() {

    var txespecialidades = $("#form-modal_usuario #txespecialidades").val();
    var txmercados = $("#form-modal_usuario #txmercados").val();

    $.ajax({
        type: "POST",
        url: '/Aluno/Mercado/IncluirConcluir',
        data: { especialidades: txespecialidades.toString(), mercados: txmercados.toString() },
        dataType: "json",
        traditional: true,
        success: function (json) {

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000,
            })

        }
    });

}

function addMercado() {
    $('#form-modal-mercados').validationEngine('attach');
    if ($('#form-modal-mercados').validationEngine('validate')) {
        var idmercado = $("#idmercado").val();
        var mercado = $("#idmercado option:selected").html();
        if (!inArray("txmercados", idmercado + "|" + mercado)) {
            var cont = $("#listmercado li").length;
            var txt = "";
            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + cont + ")'></i><span>" + mercado + "</span></li>");
            if ($("#txmercados").val() == "") { txt = idmercado + "|" + mercado; } else { txt = $("#txmercados").val() + "," + idmercado + "|" + mercado; }
            $("#txmercados").val(txt);
        } else {
            alert("Já selecionado")
        }
    }
}

function removeMercados(i) {
    var arr = $("#txmercados").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txmercados").val(txt.slice(1));
    $("#listmercado").empty();
    if ($("#txmercados").val() != "") {
        arr = $("#txmercados").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + x + ")'></i><span>" + arrT[1] + "</span></li>");
        }
    }
}

function addEspecialidade() {
    $('#form-modal-especialidades').validationEngine('attach');
    if ($('#form-modal-especialidades').validationEngine('validate')) {
        var idespecialidade = $("#idespecialidade").val();
        var especialidade = $("#idespecialidade option:selected").html();
        if (!inArray("txespecialidades", idespecialidade + "|" + especialidade)) {
            var cont = $("#listespecialidade li").length;
            var txt = "";
            $("#listespecialidade").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEspecialidades(" + cont + ")'></i><span>" + especialidade + "</span></li>");
            if ($("#txespecialidades").val() == "") { txt = idespecialidade + "|" + especialidade; } else { txt = $("#txespecialidades").val() + "," + idespecialidade + "|" + especialidade; }
            $("#txespecialidades").val(txt);
        } else {
            alert("Já selecionado")
        }
    }
}

function removeEspecialidades(i) {
    var arr = $("#txespecialidades").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txespecialidades").val(txt.slice(1));
    $("#listespecialidade").empty();
    if ($("#txespecialidades").val() != "") {
        arr = $("#txespecialidades").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listespecialidade").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEspecialidades(" + x + ")'></i><span>" + arrT[1] + "</span></li>");
        }
    }
}

function inArray(campo, val) {
    arr = $("#" + campo).val().split(",");
    if ($.inArray(val, arr) < 0) { return false; } else { return true; }
}
