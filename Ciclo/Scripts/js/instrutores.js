$(document).ready(function () {

    $("#pesquisa_instrutor").click(function () {
        InstrutorPesquisar();
    });

    $("#incluir_btn_instrutor").click(function () {
        $('#form-modal_instrutor').validationEngine('attach');
        if ($('#form-modal_instrutor').validationEngine('validate')) {
            IncluirInstrutor();
        };
    });

});

function InstrutoresTodos() {
    window.location = "/Painel/Instrutores/?pagina=1&filtro=";
}

function InstrutorPesquisar() {
    window.location = "/Painel/Instrutores/?filtro=" + $("#filtro_pesquisa").val();
}

function Instrutores(id) {
    Modal("/Painel/Instrutores/Incluir", id, "Instrutores", "");
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

    var idinstrutor = $("#form-modal_instrutor #idinstrutor").val();
    var txinstrutor = $("#form-modal_instrutor #txinstrutor").val();
    var txemail = $("#form-modal_instrutor #txemail").val();
    var txtelefone = $("#form-modal_instrutor #txtelefone").val();
    var txdescritivo = $("#form-modal_instrutor #txdescritivo").val();

    var form = $('#form-modal_instrutor #form-modal')[0];
    var data = new FormData(form);
    data.append("id", idinstrutor);
    data.append("nome", txinstrutor);
    data.append("email", txemail);
    data.append("telefone", txtelefone);
    data.append("descricao", txdescritivo);

    $.ajax({
        type: "POST",
        url: '/Painel/Instrutores/IncluirConcluir',
        data: data,
        processData: false,
        contentType: false,
        cache: false,
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

                InstrutorPesquisar();
            }

        }
    });

}

function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#prev_img').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
