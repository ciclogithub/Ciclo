$(function () {

    $(".bg-menu").height($("#tg-main").height());

    $('select').not('.no-js').select2();

    if ($("#idpais").val() != "") {
        ListaEstados($("#idpais").val(), $("#tempestado").val());
        if ($("#tempestado").val() > 0) {
            ListaCidades($("#tempestado").val(), $("#tempcidade").val());
        }
    }

    $("#incluir_btn_alterar").click(function () {
        $('#form-modal_usuario').validationEngine('attach');
        if ($('#form-modal_usuario').validationEngine('validate')) {
            IncluirUsuario();
        }
    });

});

function IncluirUsuario() {

    var txaluno = $("#form-modal_usuario #txaluno").val();
    var txtelefone = $("#form-modal_usuario #txtelefone").val();
    var idcidade = $("#form-modal_usuario #idcidade").val();
    var txempresa = $("#form-modal_usuario #txempresa").val();
    var txredes = $("#form-modal_usuario #txredes").val();
    //var txmercados = $("#form-modal_usuario #txmercados").val();

    $.ajax({
        type: "POST",
        url: '/Aluno/Perfil/IncluirConcluir',
        data: { nome: txaluno, telefone: txtelefone.toString(), cidade: idcidade, empresa: txempresa, redes: txredes.toString() },
        //data: { nome: txaluno, telefone: txtelefone.toString(), cidade: idcidade, empresa: txempresa, redes: txredes.toString(), mercados: txmercados.toString() },
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

function addTelefone() {
    $('#form-modal-telefone').validationEngine('attach');
    if ($('#form-modal-telefone').validationEngine('validate')) {
        var whatsapp = ($("#flwhatsapp").is(":checked") ? 1 : 0);
        var temp = $("#temptelefone").val();
        var cont = $("#listtelefone li").length;
        var txt = "";
        $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + cont + ")'></i><span>" + temp + (whatsapp == 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        $("#temptelefone").val("");
        if ($("#txtelefone").val() == "") { txt = whatsapp + "|" + temp; } else { txt = $("#txtelefone").val() + "," + whatsapp + "|" + temp; }
        $("#txtelefone").val(txt);
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro");
    }
}

function removeTelefone(i) {
    var arr = $("#txtelefone").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txtelefone").val(txt.slice(1));
    $("#listtelefone").empty();
    if ($("#txtelefone").val() != "") {
        arr = $("#txtelefone").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listtelefone").append("<li><i class='glyphicon glyphicon-trash' onclick='removeTelefone(" + x + ")'></i><span>" + arrT[1] + (arrT[0] == 1 ? " <i class='fa fa-whatsapp'></i> " : "") + "</span></li>");
        }
    }
}

function addRedes() {
    $('#form-modal-redes').validationEngine('attach');
    if ($('#form-modal-redes').validationEngine('validate')) {
        var temp = $("#tempredes").val();
        var idrede = $("#idredesocial").val();
        var rede = $("#idredesocial option:selected").attr("name");
        var cont = $("#listredes li").length;
        var txt = "";
        $("#listredes").append("<li><i class='glyphicon glyphicon-trash' onclick='removeRedes(" + cont + ")'></i><span><i class='fa " + rede + "'></i> " + temp + "</span></li>");
        $("#tempredes").val("");
        if ($("#txredes").val() == "") { txt = idrede + "|" + temp + "|" + rede; } else { txt = $("#txredes").val() + "," + idrede + "|" + temp + "|" + rede; }
        $("#txredes").val(txt);
    }
}

function removeRedes(i) {
    var arr = $("#txredes").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txredes").val(txt.slice(1));
    $("#listredes").empty();
    if ($("#txredes").val() != "") {
        arr = $("#txredes").val().split(",");
        for (x = 0; x < arr.length; x++) {
            arrT = arr[x].split("|");
            $("#listredes").append("<li><i class='glyphicon glyphicon-trash' onclick='removeRedes(" + x + ")'></i><span><i class='fa " + arrT[2] + "'></i>" + arrT[1] + "</span></li>");
        }
    }
}

//function addMercado() {
//    $('#form-modal-mercados').validationEngine('attach');
//    if ($('#form-modal-mercados').validationEngine('validate')) {
//        var idmercado = $("#idmercado").val();
//        var mercado = $("#idmercado option:selected").html();
//        if (!inArray("txmercados", idmercado + "|" + mercado)) {
//            var cont = $("#listmercado li").length;
//            var txt = "";
//            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + cont + ")'></i><span>" + mercado + "</span></li>");
//            if ($("#txmercados").val() == "") { txt = idmercado + "|" + mercado; } else { txt = $("#txmercados").val() + "," + idmercado + "|" + mercado; }
//            $("#txmercados").val(txt);
//        } else {
//            alert("Já selecionado")
//        }
//    }
//}

//function removeMercados(i) {
//    var arr = $("#txmercados").val().split(",");
//    var txt = "";
//    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
//    $("#txmercados").val(txt.slice(1));
//    $("#listmercado").empty();
//    if ($("#txmercados").val() != "") {
//        arr = $("#txmercados").val().split(",");
//        for (x = 0; x < arr.length; x++) {
//            arrT = arr[x].split("|");
//            $("#listmercado").append("<li><i class='glyphicon glyphicon-trash' onclick='removeMercados(" + x + ")'></i><span>" + arrT[1] + "</span></li>");
//        }
//    }
//}

function inArray(campo, val) {
    arr = $("#" + campo).val().split(",");
    if ($.inArray(val, arr) < 0) { return false; } else { return true; }
}

function whatsapp(o) {
    i = $("#flwhatsapp").is(":checked");
    if (i) {
        $('#flwhatsapp').prop('checked', false);
        $('#whatsapp_label').removeClass("verde_escuro");
    } else {
        $('#flwhatsapp').prop('checked', true);
        $('#whatsapp_label').addClass("verde_escuro");
    }

}
