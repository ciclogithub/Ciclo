$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_local").click(function () {
        LocalPesquisar();
    });

    $("#incluir_btn_local").click(function () {
        $('#form-modal_local').validationEngine('attach');
        if ($('#form-modal_local').validationEngine('validate')) {
            ValidaLocalInclusao();
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
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    } else {
        if (cont > 1) {
            swal({ title: "Selecione somente 1 registro para alterar", type: "error", timer: 3000 });
        } else {
            Locais(val);
        }
    }

}

function LocalExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        ValidaLocalExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
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

            swal({
                title: 'Operação realizada com sucesso!',
                type: 'success',
                confirmButtonText: 'Fechar',
                timer: 3000
            }).then((result) => {
                if (result.dismiss == 'timer') {
                    closeModal("LocalPesquisar()");
                }
                if (result.value) {
                    closeModal("LocalPesquisar()");
                }
            });

        }
    });

}

function ValidaLocalInclusao() {

    $.post("/Painel/Locais/VerificaLocais", { id: $("#form-modal_local #idlocal").val(), nome: $("#form-modal_local #txlocal").val() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Já existe um local com o mesmo nome, confirma a gravação de um novo registro',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    IncluirLocal();
                }
            });
        } else {
            IncluirLocal();
        }
    });
}

function ValidaLocalExcluir(ids) {
    $.post("/Painel/Locais/VerificaLocaisExcluir", { id: ids.toString() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Existem eventos vinculados a um dos locais selecionados, confirma a exclusão',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Locais", "local", "LocalPesquisar()", ids);
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
                    confirmaExcluir("Locais", "local", "LocalPesquisar()", ids);
                }
            });
        }
    });
}