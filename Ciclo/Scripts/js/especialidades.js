$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_especialidade").click(function () {
        EspecialidadePesquisar();
    });

    $("#incluir_btn_especialidade").click(function () {
        $('#form-modal_especialidade').validationEngine('attach');
        if ($('#form-modal_especialidade').validationEngine('validate')) {
            IncluirEspecialidade();
        };
    });

});

function EspecialidadesTodos() {
    window.location = "/Painel/Especialidades/?pagina=1&filtro=";
}

function EspecialidadePesquisar() {
    window.location = "/Painel/Especialidades/?filtro=" + $("#filtro_pesquisa").val();
}

function Especialidades(id) {
    Modal("/Painel/Especialidades/Incluir", id, "Especialidades", "");
}

function EspecialidadeAlterar() {

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
            Especialidades(val);
        }
    }

}

function EspecialidadeExcluir() {

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
                url: "/Painel/Especialidades/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#especialidade").val("");
                    EspecialidadePesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirEspecialidade() {

    var idespecialidade = $("#form-modal_especialidade #idespecialidade").val();
    var txespecialidade = $("#form-modal_especialidade #txespecialidade").val();
    var txsigla = $("#form-modal_especialidade #txsigla").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Especialidades/IncluirConcluir',
        data: { id: idespecialidade, nome: txespecialidade, sigla: txsigla },
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

                EspecialidadePesquisar();
            }

        }
    });

}