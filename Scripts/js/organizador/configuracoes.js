﻿$(document).ready(function () {

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirConfiguracao();
        }
    });

    $("#incluir_btn_alterar").click(function () {
        $('#form-modal_alterar').validationEngine('attach');
        if ($('#form-modal_alterar').validationEngine('validate')) {
            AlterarSenha();
        }
    });

});

function IncluirConfiguracao() {

    var idorganizador = $("#form-modal #idorganizador").val();
    var txorganizador = $("#form-modal #txorganizador").val();
    var txtelefone = $("#form-modal #txtelefone").val();
    var txdescritivo = $("#form-modal #txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Configuracoes/IncluirConcluir',
        data: { id: idorganizador, organizador: txorganizador, telefone: txtelefone, descricao: txdescritivo },
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
                    location.href = "Configuracoes";
                }
                if (result.value) {
                    location.href = "Configuracoes";
                }
            });

        }
    });
}

function AlterarSenha() {

    var idorganizador = $("#form-modal_alterar #idorganizador").val();
    var txsenhaatual = $("#form-modal_alterar #txsenhaatual").val();
    var txnovasenha = $("#form-modal_alterar #txnovasenha").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Configuracoes/AlterarSenha',
        data: { id: idorganizador, senha: txsenhaatual, novasenha: txnovasenha },
        dataType: "json",
        traditional: true,
        success: function (json) {
            json = jQuery.parseJSON(json);
            if (json.retorno == "OK") {
                swal({
                    title: 'Operação realizada com sucesso!',
                    type: 'success',
                    confirmButtonText: 'Fechar',
                    timer: 3000
                }).then((result) => {
                    if (result.dismiss == 'timer') {
                        location.href = "Configuracoes";
                    }
                    if (result.value) {
                        location.href = "Configuracoes";
                    }
                });                
            } else {
                swal({ title: json.retorno, type: "error", timer: 3000 });
            }
        }
    });

}