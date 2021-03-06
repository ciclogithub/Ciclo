﻿$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#importar_empresas").click(function () {
        Modal("/Painel/Empresas/Importar", "", "Importar Empresas", "");
    });

    $("#pesquisa_empresa").click(function () {
        EmpresaPesquisar();
    });

    $("#incluir_btn_empresa").click(function () {
        $('#form-modal_empresa').validationEngine('attach');
        if ($('#form-modal_empresa').validationEngine('validate')) {
            ValidaEmpresaInclusao();
        }
    });

    if ($("#idpais")) {
        if ($("#idpais").val() != "") {
            ListaEstados($("#idpais").val(), $("#tempestado").val());
            if ($("#tempestado").val() > 0) {
                ListaCidades($("#tempestado").val(), $("#tempcidade").val());
            }
        }
    }

    $("#txcep").on("blur", function () {
        if ($(this).val().length == 9) {
            $("#dv_cep").html("<br>Aguarde, buscando informaçoes ...");
            $.ajax({
                type: "POST",
                url: "/Painel/Empresas/Cep",
                data: { cep: $(this).val() },
                dataType: "json",
                traditional: true,
                success: function (data) {

                    if (data.idestado == 0) {
                        $("#dv_cep").html(data.endereco);
                    } else {
                        $("#txlogradouro").val(data.endereco);
                        $("#idestado").val(data.idestado);
                        ListaCidades($("#idestado").val(), data.idcidade);
                        $("#txbairro").val(data.bairro);
                        $("#dv_cep").html("");
                    }
                },
                error: function () {
                    $("#dv_cep").html("<br>Não foi possível pesquisar o cep");
                }
            });
        }
    });
});

function EmpresasTodos() {
    window.location = "/Painel/Empresas/?pagina=1&filtro=";
}

function EmpresaPesquisar() {
    window.location = "/Painel/Empresas/?filtro=" + $("#filtro_pesquisa").val();
}

function Empresas(id) {
    Modal("/Painel/Empresas/Incluir", id, "Empresas", "");
}

function EmpresaAlterar() {

    cont = 0;
    val = 0;
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            cont++;
            val = $(this).val();
        }
    });

    if (cont == 0) {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    } else {
        if (cont > 1) {
            swal({ title: "Selecione somente 1 registro para alterar", type: "error", timer: 3000 });
        } else {
            Empresas(val);
        }
    }

}

function EmpresaExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        ValidaEmpresaExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function IncluirEmpresa() {

    var idempresa = $("#form-modal_empresa #idempresa").val();
    var txcodigo = $("#form-modal_empresa #txcodigo").val();
    var txempresa = $("#form-modal_empresa #txempresa").val();
    var txemail = $("#form-modal_empresa #txemail").val();
    var txtelefone = $("#form-modal_empresa #txtelefone").val();
    var txcnpj = $("#form-modal_empresa #txcnpj").val();
    var txcep = $("#form-modal_empresa #txcep").val();
    var idcidade = $("#form-modal_empresa #idcidade").val();
    var txbairro = $("#form-modal_empresa #txbairro").val();
    var txlogradouro = $("#form-modal_empresa #txlogradouro").val();
    var txnumero = $("#form-modal_empresa #txnumero").val();
    var txcomplemento = $("#form-modal_empresa #txcomplemento").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Empresas/IncluirConcluir',
        data: { id: idempresa, codigo: txcodigo, empresa: txempresa, email: txemail.toString(), telefone: txtelefone.toString(), cnpj: txcnpj, cep: txcep, cidade: idcidade, bairro: txbairro, logradouro: txlogradouro, numero: txnumero, complemento: txcomplemento },
        dataType: "json",
        traditional: true,
        success: function (json) {

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    closeModal("EmpresaPesquisar()");
                }
                if (result.value) {
                    closeModal("EmpresaPesquisar()");
                }
            });

        }
    });

}

function addEmail() {
    $('#form-modal-email').validationEngine('attach');
    if ($('#form-modal-email').validationEngine('validate')) {
        var temp = $("#tempemail").val();
        var cont = $("#listemail li").length;
        var txt = "";
        $("#listemail").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEmail(" + cont + ")'></i><span>" + temp + "</span></li>");
        $("#tempemail").val("");
        if ($("#txemail").val() == "") { txt = temp; } else { txt = $("#txemail").val() + "," + temp; }
        $("#txemail").val(txt);
    }
}

function removeEmail(i) {
    var arr = $("#txemail").val().split(",");
    var txt = "";
    for (x = 0; x < arr.length; x++) { if (x != i) { txt = txt + "," + arr[x]; } }
    $("#txemail").val(txt.slice(1));
    $("#listemail").empty();
    if ($("#txemail").val() != "") {
        arr = $("#txemail").val().split(",");
        for (x = 0; x < arr.length; x++) {
            $("#listemail").append("<li><i class='glyphicon glyphicon-trash' onclick='removeEmail(" + x + ")'></i><span>" + arr[x] + "</span></li>");
        }
    }
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

function ValidaEmpresaInclusao() {

    $.post("/Painel/Empresas/VerificaEmpresa", { id: $("#form-modal_empresa #idempresa").val(), nome: $("#form-modal_empresa #txempresa").val(), cnpj: $("#form-modal_empresa #txcnpj").val() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Já existe uma empresa com o mesmo nome, confirma a gravação de um novo registro',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    IncluirEmpresa();
                }
            });
        } else {
            if (data == 2) {
                swal({ title: "CNPJ já cadastrado", type: "error", timer: 3000 });
            } else {
                IncluirEmpresa();
            }
        }
    });
}

function ValidaEmpresaExcluir(ids) {
    $.post("/Painel/Empresas/VerificaEmpresaExcluir", { id: ids.toString() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Existem alunos vinculados a uma das empresas selecionadas, confirma a exclusão',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Empresas", "empresa", "EmpresaPesquisar()", ids);
                }
            });
        } else {
            swal({
                title: 'Confirma a exclusão do(s) registro(s)',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Empresas", "empresa", "EmpresaPesquisar()", ids);
                }
            });
        }
    });
}