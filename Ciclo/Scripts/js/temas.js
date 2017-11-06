$(document).ready(function () {
    
    $("#pesquisa_tema").click(function () {
        TemaPesquisar();
    });

    $("#incluir_btn_tema").click(function () {
        $('#form-modal_tema').validationEngine('attach');
        if ($('#form-modal_tema').validationEngine('validate')) {
            IncluirTema();
        };
    });

});

function pagination(c) {
    var p = $("#page").val();
    var t = $("#totalpage").val();
    if (c == -1) {
        c = parseInt(p) - 1;
        if (c <= 0) { c = 1 }
        window.location = "/Painel/Temas/?pagina=" + c + "&tema=" + $("#tema").val();
    } else {
        if (c == 0) {
            c = parseInt(p) + 1;
            if (c > t) { c = t }
            window.location = "/Painel/Temas/?pagina=" + c + "&tema=" + $("#tema").val();
        } else {
            window.location = "/Painel/Temas/?pagina=" + c + "&tema=" + $("#tema").val();
        }
    }
}

function TemasTodos() {
    window.location = "/Painel/Temas/?pagina=1&tema=";
}

function TemaPesquisar() {
    window.location = "/Painel/Temas/?tema=" + $("#tema").val();
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

                TemaPesquisar();
            }

        }
    });

}