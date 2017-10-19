$(document).ready(function () {

    $("#pesquisa_local").click(function () {
        LocalPesquisar();
    });

    $("#incluir_btn").click(function () {
        $('#form-modal').validationEngine('attach');
        if ($('#form-modal').validationEngine('validate')) {
            IncluirLocal();
        };
    });

    $("#idestado").on("change", function () {
        if ($(this).val() == "") {
            $("#dv_cidade").html("<select id='idcidade' name='idcidade' class='form-control validate[required]'><option value=''>-- Selecione o estado --</option></select>");
        } else {
            $("#dv_cidade").html("Carregando lista ...");

            $.ajax({
                url: "/Painel/Locais/ListaCidades",
                data: { id: $(this).val() },
                cache: false,
                type: "POST",
                success: function (data) {
                    var temp = "";
                    temp += "<select id='idcidade' name='idcidade' class='form-control validate[required]'><option value=''>-- Selecione --</option>";
                    for (var x = 0; x < data.length; x++) {
                        temp += "<option value=" + data[x].idcidade + ">" + data[x].txcidade + "</option>";
                    }
                    temp += "</select>";
                    $("#dv_cidade").html(temp);
                },
                error: function (reponse) {
                    $("#dv_cidade").html("Não foi possível carregar a lista de cidades");
                }
            });
        }
    })

});

function LocalPesquisar() {
    window.location = "/Painel/Locais/?local=" + $("#local").val();
}

function Locais(id) {
    Modal("/Painel/Locais/Incluir", id, "Locais");
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

    var idlocal = $("#idlocal").val();
    var idcidade = $("#idcidade").val();
    var txlocal = $("#txlocal").val();
    var txlogradouro = $("#txlogradouro").val();

    $.ajax({
        type: "POST",
        url: '/Painel/Locais/IncluirConcluir',
        data: { id: idlocal, cidade: idcidade, nome: txlocal, logradouro: txlogradouro },
        dataType: "json",
        traditional: true,
        success: function (json) {

            alert("Operação realizada com sucesso!");

            window.setTimeout(function () {
                $('.modal').modal('hide');
            }, 1000);

            LocalPesquisar();

        }
    });

}