$(document).ready(function () {

    $('select').not('.no-js').select2();

    $("#pesquisa_tema").click(function () {
        TemaPesquisar();
    });

    $("#incluir_btn_tema").click(function () {
        $('#form-modal_tema').validationEngine('attach');
        if ($('#form-modal_tema').validationEngine('validate')) {
            IncluirTema();
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

    if (cont === 0) {
        alert("Selecione pelo menos 1 registro");
    } else {
        if (cont > 1) {
            alert("Selecione somente 1 registro para alterar");
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

    if (ids !== "") {
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
        alert("Selecione pelo menos 1 registro");
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

            alert("Operação realizada com sucesso!");

            if ($("#modal2").is(":visible")) {
                window.setTimeout(function () {
                    $('#modal2.modal').modal('hide');
                }, 1000);
            } else {

                window.setTimeout(function () {
                    $('#modal1.modal').modal('hide');
                }, 1000);

                $("#filtro_pesquisa").val("");
                TemaPesquisar();
            }

        }
    });

}

//function ValidaTema() {

//    // Tema já existe
//    $.post("/Painel/Temas/VerificaTema", { id: $("#form-modal_tema #idtema").val(), nome: $("#form-modal_tema #txtema").val() }).done(function (data) {
//        if (data == 1) {
//            swal({
//                title: 'Já existe um tema com o mesmo nome, confirma a gravação de um novo registro',
//                type: 'warning',
//                showCancelButton: true,
//                confirmButtonText: 'Sim',
//                cancelButtonText: 'Não'
//            }).then((result) => {
//                if (result.value) {
//                    return true;
//                }
//            })
//        } else {
//            return true;
//        }
//    });
//    return true
//}