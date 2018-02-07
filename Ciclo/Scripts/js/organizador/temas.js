$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_tema").click(function () {
        TemaPesquisar();
    });

    $("#incluir_btn_tema").click(function () {
        $('#form-modal_tema').validationEngine('attach');
        if ($('#form-modal_tema').validationEngine('validate')) {            
            ValidaTemaInclusao();
        }
    });

});

function TemasTodos() {
    window.location = "/Painel/Temas/?pagina=1&filtro=";
}

function TemaPesquisar() {
    window.location = "/Painel/Temas/?filtro=" + $("#filtro_pesquisa").val();
}

function Temas(id) {
    Modal("/Painel/Temas/Incluir", id, "Temas", "");
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
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    } else {
        if (cont > 1) {
            swal({ title: "Selecione somente 1 registro para alterar", type: "error", timer: 3000 });
        } else {
            Temas(val);
        }
    }

}

function TemaExcluir() {

    ids = "";
    $("input[name='ident']").each(function () {
        if ($(this).is(":checked")) {
            ids = ids + "," + $(this).val();
        }
    });

    var ids = ids.substring(1);

    if (ids != "") {
        ValidaTemaExcluir(ids);
    } else {
        swal({ title: "Selecione pelo menos 1 registro", type: "error", timer: 3000 });
    }
}

function IncluirTema() {
    
    var idtema = $("#form-modal_tema #idtema").val();
    var txtema = $("#form-modal_tema #txtema").val();
    var txsubtitulo = $("#form-modal_tema #txsubtitulo").val();
    var txdescritivo = $("#form-modal_tema #txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Temas/IncluirConcluir',
        data: { id: idtema, nome: txtema, subtitulo: txsubtitulo, descricao: txdescritivo },
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
                    closeModal("TemaPesquisar()");
                }
                if (result.value) {
                    closeModal("TemaPesquisar()");
                }
            });

        }
    });

}

function ValidaTemaInclusao() {

    $.post("/Painel/Temas/VerificaTema", { id: $("#form-modal_tema #idtema").val(), nome: $("#form-modal_tema #txtema").val() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Já existe um tema com o mesmo nome, confirma a gravação de um novo registro',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    IncluirTema();
                }
            });
        } else {
            IncluirTema();
        }
    });
}

function ValidaTemaExcluir(ids) {
    $.post("/Painel/Temas/VerificaTemaExcluir", { id: ids.toString() }).done(function (data) {
        if (data == 1) {
            swal({
                title: 'Existem eventos vinculados a um dos temas selecionados, confirma a exclusão',
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Sim',
                cancelButtonText: 'Não'
            }).then((result) => {
                if (result.value) {
                    confirmaExcluir("Temas", "tema", "TemaPesquisar()", ids);
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
                    confirmaExcluir("Temas", "tema", "TemaPesquisar()", ids);
                }
            });
        }
    });
}