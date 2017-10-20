﻿$(document).ready(function () {

    $("#pesquisa_tema").click(function () {
        TemaPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirTema();
        };
    });

});

function TemaPesquisar() {
    window.location = "/Painel/Temas/?tema=" + $("#tema").val();
}

function Temas(id) {
    Modal("/Painel/Temas/Incluir", id, "Temas");
}

function TemaAlterar() {

    cont = 0;
    val = 0;
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            cont++;
            val = $(this).val();
        }
    });

    if (cont == 0) {
        alert("Selecione pelo menos 1 registro")
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar")
        } else {
            Temas(val);
        }
    }

}

function TemaExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val()
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        if (confirm("Certeza que deseja excluir o(s) registro(s) selecionado(s)?")) {
            $.ajax({
                type: "POST",
                url: "/Painel/Temas/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#tema").val("");
                    TemaPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirTema() {

    var idtema = $("#idtema").val();
    var txtema = $("#txtema").val();
    var txsubtitulo = $("#txsubtitulo").val();
    var txdescritivo = $("#txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Temas/IncluirConcluir',
        data: { id: idtema, nome: txtema, subtitulo: txsubtitulo, descricao: txdescritivo },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

            TemaPesquisar();

        }
    });

}