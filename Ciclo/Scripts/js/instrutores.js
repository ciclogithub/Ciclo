$(document).ready(function () {

    $("#pesquisa_instrutor").click(function () {
        InstrutorPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirInstrutor();
        };
    });

});

function InstrutorPesquisar() {
    window.location = "/Painel/Instrutores/?instrutor=" + $("#instrutor").val();
}

function Instrutores(id) {
    Modal("/Painel/Instrutores/Incluir", id, "Instrutores");
}

function InstrutorAlterar() {

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
            Instrutores(val);
        }
    }

}

function InstrutorExcluir() {

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
                url: "/Painel/Instrutores/Excluir",
                data: { ident: ids },
                dataType: "json",
                traditional: true,
                success: function () {
                    $("#instrutor").val("");
                    InstrutorPesquisar();
                }
            });
        }
    } else {
        alert("Selecione pelo menos 1 registro")
    }
}

function IncluirInstrutor() {

    var idinstrutor = $("#idinstrutor").val();
    var txinstrutor = $("#txinstrutor").val();
    var txemail = $("#txemail").val();
    var txtelefone = $("#txtelefone").val();
    var txdescritivo = $("#txdescritivo").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Instrutores/IncluirConcluir',
        data: { id: idinstrutor, nome: txinstrutor, email: txemail, telefone: txtelefone, descricao: txdescritivo },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

            InstrutorPesquisar();

        }
    });

}