$(document).ready(function () {
    $("#pesquisa_instrutor").click(function () {
        InstrutorPesquisar();
    });
});

function InstrutorPesquisar() {
    window.location = "/Painel/Instrutores/?instrutor=" + $("#instrutor").val();
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
                url: "/Painel/InstrutorExcluir/",
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
