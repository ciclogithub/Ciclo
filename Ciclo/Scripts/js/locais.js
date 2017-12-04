$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_local").click(function () {
        LocalPesquisar();
    });

    $("#incluir_btn_local").click(function () {
        $('#form-modal_local').validationEngine('attach');
        if ($('#form-modal_local').validationEngine('validate')) {
            IncluirLocal();
        };
    });

    if ($("#idpais")) {
        if ($("#idpais").val() != "") {
            ListaEstados($("#idpais").val(), $("#tempestado").val());
            if ($("#tempestado").val() > 0) {
                ListaCidades($("#tempestado").val(), $("#tempcidade").val());
            }
        }
    }
});

function LocaisTodos() {
    window.location = "/Painel/Locais/?pagina=1&filtro=";
}

function LocalPesquisar() {
    window.location = "/Painel/Locais/?filtro=" + $("#filtro_pesquisa").val();
}

function Locais(id) {
    Modal("/Painel/Locais/Incluir", id, "Locais", "");
}

function LocalAlterar() {

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
            Locais(val);
        }
    }

}

function LocalExcluir() {

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
                url: "/Painel/Locais/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#local").val("");
                    LocalPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirLocal() {

    var idlocal = $("#form-modal_local #idlocal").val();
    var idcidade = $("#form-modal_local #idcidade").val();
    var txlocal = $("#form-modal_local #txlocal").val();
    var txlogradouro = $("#form-modal_local #txlogradouro").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Locais/IncluirConcluir',
        data: { id: idlocal, cidade: idcidade, nome: txlocal, logradouro: txlogradouro },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            if ($("#modal2").is(":visible")) {
                window.setTimeout(function () {
                    $('#modal2.modal').modal('hide');
                }, 1000);
            } else {

                window.setTimeout(function () {
                    $('#modal1.modal').modal('hide');
                }, 1000);

                LocalPesquisar();
            }

        }
    });

}